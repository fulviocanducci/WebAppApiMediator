﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebAppApi.DataAccess;

namespace WebAppApi.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20211010233336_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("WebAppApi.Models.Todo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("description");

                    b.Property<bool>("Done")
                        .HasColumnType("INTEGER")
                        .HasColumnName("done");

                    b.HasKey("Id");

                    b.ToTable("todos");
                });
#pragma warning restore 612, 618
        }
    }
}