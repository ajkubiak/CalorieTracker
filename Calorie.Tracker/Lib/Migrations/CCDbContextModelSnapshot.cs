﻿// <auto-generated />
using System;
using Lib.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Lib.Migrations
{
    [DbContext(typeof(CCDbContext))]
    partial class CCDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Lib.Models.Auth.User", b =>
                {
                    b.Property<string>("Username")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20);

                    b.Property<string>("Email");

                    b.Property<string>("Role");

                    b.HasKey("Username");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Lib.Models.Auth.UserLogin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("Username");

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("Lib.Models.Diary.DiaryEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EntryDate")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("OwnedById")
                        .IsRequired();

                    b.Property<DateTime>("Updated")
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("OwnedById");

                    b.ToTable("DiaryEntries");
                });

            modelBuilder.Entity("Lib.Models.Diary.FoodItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("Carbohydrates");

                    b.Property<float>("Fat");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("OwnedById")
                        .IsRequired();

                    b.Property<float>("Protein");

                    b.HasKey("Id");

                    b.HasIndex("OwnedById");

                    b.ToTable("FoodItems");
                });

            modelBuilder.Entity("Lib.Models.Diary.FoodItemMeal", b =>
                {
                    b.Property<Guid>("MealId");

                    b.Property<Guid>("FoodItemId");

                    b.HasKey("MealId", "FoodItemId");

                    b.HasIndex("FoodItemId");

                    b.ToTable("FoodItemMeal");
                });

            modelBuilder.Entity("Lib.Models.Diary.Meal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("DiaryEntryId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<long>("Order");

                    b.Property<string>("OwnedById")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("DiaryEntryId");

                    b.HasIndex("OwnedById");

                    b.ToTable("Meals");
                });

            modelBuilder.Entity("Lib.Models.Auth.UserLogin", b =>
                {
                    b.HasOne("Lib.Models.Auth.User", "User")
                        .WithMany()
                        .HasForeignKey("Username")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Lib.Models.Diary.FoodItemMeal", b =>
                {
                    b.HasOne("Lib.Models.Diary.FoodItem", "FoodItem")
                        .WithMany("MealLinks")
                        .HasForeignKey("FoodItemId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Lib.Models.Diary.Meal", "Meal")
                        .WithMany("FoodItemLinks")
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Lib.Models.Diary.Meal", b =>
                {
                    b.HasOne("Lib.Models.Diary.DiaryEntry")
                        .WithMany("MealGroupings")
                        .HasForeignKey("DiaryEntryId");
                });
#pragma warning restore 612, 618
        }
    }
}