﻿// <auto-generated />
using System;
using AppointmentBooking.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AppointmentBooking.Migrations
{
    [DbContext(typeof(AppointmentDbContext))]
    [Migration("20210616083216_addToDatabse")]
    partial class addToDatabse
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AppointmentBooking.Models.AvailableTimes", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("InterviewerID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("InterviewerID");

                    b.ToTable("AvailableTimes");
                });

            modelBuilder.Entity("AppointmentBooking.Models.BookedAppointment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Dates")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("InterviewerID")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("InterviewerID");

                    b.ToTable("Appointment");
                });

            modelBuilder.Entity("AppointmentBooking.Models.InterviewerModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Interviewer");
                });

            modelBuilder.Entity("AppointmentBooking.Models.AvailableTimes", b =>
                {
                    b.HasOne("AppointmentBooking.Models.InterviewerModel", "Interviewer")
                        .WithMany("AvailableSlots")
                        .HasForeignKey("InterviewerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Interviewer");
                });

            modelBuilder.Entity("AppointmentBooking.Models.BookedAppointment", b =>
                {
                    b.HasOne("AppointmentBooking.Models.InterviewerModel", "Interviewer")
                        .WithMany("AppointmentList")
                        .HasForeignKey("InterviewerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Interviewer");
                });

            modelBuilder.Entity("AppointmentBooking.Models.InterviewerModel", b =>
                {
                    b.Navigation("AppointmentList");

                    b.Navigation("AvailableSlots");
                });
#pragma warning restore 612, 618
        }
    }
}