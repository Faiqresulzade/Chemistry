using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcces.Migrations
{
    public partial class addCategoryinVideoLesson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "VideoLessons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "VideoLessonCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoLessonCategory", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VideoLessons_CategoryID",
                table: "VideoLessons",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_VideoLessons_VideoLessonCategory_CategoryID",
                table: "VideoLessons",
                column: "CategoryID",
                principalTable: "VideoLessonCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VideoLessons_VideoLessonCategory_CategoryID",
                table: "VideoLessons");

            migrationBuilder.DropTable(
                name: "VideoLessonCategory");

            migrationBuilder.DropIndex(
                name: "IX_VideoLessons_CategoryID",
                table: "VideoLessons");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "VideoLessons");
        }
    }
}
