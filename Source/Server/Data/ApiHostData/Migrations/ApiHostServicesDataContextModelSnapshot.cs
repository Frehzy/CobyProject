﻿// <auto-generated />
using System;
using ApiHostData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ApiHostData.Migrations
{
    [DbContext(typeof(ApiHostServicesDataContext))]
    partial class ApiHostServicesDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ApiHostData.Domain.Entities.DiscountTypeEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.Property<Guid>("WaiterCreatedId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("WaiterUpdatedId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("AllDiscountTypes");
                });

            modelBuilder.Entity("ApiHostData.Domain.Entities.Order.OrderDiscountEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("DiscountSum")
                        .HasColumnType("numeric");

                    b.Property<Guid>("DiscountTypeEntityId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<Guid>("OrderEntityId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("OrderEntityId");

                    b.ToTable("OrderDiscountEntity");
                });

            modelBuilder.Entity("ApiHostData.Domain.Entities.Order.OrderGuestEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("OrderEntityId")
                        .HasColumnType("uuid");

                    b.Property<int>("Rank")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OrderEntityId");

                    b.ToTable("OrderGuestEntity");
                });

            modelBuilder.Entity("ApiHostData.Domain.Entities.Order.OrderPaymentEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("OrderEntityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PaymentTypeEntityId")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<decimal>("Sum")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("OrderEntityId");

                    b.ToTable("OrderPaymentEntity");
                });

            modelBuilder.Entity("ApiHostData.Domain.Entities.Order.OrderProductEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<Guid>("OrderEntityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("OrderGuestEntityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("OrderWaiterEntityId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("PrintTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("ProductItemEntityId")
                        .HasColumnType("uuid");

                    b.Property<byte>("Status")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("OrderEntityId");

                    b.ToTable("OrderProductEntity");
                });

            modelBuilder.Entity("ApiHostData.Domain.Entities.Order.OrderTableEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("OrderEntityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TableEntityId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("OrderEntityId");

                    b.ToTable("OrderTableEntity");
                });

            modelBuilder.Entity("ApiHostData.Domain.Entities.Order.OrderWaiterEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("OrderEntityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("WaiterEntityId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("OrderEntityId")
                        .IsUnique();

                    b.ToTable("OrderWaiterEntity");
                });

            modelBuilder.Entity("ApiHostData.Domain.Entities.OrderEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CloseTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<byte>("Status")
                        .HasColumnType("smallint");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.Property<Guid>("WaiterCreatedId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("WaiterUpdatedId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("AllOrders");
                });

            modelBuilder.Entity("ApiHostData.Domain.Entities.PaymentTypeEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("Kind")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("NeedOpenCashBox")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.Property<Guid>("WaiterCreatedId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("WaiterUpdatedId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("AllPaymentTypes");
                });

            modelBuilder.Entity("ApiHostData.Domain.Entities.ProductItemEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<byte>("Type")
                        .HasColumnType("smallint");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.Property<Guid>("WaiterCreatedId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("WaiterUpdatedId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("AllProducts");
                });

            modelBuilder.Entity("ApiHostData.Domain.Entities.TableEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.Property<Guid>("WaiterCreatedId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("WaiterUpdatedId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("AllTables");
                });

            modelBuilder.Entity("ApiHostData.Domain.Entities.WaiterEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsSessionOpen")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("Permissions")
                        .IsRequired()
                        .HasColumnType("smallint[]");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.Property<Guid>("WaiterCreatedId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("WaiterUpdatedId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("AllWaiters");

                    b.HasData(
                        new
                        {
                            Id = new Guid("2c4c850c-728e-45a9-bb41-615f5723e0aa"),
                            CreatedTime = new DateTime(2022, 6, 25, 18, 54, 10, 118, DateTimeKind.Local).AddTicks(4290),
                            IsDeleted = false,
                            IsSessionOpen = false,
                            Name = "ADMIN",
                            Password = "ADMINPASSWORD",
                            Permissions = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21 },
                            Version = 1,
                            WaiterCreatedId = new Guid("00000000-0000-0000-0000-000000000000"),
                            WaiterUpdatedId = new Guid("00000000-0000-0000-0000-000000000000")
                        });
                });

            modelBuilder.Entity("ApiHostData.Domain.Entities.Order.OrderDiscountEntity", b =>
                {
                    b.HasOne("ApiHostData.Domain.Entities.OrderEntity", "OrderEntity")
                        .WithMany("OrderDiscountEntities")
                        .HasForeignKey("OrderEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderEntity");
                });

            modelBuilder.Entity("ApiHostData.Domain.Entities.Order.OrderGuestEntity", b =>
                {
                    b.HasOne("ApiHostData.Domain.Entities.OrderEntity", "OrderEntity")
                        .WithMany("OrderGuestEntities")
                        .HasForeignKey("OrderEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderEntity");
                });

            modelBuilder.Entity("ApiHostData.Domain.Entities.Order.OrderPaymentEntity", b =>
                {
                    b.HasOne("ApiHostData.Domain.Entities.OrderEntity", "OrderEntity")
                        .WithMany("OrderPaymentEntities")
                        .HasForeignKey("OrderEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderEntity");
                });

            modelBuilder.Entity("ApiHostData.Domain.Entities.Order.OrderProductEntity", b =>
                {
                    b.HasOne("ApiHostData.Domain.Entities.OrderEntity", "OrderEntity")
                        .WithMany("OrderProductEntities")
                        .HasForeignKey("OrderEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderEntity");
                });

            modelBuilder.Entity("ApiHostData.Domain.Entities.Order.OrderTableEntity", b =>
                {
                    b.HasOne("ApiHostData.Domain.Entities.OrderEntity", "OrderEntity")
                        .WithMany("OrderTableEntities")
                        .HasForeignKey("OrderEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderEntity");
                });

            modelBuilder.Entity("ApiHostData.Domain.Entities.Order.OrderWaiterEntity", b =>
                {
                    b.HasOne("ApiHostData.Domain.Entities.OrderEntity", "OrderEntity")
                        .WithOne("OrderWaiterEntity")
                        .HasForeignKey("ApiHostData.Domain.Entities.Order.OrderWaiterEntity", "OrderEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderEntity");
                });

            modelBuilder.Entity("ApiHostData.Domain.Entities.OrderEntity", b =>
                {
                    b.Navigation("OrderDiscountEntities");

                    b.Navigation("OrderGuestEntities");

                    b.Navigation("OrderPaymentEntities");

                    b.Navigation("OrderProductEntities");

                    b.Navigation("OrderTableEntities");

                    b.Navigation("OrderWaiterEntity")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
