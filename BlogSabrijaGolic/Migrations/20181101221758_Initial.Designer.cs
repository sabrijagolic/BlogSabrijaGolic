﻿// <auto-generated />
using System;
using BlogSabrijaGolic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BlogSabrijaGolic.Migrations
{
    [DbContext(typeof(BlogPostContext))]
    [Migration("20181101221758_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024");

            modelBuilder.Entity("BlogSabrijaGolic.Models.BlogPost", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body");

                    b.Property<DateTime>("CratedAt");

                    b.Property<string>("Description");

                    b.Property<string>("Slug");

                    b.Property<string>("Title");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("ID");

                    b.ToTable("BlogPost");
                });

            modelBuilder.Entity("BlogSabrijaGolic.Models.BlogPostTag", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BlogPostID");

                    b.Property<int>("TagID");

                    b.HasKey("ID");

                    b.HasIndex("BlogPostID");

                    b.HasIndex("TagID");

                    b.ToTable("BlogPostTag");
                });

            modelBuilder.Entity("BlogSabrijaGolic.Models.Tag", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("BlogSabrijaGolic.Models.BlogPostTag", b =>
                {
                    b.HasOne("BlogSabrijaGolic.Models.BlogPost", "BlogPost")
                        .WithMany("tagList")
                        .HasForeignKey("BlogPostID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BlogSabrijaGolic.Models.Tag", "Tag")
                        .WithMany("BlogPosts")
                        .HasForeignKey("TagID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}