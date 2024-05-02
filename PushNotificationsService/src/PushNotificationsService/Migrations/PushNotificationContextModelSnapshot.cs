﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PushNotificationsService.DbContextConfig;

#nullable disable

namespace PushNotificationsService.Migrations
{
    [DbContext(typeof(PushNotificationContext))]
    partial class PushNotificationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PushNotificationsService.DeviceInfo.Entities.DeviceInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("customerDeviceEndpoint")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("customerId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("DeviceInfos");
                });
#pragma warning restore 612, 618
        }
    }
}
