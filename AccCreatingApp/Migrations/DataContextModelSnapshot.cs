﻿// <auto-generated />
using AccCreatingApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AccCreatingApp.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AccCreatingApp.Data.Account", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("IncidentName")
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Name");

                    b.HasIndex("IncidentName");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("AccCreatingApp.Data.Contact", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("AccountName")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Email");

                    b.HasIndex("AccountName");

                    b.ToTable("Contact");
                });

            modelBuilder.Entity("AccCreatingApp.Data.Incident", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(MAX)");

                    b.HasKey("Name");

                    b.ToTable("Incident");
                });

            modelBuilder.Entity("AccCreatingApp.Data.Account", b =>
                {
                    b.HasOne("AccCreatingApp.Data.Incident", "Incident")
                        .WithMany("Account")
                        .HasForeignKey("IncidentName");

                    b.Navigation("Incident");
                });

            modelBuilder.Entity("AccCreatingApp.Data.Contact", b =>
                {
                    b.HasOne("AccCreatingApp.Data.Account", "Account")
                        .WithMany("Contact")
                        .HasForeignKey("AccountName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("AccCreatingApp.Data.Account", b =>
                {
                    b.Navigation("Contact");
                });

            modelBuilder.Entity("AccCreatingApp.Data.Incident", b =>
                {
                    b.Navigation("Account");
                });
#pragma warning restore 612, 618
        }
    }
}