﻿// <auto-generated />
using System;
using Gravity.Express.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Gravity.Express.Infrastructure.Migrations
{
    [DbContext(typeof(GravityExpressDbContext))]
    partial class GravityExpressDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Gravity.Express.Domain.Entities.AuditLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("LastUpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("TenantId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("AuditLogs");
                });

            modelBuilder.Entity("Gravity.Express.Domain.Entities.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPassive")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Gravity.Express.Domain.Entities.Delivery", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("ActualDeliveryDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<string>("DeliveryAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ECommercePlatformId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("EstimatedDeliveryDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPassive")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TrackingNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ECommercePlatformId");

                    b.HasIndex("OrderId");

                    b.ToTable("Deliveries");
                });

            modelBuilder.Entity("Gravity.Express.Domain.Entities.ECommercePlatform", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ApiKey")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ApiUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPassive")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ECommercePlatforms");
                });

            modelBuilder.Entity("Gravity.Express.Domain.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ECommercePlatformId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPassive")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("OrderStatus")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ECommercePlatformId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Gravity.Express.Domain.Entities.Delivery", b =>
                {
                    b.HasOne("Gravity.Express.Domain.Entities.ECommercePlatform", "ECommercePlatform")
                        .WithMany("Deliveries")
                        .HasForeignKey("ECommercePlatformId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gravity.Express.Domain.Entities.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ECommercePlatform");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Gravity.Express.Domain.Entities.Order", b =>
                {
                    b.HasOne("Gravity.Express.Domain.Entities.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gravity.Express.Domain.Entities.ECommercePlatform", "ECommercePlatform")
                        .WithMany()
                        .HasForeignKey("ECommercePlatformId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("ECommercePlatform");
                });

            modelBuilder.Entity("Gravity.Express.Domain.Entities.Customer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Gravity.Express.Domain.Entities.ECommercePlatform", b =>
                {
                    b.Navigation("Deliveries");
                });
#pragma warning restore 612, 618
        }
    }
}
