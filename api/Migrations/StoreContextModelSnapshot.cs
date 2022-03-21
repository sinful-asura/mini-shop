﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models;

#nullable disable

namespace api.Migrations
{
    [DbContext(typeof(StoreContext))]
    partial class StoreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Models.Item", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int?>("BelongsToSectionFK")
                        .HasColumnType("int");

                    b.Property<float>("Discount")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<int?>("SaleID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("BelongsToSectionFK");

                    b.HasIndex("SaleID");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("Models.Sale", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Coupon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SaleDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("SellerID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("SellerID");

                    b.ToTable("Sale");
                });

            modelBuilder.Entity("Models.Section", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("ID");

                    b.ToTable("Section");
                });

            modelBuilder.Entity("Models.Store", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("ID");

                    b.ToTable("Store");
                });

            modelBuilder.Entity("Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Salary")
                        .HasColumnType("real");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Models.WorksIn", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("StoreID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("StoreID");

                    b.HasIndex("UserID");

                    b.ToTable("WorksIn");
                });

            modelBuilder.Entity("Models.Item", b =>
                {
                    b.HasOne("Models.Section", "Section")
                        .WithMany("Items")
                        .HasForeignKey("BelongsToSectionFK");

                    b.HasOne("Models.Sale", null)
                        .WithMany("SoldItems")
                        .HasForeignKey("SaleID");

                    b.Navigation("Section");
                });

            modelBuilder.Entity("Models.Sale", b =>
                {
                    b.HasOne("Models.User", "Seller")
                        .WithMany()
                        .HasForeignKey("SellerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("Models.WorksIn", b =>
                {
                    b.HasOne("Models.Store", "Store")
                        .WithMany("Staff")
                        .HasForeignKey("StoreID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.User", "User")
                        .WithMany("Workplaces")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Store");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Models.Sale", b =>
                {
                    b.Navigation("SoldItems");
                });

            modelBuilder.Entity("Models.Section", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("Models.Store", b =>
                {
                    b.Navigation("Staff");
                });

            modelBuilder.Entity("Models.User", b =>
                {
                    b.Navigation("Workplaces");
                });
#pragma warning restore 612, 618
        }
    }
}
