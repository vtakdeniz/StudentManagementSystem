﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudentManagementSystem.Data;

namespace StudentManagementSystem.Migrations
{
    [DbContext(typeof(ManagementContext))]
    [Migration("20211206134247_AddedSchool")]
    partial class AddedSchool
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("StudentManagementSystem.Models.Lecture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("classroom_code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lecture_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("lecturerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("lecturerId");

                    b.ToTable("lectures");
                });

            modelBuilder.Entity("StudentManagementSystem.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("age")
                        .HasColumnType("int");

                    b.Property<int>("class_year")
                        .HasColumnType("int");

                    b.Property<DateTime>("enrollment_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("first_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("last_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("school_number")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("students");
                });

            modelBuilder.Entity("StudentManagementSystem.Models.Student_has_lectures", b =>
                {
                    b.Property<int>("student_id")
                        .HasColumnType("int");

                    b.Property<int>("lecture_id")
                        .HasColumnType("int");

                    b.HasKey("student_id", "lecture_id");

                    b.HasIndex("lecture_id");

                    b.ToTable("student_Has_Lectures");
                });

            modelBuilder.Entity("StudentManagementSystem.Models.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("age")
                        .HasColumnType("int");

                    b.Property<string>("first_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("last_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("teachers");
                });

            modelBuilder.Entity("StudentManagementSystem.Models.Lecture", b =>
                {
                    b.HasOne("StudentManagementSystem.Models.Teacher", "lecturer")
                        .WithMany("lectures")
                        .HasForeignKey("lecturerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("lecturer");
                });

            modelBuilder.Entity("StudentManagementSystem.Models.Student_has_lectures", b =>
                {
                    b.HasOne("StudentManagementSystem.Models.Lecture", "lecture")
                        .WithMany("lecture_Has_Students")
                        .HasForeignKey("lecture_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentManagementSystem.Models.Student", "student")
                        .WithMany("student_Has_Lectures")
                        .HasForeignKey("student_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("lecture");

                    b.Navigation("student");
                });

            modelBuilder.Entity("StudentManagementSystem.Models.Lecture", b =>
                {
                    b.Navigation("lecture_Has_Students");
                });

            modelBuilder.Entity("StudentManagementSystem.Models.Student", b =>
                {
                    b.Navigation("student_Has_Lectures");
                });

            modelBuilder.Entity("StudentManagementSystem.Models.Teacher", b =>
                {
                    b.Navigation("lectures");
                });
#pragma warning restore 612, 618
        }
    }
}
