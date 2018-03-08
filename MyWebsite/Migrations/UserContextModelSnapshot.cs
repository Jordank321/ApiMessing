﻿// <auto-generated />
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace MyWebsite.Migrations
{
    [DbContext(typeof(UserContext))]
    partial class UserContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Database.AccessToken", b =>
                {
                    b.Property<int>("AccessTokenId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Expiry");

                    b.Property<string>("TokenString");

                    b.Property<int?>("UserId");

                    b.HasKey("AccessTokenId");

                    b.HasIndex("UserId");

                    b.ToTable("AccessTokens");
                });

            modelBuilder.Entity("Database.Nonce", b =>
                {
                    b.Property<int>("NonceId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Expiry");

                    b.Property<string>("NonceString");

                    b.Property<int?>("UserId");

                    b.HasKey("NonceId");

                    b.HasIndex("UserId");

                    b.ToTable("Nonces");
                });

            modelBuilder.Entity("Database.PasswordHash", b =>
                {
                    b.Property<int>("PasswordHashId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("HashString");

                    b.Property<int?>("UserId");

                    b.HasKey("PasswordHashId");

                    b.HasIndex("UserId");

                    b.ToTable("Hashes");
                });

            modelBuilder.Entity("Database.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Username");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Database.AccessToken", b =>
                {
                    b.HasOne("Database.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Database.Nonce", b =>
                {
                    b.HasOne("Database.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Database.PasswordHash", b =>
                {
                    b.HasOne("Database.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
