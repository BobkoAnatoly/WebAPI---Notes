﻿// <auto-generated />
using System;
using Application.Model.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Application.Migrations
{
    [DbContext(typeof(ApplicationDatabaseContext))]
    [Migration("20220731160444_AddRefreshToken")]
    partial class AddRefreshToken
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Application.Model.Models.RefreshToken", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ValidFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ValidTo")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("Application.Model.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<Guid>("RefreshTokenId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Application.Model.Models.RefreshToken", b =>
                {
                    b.HasOne("Application.Model.Models.User", "User")
                        .WithOne("RefreshToken")
                        .HasForeignKey("Application.Model.Models.RefreshToken", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Application.Model.Models.User", b =>
                {
                    b.Navigation("RefreshToken")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
