using Microsoft.EntityFrameworkCore.Migrations;

namespace TestTaskLibrary.Infrastructure.Data.Migrations
{
    public partial class ChangeRelationship2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BookStatuses_UserId",
                table: "BookStatuses");

            migrationBuilder.CreateIndex(
                name: "IX_BookStatuses_UserId",
                table: "BookStatuses",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BookStatuses_UserId",
                table: "BookStatuses");

            migrationBuilder.CreateIndex(
                name: "IX_BookStatuses_UserId",
                table: "BookStatuses",
                column: "UserId",
                unique: true);
        }
    }
}
