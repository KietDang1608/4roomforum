using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostService.Migrations
{
    /// <inheritdoc />
    public partial class PostReplyDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    thread_id = table.Column<int>(type: "int", nullable: false),
                    posted_by = table.Column<int>(type: "int", nullable: false),
                    like = table.Column<int>(type: "int", nullable: false),
                    post_content = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    post_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    is_edited = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Replies",
                columns: table => new
                {
                    reply_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    post_id = table.Column<int>(type: "int", nullable: false),
                    replied_by = table.Column<int>(type: "int", nullable: false),
                    reply_content = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    reply_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    upvote_amount = table.Column<int>(type: "int", nullable: false),
                    downvote_amount = table.Column<int>(type: "int", nullable: false),
                    reply_to_reply = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Replies", x => x.reply_id);
                    table.ForeignKey(
                        name: "FK_Replies_Posts_post_id",
                        column: x => x.post_id,
                        principalTable: "Posts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Replies_Replies_reply_to_reply",
                        column: x => x.reply_to_reply,
                        principalTable: "Replies",
                        principalColumn: "reply_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Replies_post_id",
                table: "Replies",
                column: "post_id");

            migrationBuilder.CreateIndex(
                name: "IX_Replies_reply_to_reply",
                table: "Replies",
                column: "reply_to_reply");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Replies");

            migrationBuilder.DropTable(
                name: "Posts");
        }
    }
}
