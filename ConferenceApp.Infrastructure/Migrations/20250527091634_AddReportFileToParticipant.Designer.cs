﻿// <auto-generated />
using System;
using ConferenceApp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ConferenceApp.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250527091634_AddReportFileToParticipant")]
    partial class AddReportFileToParticipant
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.5");

            modelBuilder.Entity("ConferenceApp.Domain.Entities.Conference", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Conferences");
                });

            modelBuilder.Entity("ConferenceApp.Domain.Entities.Participant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ConferenceId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Organization")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("ReportFile")
                        .HasColumnType("BLOB");

                    b.Property<string>("ReportFileName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TitleLecture")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ConferenceId");

                    b.ToTable("Participants");
                });

            modelBuilder.Entity("ConferenceApp.Domain.Entities.Participant", b =>
                {
                    b.HasOne("ConferenceApp.Domain.Entities.Conference", null)
                        .WithMany("Participants")
                        .HasForeignKey("ConferenceId");
                });

            modelBuilder.Entity("ConferenceApp.Domain.Entities.Conference", b =>
                {
                    b.Navigation("Participants");
                });
#pragma warning restore 612, 618
        }
    }
}
