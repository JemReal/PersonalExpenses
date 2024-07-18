﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PersonalExpenses.API.Data;

#nullable disable

namespace PersonalExpenses.API.Migrations
{
    [DbContext(typeof(PersonalExpensesDbContext))]
    [Migration("20240717234821_Seeding Data for Frequencies and Categories")]
    partial class SeedingDataforFrequenciesandCategories
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PersonalExpenses.API.Models.Domain.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Abbr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CategoyImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = new Guid("9aa916c3-97f4-4906-9214-6755ee0023ff"),
                            Abbr = "UTILS",
                            CategoyImageUrl = "https://images.pexels.com/photos/2898199/pexels-photo-2898199.jpeg",
                            Name = "Utilities"
                        },
                        new
                        {
                            Id = new Guid("d880b23f-8fab-4b1a-af6f-1a2603b266ed"),
                            Abbr = "TRANSP",
                            CategoyImageUrl = "https://images.pexels.com/photos/210182/pexels-photo-210182.jpeg",
                            Name = "Transportation"
                        },
                        new
                        {
                            Id = new Guid("c4dd6eb2-51c9-4174-86ba-3a56cbe9a05f"),
                            Abbr = "FOOD",
                            CategoyImageUrl = "https://www.food-safety.com/ext/resources/fsm/cache/file/26EC8DA4-CFD1-4437-897750814E836EBE.png",
                            Name = "Food"
                        },
                        new
                        {
                            Id = new Guid("1083f4c1-51a5-4506-934d-23145054fd7b"),
                            Abbr = "CLOTH",
                            CategoyImageUrl = "https://images.pexels.com/photos/3812433/pexels-photo-3812433.jpeg",
                            Name = "Clothing"
                        });
                });

            modelBuilder.Entity("PersonalExpenses.API.Models.Domain.Expense", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExpenseImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("FrequencyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Quantity")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("FrequencyId");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("PersonalExpenses.API.Models.Domain.Frequency", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Frequencies");

                    b.HasData(
                        new
                        {
                            Id = new Guid("24413659-47c5-4fba-8ace-6ef37060b9e5"),
                            Name = "Daily"
                        },
                        new
                        {
                            Id = new Guid("1b358da8-c0d7-4e6a-933c-a7568a687a5d"),
                            Name = "Weekly"
                        },
                        new
                        {
                            Id = new Guid("341bb587-72ed-4231-85c7-82c436080fbc"),
                            Name = "Monthly"
                        });
                });

            modelBuilder.Entity("PersonalExpenses.API.Models.Domain.Expense", b =>
                {
                    b.HasOne("PersonalExpenses.API.Models.Domain.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PersonalExpenses.API.Models.Domain.Frequency", "Frequency")
                        .WithMany()
                        .HasForeignKey("FrequencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Frequency");
                });
#pragma warning restore 612, 618
        }
    }
}
