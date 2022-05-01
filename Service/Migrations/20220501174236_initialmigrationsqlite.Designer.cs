﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RequestService.Data;

#nullable disable

namespace RequestService.Migrations
{
    [DbContext(typeof(RequestsContext))]
    [Migration("20220501174236_initialmigrationsqlite")]
    partial class initialmigrationsqlite
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.4");

            modelBuilder.Entity("RequestService.Common.Models.Request", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("RequestDateTime")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RouteId")
                        .HasColumnType("TEXT");

                    b.Property<int>("SeatsCount")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RouteId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("RequestService.Common.Models.Route", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("Destination")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Origin")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("RequestService.Common.Models.Request", b =>
                {
                    b.HasOne("RequestService.Common.Models.Route", "Route")
                        .WithMany()
                        .HasForeignKey("RouteId");

                    b.Navigation("Route");
                });
#pragma warning restore 612, 618
        }
    }
}
