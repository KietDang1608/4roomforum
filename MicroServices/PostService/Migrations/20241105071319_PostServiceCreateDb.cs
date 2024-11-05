using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostService.Migrations
{
    /// <inheritdoc />
    public partial class PostServiceCreateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "reply_by",
                table: "Replies",
                newName: "replied_by");

            migrationBuilder.RenameColumn(
                name: "Like",
                table: "Posts",
                newName: "like");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Posts",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ThreadId",
                table: "Posts",
                newName: "thread_id");

            migrationBuilder.RenameColumn(
                name: "PostedBy",
                table: "Posts",
                newName: "posted_by");

            migrationBuilder.RenameColumn(
                name: "PostDate",
                table: "Posts",
                newName: "post_date");

            migrationBuilder.RenameColumn(
                name: "PostContent",
                table: "Posts",
                newName: "post_content");

            migrationBuilder.RenameColumn(
                name: "IsEdited",
                table: "Posts",
                newName: "is_edited");

            migrationBuilder.CreateIndex(
                name: "IX_Replies_post_id",
                table: "Replies",
                column: "post_id");

            migrationBuilder.CreateIndex(
                name: "IX_Replies_reply_to_reply",
                table: "Replies",
                column: "reply_to_reply");

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Posts_post_id",
                table: "Replies",
                column: "post_id",
                principalTable: "Posts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Replies_reply_to_reply",
                table: "Replies",
                column: "reply_to_reply",
                principalTable: "Replies",
                principalColumn: "reply_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Posts_post_id",
                table: "Replies");

            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Replies_reply_to_reply",
                table: "Replies");

            migrationBuilder.DropIndex(
                name: "IX_Replies_post_id",
                table: "Replies");

            migrationBuilder.DropIndex(
                name: "IX_Replies_reply_to_reply",
                table: "Replies");

            migrationBuilder.RenameColumn(
                name: "replied_by",
                table: "Replies",
                newName: "reply_by");

            migrationBuilder.RenameColumn(
                name: "like",
                table: "Posts",
                newName: "Like");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Posts",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "thread_id",
                table: "Posts",
                newName: "ThreadId");

            migrationBuilder.RenameColumn(
                name: "posted_by",
                table: "Posts",
                newName: "PostedBy");

            migrationBuilder.RenameColumn(
                name: "post_date",
                table: "Posts",
                newName: "PostDate");

            migrationBuilder.RenameColumn(
                name: "post_content",
                table: "Posts",
                newName: "PostContent");

            migrationBuilder.RenameColumn(
                name: "is_edited",
                table: "Posts",
                newName: "IsEdited");
        }
    }
}
