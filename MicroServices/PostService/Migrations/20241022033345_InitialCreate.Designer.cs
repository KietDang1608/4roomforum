﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PostService.Data;

#nullable disable

namespace PostService.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20241022033345_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("PostService.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsEdited")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Like")
                        .HasColumnType("int");

                    b.Property<string>("PostContent")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("PostDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("PostedBy")
                        .HasColumnType("int");

                    b.Property<int>("ThreadId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("PostService.Models.Reply", b =>
                {
                    b.Property<int>("ReplyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("reply_id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("ReplyId"));

                    b.Property<int>("DownvoteAmount")
                        .HasColumnType("int")
                        .HasColumnName("downvote_amount");

                    b.Property<int>("PostId")
                        .HasColumnType("int")
                        .HasColumnName("post_id");

                    b.Property<int>("ReplyBy")
                        .HasColumnType("int")
                        .HasColumnName("reply_by");

                    b.Property<string>("ReplyContent")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("reply_content");

                    b.Property<DateTime>("ReplyDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("reply_date");

                    b.Property<int?>("ReplyToReply")
                        .HasColumnType("int")
                        .HasColumnName("reply_to_reply");

                    b.Property<int>("UpvoteAmount")
                        .HasColumnType("int")
                        .HasColumnName("upvote_amount");

                    b.HasKey("ReplyId");

                    b.ToTable("Replies");
                });
#pragma warning restore 612, 618
        }
    }
}