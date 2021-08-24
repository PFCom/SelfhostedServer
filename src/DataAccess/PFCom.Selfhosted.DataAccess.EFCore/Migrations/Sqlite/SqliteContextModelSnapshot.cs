﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PFCom.Selfhosted.DataAccess.EFCore.Providers;

namespace PFCom.Selfhosted.DataAccess.EFCore.Migrations.Sqlite
{
    [DbContext(typeof(SqliteContext))]
    partial class SqliteContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.9");

            modelBuilder.Entity("PFCom.Selfhosted.Core.Users.LocalUser", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordSalt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("LocalUser");
                });

            modelBuilder.Entity("PFCom.Selfhosted.Core.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nickname")
                        .HasColumnType("TEXT");

                    b.Property<string>("Type_str")
                        .HasColumnType("TEXT")
                        .HasColumnName("Type");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("PFCom.Selfhosted.Core.Users.LocalUser", b =>
                {
                    b.HasOne("PFCom.Selfhosted.Core.Users.User", "User")
                        .WithOne()
                        .HasForeignKey("PFCom.Selfhosted.Core.Users.LocalUser", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
