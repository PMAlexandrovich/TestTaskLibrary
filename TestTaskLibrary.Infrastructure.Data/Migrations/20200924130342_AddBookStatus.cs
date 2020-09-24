using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace TestTaskLibrary.Infrastructure.Data.Migrations
{
    public partial class AddBookStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBooked",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "IsIssued",
                table: "Books");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Books",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Genre",
                table: "Books",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Author",
                table: "Books",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BookStatusId",
                table: "Books",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BookStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(nullable: false),
                    IsBooked = table.Column<bool>(nullable: false),
                    IsIssued = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookStatuses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookStatusId",
                table: "Books",
                column: "BookStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_BookStatuses_UserId",
                table: "BookStatuses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookStatuses_BookStatusId",
                table: "Books",
                column: "BookStatusId",
                principalTable: "BookStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookStatuses_BookStatusId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "BookStatuses");

            migrationBuilder.DropIndex(
                name: "IX_Books_BookStatusId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "BookStatusId",
                table: "Books");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Books",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Genre",
                table: "Books",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Author",
                table: "Books",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<bool>(
                name: "IsBooked",
                table: "Books",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsIssued",
                table: "Books",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
