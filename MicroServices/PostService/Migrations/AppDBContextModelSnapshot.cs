﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PostService.Data;

#nullable disable

namespace PostService.Migrations
{
    [DbContext(typeof(AppDBContext))]
    partial class AppDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("PostService.Models.LikeOfPost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PostId")
                        .HasColumnType("int")
                        .HasColumnName("post_id");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("LikeOfPosts");
                });

            modelBuilder.Entity("PostService.Models.LikeOfReply", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ReplyId")
                        .HasColumnType("int")
                        .HasColumnName("reply_id");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.Property<int>("vote")
                        .HasColumnType("int")
                        .HasColumnName("vote");

                    b.HasKey("Id");

                    b.HasIndex("ReplyId");

                    b.ToTable("LikeOfReplies");
                });

            modelBuilder.Entity("PostService.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsEdited")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_edited");

                    b.Property<int>("Like")
                        .HasColumnType("int")
                        .HasColumnName("like");

                    b.Property<string>("PostContent")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("post_content");

                    b.Property<DateTime>("PostDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("post_date");

                    b.Property<int>("PostedBy")
                        .HasColumnType("int")
                        .HasColumnName("posted_by");

                    b.Property<int>("ThreadId")
                        .HasColumnType("int")
                        .HasColumnName("thread_id");

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

                    b.Property<int>("RepliedBy")
                        .HasColumnType("int")
                        .HasColumnName("replied_by");

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

                    b.HasIndex("PostId");

                    b.HasIndex("ReplyToReply");

                    b.ToTable("Replies");
                });

            modelBuilder.Entity("PostService.Models.LikeOfPost", b =>
                {
                    b.HasOne("PostService.Models.Post", "Post")
                        .WithMany("Likes")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");
                });

            modelBuilder.Entity("PostService.Models.LikeOfReply", b =>
                {
                    b.HasOne("PostService.Models.Reply", "Reply")
                        .WithMany("Likes")
                        .HasForeignKey("ReplyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reply");
                });

            modelBuilder.Entity("PostService.Models.Reply", b =>
                {
                    b.HasOne("PostService.Models.Post", "Post")
                        .WithMany("Replies")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PostService.Models.Reply", "ReplyToReply2")
                        .WithMany("ReplyToReplies")
                        .HasForeignKey("ReplyToReply")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Post");

                    b.Navigation("ReplyToReply2");
                });

            modelBuilder.Entity("PostService.Models.Post", b =>
                {
                    b.Navigation("Likes");

                    b.Navigation("Replies");
                });

            modelBuilder.Entity("PostService.Models.Reply", b =>
                {
                    b.Navigation("Likes");

                    b.Navigation("ReplyToReplies");
                });
#pragma warning restore 612, 618
        }
    }
}
