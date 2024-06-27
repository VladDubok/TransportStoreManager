using Microsoft.EntityFrameworkCore;
using TransportStoreManagerApi.Data;
using TransportStoreManagerApi.Managers;
using TransportStoreManagerApi.Repositories;
using TransportStoreManagerApi.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<IProductManager, ProductManager>();
builder.Services.AddScoped<IFileManager, FileManager>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(x => x.LowercaseUrls = true);
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.MapControllers();
app.UseHttpsRedirection();
app.Run();