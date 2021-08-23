﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PFCom.Selfhosted.DataAccess.EFCore.Providers;

namespace PFCom.Selfhosted.DataAccess.EFCore.Migrations.Mysql
{
    [DbContext(typeof(MysqlContext))]
    partial class MysqlContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.9");

            modelBuilder.Entity("PFCom.Selfhosted.Core.Users.LocalUser", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<string>("PasswordSalt")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("LocalUser");
                });

            modelBuilder.Entity("PFCom.Selfhosted.Core.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Nickname")
                        .HasColumnType("longtext");

                    b.Property<string>("Type_str")
                        .HasColumnType("longtext")
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
