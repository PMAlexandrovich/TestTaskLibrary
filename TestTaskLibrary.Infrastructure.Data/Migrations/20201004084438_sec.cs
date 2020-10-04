using Microsoft.EntityFrameworkCore.Migrations;

namespace TestTaskLibrary.Infrastructure.Data.Migrations
{
    public partial class sec : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookStatuses_Books_BookId",
                table: "BookStatuses");

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

            migrationBuilder.AddForeignKey(
                name: "FK_BookStatuses_Books_BookId",
                table: "BookStatuses",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
