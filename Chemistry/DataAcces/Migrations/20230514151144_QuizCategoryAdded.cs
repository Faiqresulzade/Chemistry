using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcces.Migrations
{
    public partial class QuizCategoryAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_QuizCategory_QuizCategoryId",
                table: "Quizzes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizCategory",
                table: "QuizCategory");

            migrationBuilder.RenameTable(
                name: "QuizCategory",
                newName: "QuizCategories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizCategories",
                table: "QuizCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_QuizCategories_QuizCategoryId",
                table: "Quizzes",
                column: "QuizCategoryId",
                principalTable: "QuizCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_QuizCategories_QuizCategoryId",
                table: "Quizzes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizCategories",
                table: "QuizCategories");

            migrationBuilder.RenameTable(
                name: "QuizCategories",
                newName: "QuizCategory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizCategory",
                table: "QuizCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_QuizCategory_QuizCategoryId",
                table: "Quizzes",
                column: "QuizCategoryId",
                principalTable: "QuizCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
