using Microsoft.EntityFrameworkCore.Migrations;

namespace TestTaskLibrary.Infrastructure.Data.Migrations
{
    public partial class ChangeRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookStatuses_BookStatusId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_BookStatuses_UserId",
                table: "BookStatuses");

            migrationBuilder.DropIndex(
                name: "IX_Books_BookStatusId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "BookStatusId",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "BookStatuses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BookStatuses_BookId",
                table: "BookStatuses",
                column: "BookId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookStatuses_UserId",
                table: "BookStatuses",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookStatuses_Books_BookId",
                table: "BookStatuses",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookStatuses_Books_BookId",
                table: "BookStatuses");

            migrationBuilder.DropIndex(
                name: "IX_BookStatuses_BookId",
                table: "BookStatuses");

            migrationBuilder.DropIndex(
                name: "IX_BookStatuses_UserId",
                table: "BookStatuses");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "BookStatuses");

            migrationBuilder.AddColumn<int>(
                name: "BookStatusId",
                table: "Books",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BookStatuses_UserId",
                table: "BookStatuses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookStatusId",
                table: "Books",
                column: "BookStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookStatuses_BookStatusId",
                table: "Books",
                column: "BookStatusId",
                principalTable: "BookStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
