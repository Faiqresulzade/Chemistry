using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcces.Migrations
{
    public partial class AddedVideoLessonCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoLessons_VideoLessonCategory_CategoryID",
                table: "VideoLessons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VideoLessonCategory",
                table: "VideoLessonCategory");

            migrationBuilder.RenameTable(
                name: "VideoLessonCategory",
                newName: "VideoLessonCategories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VideoLessonCategories",
                table: "VideoLessonCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VideoLessons_VideoLessonCategories_CategoryID",
                table: "VideoLessons",
                column: "CategoryID",
                principalTable: "VideoLessonCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoLessons_VideoLessonCategories_CategoryID",
                table: "VideoLessons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VideoLessonCategories",
                table: "VideoLessonCategories");

            migrationBuilder.RenameTable(
                name: "VideoLessonCategories",
                newName: "VideoLessonCategory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VideoLessonCategory",
                table: "VideoLessonCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VideoLessons_VideoLessonCategory_CategoryID",
                table: "VideoLessons",
                column: "CategoryID",
                principalTable: "VideoLessonCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
