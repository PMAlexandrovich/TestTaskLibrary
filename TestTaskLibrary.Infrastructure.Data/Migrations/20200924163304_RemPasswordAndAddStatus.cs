using Microsoft.EntityFrameworkCore.Migrations;

namespace TestTaskLibrary.Infrastructure.Data.Migrations
{
    public partial class RemPasswordAndAddStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBooked",
                table: "BookStatuses");

            migrationBuilder.DropColumn(
                name: "IsIssued",
                table: "BookStatuses");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "BookStatuses",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "BookStatuses");

            migrationBuilder.AddColumn<bool>(
                name: "IsBooked",
                table: "BookStatuses",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsIssued",
                table: "BookStatuses",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
