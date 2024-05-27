﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TransportStoreManagerApi.Data;

#nullable disable

namespace TransportStoreManagerApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-preview.4.24267.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TransportStoreManagerApi.Data.Entities.BlobFile", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<byte[]>("Data")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.ToTable("BlobFile");
                });

            modelBuilder.Entity("TransportStoreManagerApi.Data.Entities.Brand", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Brand");
                });

            modelBuilder.Entity("TransportStoreManagerApi.Data.Entities.Currency", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Currency");
                });

            modelBuilder.Entity("TransportStoreManagerApi.Data.Entities.Customer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("TransportStoreManagerApi.Data.Entities.OutboxMessages", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OutboxMessages");
                });

            modelBuilder.Entity("TransportStoreManagerApi.Data.Entities.Product", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("BrandId")
                        .HasColumnType("bigint");

                    b.Property<long?>("BrandId1")
                        .HasColumnType("bigint");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Count")
                        .HasColumnType("bigint");

                    b.Property<long>("CurrencyId")
                        .HasColumnType("bigint");

                    b.Property<long?>("CurrencyId1")
                        .HasColumnType("bigint");

                    b.Property<long>("CustomerId")
                        .HasColumnType("bigint");

                    b.Property<long?>("CustomerId1")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<long>("ProductTypeId")
                        .HasColumnType("bigint");

                    b.Property<long?>("ProductTypeId1")
                        .HasColumnType("bigint");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("BrandId1");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("CurrencyId1");

                    b.HasIndex("CustomerId");

                    b.HasIndex("CustomerId1");

                    b.HasIndex("ProductTypeId");

                    b.HasIndex("ProductTypeId1");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("TransportStoreManagerApi.Data.Entities.ProductPhoto", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long?>("BlobFileId")
                        .HasColumnType("bigint");

                    b.Property<long>("FileId")
                        .HasColumnType("bigint");

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint");

                    b.Property<long?>("ProductId1")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("BlobFileId");

                    b.HasIndex("FileId");

                    b.HasIndex("ProductId");

                    b.HasIndex("ProductId1");

                    b.ToTable("ProductPhoto");
                });

            modelBuilder.Entity("TransportStoreManagerApi.Data.Entities.ProductPromotion", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint");

                    b.Property<long?>("ProductId1")
                        .HasColumnType("bigint");

                    b.Property<long>("PromotionId")
                        .HasColumnType("bigint");

                    b.Property<long?>("PromotionId1")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("ProductId1");

                    b.HasIndex("PromotionId");

                    b.HasIndex("PromotionId1");

                    b.ToTable("ProductPromotion");
                });

            modelBuilder.Entity("TransportStoreManagerApi.Data.Entities.ProductPromotionHistory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("Percent")
                        .HasColumnType("int");

                    b.Property<long>("ProductPromotionId")
                        .HasColumnType("bigint");

                    b.Property<long?>("ProductPromotionId1")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ProductPromotionId");

                    b.HasIndex("ProductPromotionId1");

                    b.ToTable("ProductPromotionHistory");
                });

            modelBuilder.Entity("TransportStoreManagerApi.Data.Entities.ProductType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ProductType");
                });

            modelBuilder.Entity("TransportStoreManagerApi.Data.Entities.Promotion", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Percent")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Promotion");
                });

            modelBuilder.Entity("TransportStoreManagerApi.Data.Entities.Product", b =>
                {
                    b.HasOne("TransportStoreManagerApi.Data.Entities.Brand", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TransportStoreManagerApi.Data.Entities.Brand", null)
                        .WithMany("Products")
                        .HasForeignKey("BrandId1");

                    b.HasOne("TransportStoreManagerApi.Data.Entities.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TransportStoreManagerApi.Data.Entities.Currency", null)
                        .WithMany("Products")
                        .HasForeignKey("CurrencyId1");

                    b.HasOne("TransportStoreManagerApi.Data.Entities.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TransportStoreManagerApi.Data.Entities.Customer", null)
                        .WithMany("Products")
                        .HasForeignKey("CustomerId1");

                    b.HasOne("TransportStoreManagerApi.Data.Entities.ProductType", "ProductType")
                        .WithMany()
                        .HasForeignKey("ProductTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TransportStoreManagerApi.Data.Entities.ProductType", null)
                        .WithMany("Products")
                        .HasForeignKey("ProductTypeId1");

                    b.Navigation("Brand");

                    b.Navigation("Currency");

                    b.Navigation("Customer");

                    b.Navigation("ProductType");
                });

            modelBuilder.Entity("TransportStoreManagerApi.Data.Entities.ProductPhoto", b =>
                {
                    b.HasOne("TransportStoreManagerApi.Data.Entities.BlobFile", null)
                        .WithMany("ProductPhotos")
                        .HasForeignKey("BlobFileId");

                    b.HasOne("TransportStoreManagerApi.Data.Entities.BlobFile", "BlobFile")
                        .WithMany()
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TransportStoreManagerApi.Data.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TransportStoreManagerApi.Data.Entities.Product", null)
                        .WithMany("ProductPhotos")
                        .HasForeignKey("ProductId1");

                    b.Navigation("BlobFile");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("TransportStoreManagerApi.Data.Entities.ProductPromotion", b =>
                {
                    b.HasOne("TransportStoreManagerApi.Data.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TransportStoreManagerApi.Data.Entities.Product", null)
                        .WithMany("ProductPromotions")
                        .HasForeignKey("ProductId1");

                    b.HasOne("TransportStoreManagerApi.Data.Entities.Promotion", "Promotion")
                        .WithMany()
                        .HasForeignKey("PromotionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TransportStoreManagerApi.Data.Entities.Promotion", null)
                        .WithMany("ProductPromotions")
                        .HasForeignKey("PromotionId1");

                    b.Navigation("Product");

                    b.Navigation("Promotion");
                });

            modelBuilder.Entity("TransportStoreManagerApi.Data.Entities.ProductPromotionHistory", b =>
                {
                    b.HasOne("TransportStoreManagerApi.Data.Entities.ProductPromotion", "ProductPromotion")
                        .WithMany()
                        .HasForeignKey("ProductPromotionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TransportStoreManagerApi.Data.Entities.ProductPromotion", null)
                        .WithMany("ProductPromotionHistories")
                        .HasForeignKey("ProductPromotionId1");

                    b.Navigation("ProductPromotion");
                });

            modelBuilder.Entity("TransportStoreManagerApi.Data.Entities.BlobFile", b =>
                {
                    b.Navigation("ProductPhotos");
                });

            modelBuilder.Entity("TransportStoreManagerApi.Data.Entities.Brand", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("TransportStoreManagerApi.Data.Entities.Currency", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("TransportStoreManagerApi.Data.Entities.Customer", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("TransportStoreManagerApi.Data.Entities.Product", b =>
                {
                    b.Navigation("ProductPhotos");

                    b.Navigation("ProductPromotions");
                });

            modelBuilder.Entity("TransportStoreManagerApi.Data.Entities.ProductPromotion", b =>
                {
                    b.Navigation("ProductPromotionHistories");
                });

            modelBuilder.Entity("TransportStoreManagerApi.Data.Entities.ProductType", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("TransportStoreManagerApi.Data.Entities.Promotion", b =>
                {
                    b.Navigation("ProductPromotions");
                });
#pragma warning restore 612, 618
        }
    }
}
