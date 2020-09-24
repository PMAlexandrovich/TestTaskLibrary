using Microsoft.EntityFrameworkCore.Migrations;

namespace TestTaskLibrary.Infrastructure.Data.Migrations
{
    public partial class BookStatusUserNotRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookStatuses_AspNetUsers_UserId",
                table: "BookStatuses");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "BookStatuses",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_BookStatuses_AspNetUsers_UserId",
                table: "BookStatuses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookStatuses_AspNetUsers_UserId",
                table: "BookStatuses");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "BookStatuses",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookStatuses_AspNetUsers_UserId",
                table: "BookStatuses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
