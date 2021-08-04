﻿// <auto-generated />
using System;
using Kalabean.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Kalabean.API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Kalabean.Domain.Entities.AccessRule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AccessRules");
                });

            modelBuilder.Entity("Kalabean.Domain.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid?>("AccessRuleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HtmlContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte?>("Order")
                        .HasColumnType("tinyint");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccessRuleId");

                    b.HasIndex("ParentId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Kalabean.Domain.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid?>("AccessRuleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HasImage")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte?>("Order")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.HasIndex("AccessRuleId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Kalabean.Domain.Entities.Floor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte?>("Order")
                        .HasColumnType("tinyint");

                    b.Property<int>("ShoppingCenterId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ShoppingCenterId");

                    b.ToTable("Floors");
                });

            modelBuilder.Entity("Kalabean.Domain.Entities.ShoppingCenter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HasAuction")
                        .HasColumnType("bit");

                    b.Property<bool>("HasImage")
                        .HasColumnType("bit");

                    b.Property<string>("InstagramUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShoppingCenterServices")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TelegramUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.Property<string>("VirtualTourUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WebsiteUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WorkingHours")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("TypeId");

                    b.ToTable("ShoppingCenters");
                });

            modelBuilder.Entity("Kalabean.Domain.Entities.ShoppingCenterType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid?>("AccessRuleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HasImage")
                        .HasColumnType("bit");

                    b.Property<string>("HtmlContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte?>("Order")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.HasIndex("AccessRuleId");

                    b.ToTable("ShoppingCenterTypes");
                });

            modelBuilder.Entity("Kalabean.Domain.Entities.Store", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("AuctionPercentage")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("real")
                        .HasDefaultValue(0f);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("DiscountCoupon")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)")
                        .HasDefaultValue(0m);

                    b.Property<float>("DiscountPercentage")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("real")
                        .HasDefaultValue(0f);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FloorId")
                        .HasColumnType("int");

                    b.Property<bool>("HasImage")
                        .HasColumnType("bit");

                    b.Property<string>("InstagramUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Lat")
                        .HasColumnType("float");

                    b.Property<double?>("Lng")
                        .HasColumnType("float");

                    b.Property<string>("Mobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ShoppingCenterId")
                        .HasColumnType("int");

                    b.Property<int?>("ShoppingCenterTypeId")
                        .HasColumnType("int");

                    b.Property<string>("StoreNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TelegramUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VirtualTourUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WebsiteUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WorkingHours")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("FloorId");

                    b.HasIndex("ShoppingCenterId");

                    b.HasIndex("ShoppingCenterTypeId");

                    b.ToTable("Stores");
                });

            modelBuilder.Entity("Kalabean.Domain.Entities.Category", b =>
                {
                    b.HasOne("Kalabean.Domain.Entities.AccessRule", "AccessRule")
                        .WithMany("Category")
                        .HasForeignKey("AccessRuleId");

                    b.HasOne("Kalabean.Domain.Entities.Category", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.Navigation("AccessRule");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Kalabean.Domain.Entities.City", b =>
                {
                    b.HasOne("Kalabean.Domain.Entities.AccessRule", "AccessRule")
                        .WithMany("City")
                        .HasForeignKey("AccessRuleId");

                    b.Navigation("AccessRule");
                });

            modelBuilder.Entity("Kalabean.Domain.Entities.Floor", b =>
                {
                    b.HasOne("Kalabean.Domain.Entities.ShoppingCenter", "ShoppingCenter")
                        .WithMany("Floors")
                        .HasForeignKey("ShoppingCenterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ShoppingCenter");
                });

            modelBuilder.Entity("Kalabean.Domain.Entities.ShoppingCenter", b =>
                {
                    b.HasOne("Kalabean.Domain.Entities.City", "City")
                        .WithMany("ShoppingCenters")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kalabean.Domain.Entities.ShoppingCenterType", "Type")
                        .WithMany("ShoppingCenters")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Kalabean.Domain.Entities.ShoppingCenterType", b =>
                {
                    b.HasOne("Kalabean.Domain.Entities.AccessRule", "AccessRule")
                        .WithMany("ShoppingCenterTypes")
                        .HasForeignKey("AccessRuleId");

                    b.Navigation("AccessRule");
                });

            modelBuilder.Entity("Kalabean.Domain.Entities.Store", b =>
                {
                    b.HasOne("Kalabean.Domain.Entities.Category", "Category")
                        .WithMany("Stores")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kalabean.Domain.Entities.Floor", "Floor")
                        .WithMany("Stores")
                        .HasForeignKey("FloorId");

                    b.HasOne("Kalabean.Domain.Entities.ShoppingCenter", "ShoppingCenter")
                        .WithMany("Stores")
                        .HasForeignKey("ShoppingCenterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kalabean.Domain.Entities.ShoppingCenterType", null)
                        .WithMany("Stores")
                        .HasForeignKey("ShoppingCenterTypeId");

                    b.Navigation("Category");

                    b.Navigation("Floor");

                    b.Navigation("ShoppingCenter");
                });

            modelBuilder.Entity("Kalabean.Domain.Entities.AccessRule", b =>
                {
                    b.Navigation("Category");

                    b.Navigation("City");

                    b.Navigation("ShoppingCenterTypes");
                });

            modelBuilder.Entity("Kalabean.Domain.Entities.Category", b =>
                {
                    b.Navigation("Children");

                    b.Navigation("Stores");
                });

            modelBuilder.Entity("Kalabean.Domain.Entities.City", b =>
                {
                    b.Navigation("ShoppingCenters");
                });

            modelBuilder.Entity("Kalabean.Domain.Entities.Floor", b =>
                {
                    b.Navigation("Stores");
                });

            modelBuilder.Entity("Kalabean.Domain.Entities.ShoppingCenter", b =>
                {
                    b.Navigation("Floors");

                    b.Navigation("Stores");
                });

            modelBuilder.Entity("Kalabean.Domain.Entities.ShoppingCenterType", b =>
                {
                    b.Navigation("ShoppingCenters");

                    b.Navigation("Stores");
                });
#pragma warning restore 612, 618
        }
    }
}
