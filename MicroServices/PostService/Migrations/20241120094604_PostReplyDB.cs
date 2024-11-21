using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostService.Migrations
{
    /// <inheritdoc />
    public partial class PostReplyDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:AutoIncrement", true),  // Auto increment
                    thread_id = table.Column<int>(type: "int", nullable: false),
                    posted_by = table.Column<int>(type: "int", nullable: false),
                    like = table.Column<int>(type: "int", nullable: false),
                    post_content = table.Column<string>(type: "longtext", nullable: false),
                    post_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    is_edited = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.id);  // Primary Key with auto-increment
                });

            migrationBuilder.CreateTable(
                name: "Replies",
                columns: table => new
                {
                    reply_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:AutoIncrement", true),  // Auto increment
                    post_id = table.Column<int>(type: "int", nullable: false),
                    replied_by = table.Column<int>(type: "int", nullable: false),
                    reply_content = table.Column<string>(type: "longtext", nullable: false),
                    reply_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    upvote_amount = table.Column<int>(type: "int", nullable: false),
                    downvote_amount = table.Column<int>(type: "int", nullable: false),
                    reply_to_reply = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_replies", x => x.reply_id);  // Primary Key with auto-increment
                    table.ForeignKey(
                        name: "FK_replies_posts_post_id",
                        column: x => x.post_id,
                        principalTable: "Posts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_replies_replies_reply_to_reply",
                        column: x => x.reply_to_reply,
                        principalTable: "Replies",
                        principalColumn: "reply_id",  // Corrected reference to reply_id for self-join
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Like_of_post",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:AutoIncrement", true),  // Auto increment
                    post_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_like_of_post", x => x.id);  // Primary Key with auto-increment
                    table.ForeignKey(
                        name: "FK_like_of_post_posts_post_id",
                        column: x => x.post_id,
                        principalTable: "Posts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Like_of_reply",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:AutoIncrement", true),  // Auto increment
                    vote = table.Column<int>(type: "int", nullable: false),
                    reply_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_like_of_reply", x => x.id);  // Primary Key with auto-increment
                    table.ForeignKey(
                        name: "FK_like_of_reply_replies_reply_id",
                        column: x => x.reply_id,
                        principalTable: "Replies",
                        principalColumn: "reply_id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Like_of_post");

            migrationBuilder.DropTable(
                name: "Like_of_reply");

            migrationBuilder.DropTable(
                name: "Replies");

            migrationBuilder.DropTable(
                name: "Posts");
        }
    }
}
