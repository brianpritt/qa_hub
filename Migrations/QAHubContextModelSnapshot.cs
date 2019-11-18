﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QAHub.Models;

namespace QAHub.Migrations
{
    [DbContext(typeof(QAHubContext))]
    partial class QAHubContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("QAHub.Models.Ticket", b =>
                {
                    b.Property<int>("TicketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("TicketAuthor")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("TicketBody")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("TicketCategory")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("TicketTitle")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("TicketId");

                    b.ToTable("Tickets");
                });
#pragma warning restore 612, 618
        }
    }
}
