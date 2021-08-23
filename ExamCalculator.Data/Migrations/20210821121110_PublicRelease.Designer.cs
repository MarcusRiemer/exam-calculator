﻿// <auto-generated />
using System;
using ExamCalculator.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ExamCalculator.Data.Migrations
{
    [DbContext(typeof(ApplicationDataContext))]
    [Migration("20210821121110_PublicRelease")]
    partial class PublicRelease
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.9");

            modelBuilder.Entity("ExamCalculator.Data.Group", b =>
                {
                    b.Property<Guid>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("GroupId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("ExamCalculator.Data.Pupil", b =>
                {
                    b.Property<Guid>("PupilId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.HasKey("PupilId");

                    b.ToTable("Pupils");
                });

            modelBuilder.Entity("GroupPupil", b =>
                {
                    b.Property<Guid>("GroupsGroupId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PupilsPupilId")
                        .HasColumnType("TEXT");

                    b.HasKey("GroupsGroupId", "PupilsPupilId");

                    b.HasIndex("PupilsPupilId");

                    b.ToTable("GroupPupil");
                });

            modelBuilder.Entity("GroupPupil", b =>
                {
                    b.HasOne("ExamCalculator.Data.Group", null)
                        .WithMany()
                        .HasForeignKey("GroupsGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExamCalculator.Data.Pupil", null)
                        .WithMany()
                        .HasForeignKey("PupilsPupilId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}