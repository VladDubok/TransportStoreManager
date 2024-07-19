using Hangfire;
using Microsoft.EntityFrameworkCore;
using TransportStoreManagerApi.Data;
using TransportStoreManagerApi.Handlers;
using TransportStoreManagerApi.Managers;
using TransportStoreManagerApi.Managers.Interfaces;
using TransportStoreManagerApi.Repositories;
using TransportStoreManagerApi.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddAutoMapper(typeof(Program));

builder.Services
    .AddHangfire(configuration => configuration.UseSqlServerStorage(connectionString))
    .AddHangfireServer();

builder.Services.AddScoped<AppDbContext>();
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<IProductManager, ProductManager>();
builder.Services.AddScoped<IFileManager, FileManager>();
builder.Services.AddScoped<IPriceManager, PriceManager>();
builder.Services.AddScoped<IBrandManager, BrandManager>();
builder.Services.AddScoped<IOutboxMessageManager, OutboxMessageManager>();
builder.Services.AddScoped<IPromotionManager, PromotionManager>();

builder.Services.AddScoped<IOutboxMessageProcessor, OutboxMessageProcessor>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(x => x.LowercaseUrls = true);
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHangfireDashboard();
}

app.UseRouting();
app.MapHangfireDashboard();
app.MapControllers();
app.UseHttpsRedirection();

using var scope = app.Services.CreateScope();
RecurringJob.AddOrUpdate(() => scope.ServiceProvider.GetRequiredService<IOutboxMessageProcessor>().Process(),
    Cron.Hourly);

app.Run();