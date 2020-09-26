using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace TestTaskLibrary.Infrastructure.Data.Migrations
{
    public partial class ResolveError : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BookRatingComments",
                table: "BookRatingComments");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "BookRatingComments",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookRatingComments",
                table: "BookRatingComments",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_BookRatingComments_BookAdditionalInfoId",
                table: "BookRatingComments",
                column: "BookAdditionalInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BookRatingComments",
                table: "BookRatingComments");

            migrationBuilder.DropIndex(
                name: "IX_BookRatingComments_BookAdditionalInfoId",
                table: "BookRatingComments");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BookRatingComments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookRatingComments",
                table: "BookRatingComments",
                column: "BookAdditionalInfoId");
        }
    }
}
