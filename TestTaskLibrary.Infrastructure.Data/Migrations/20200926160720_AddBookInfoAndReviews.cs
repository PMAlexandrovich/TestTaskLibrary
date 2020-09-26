using Microsoft.EntityFrameworkCore.Migrations;

namespace TestTaskLibrary.Infrastructure.Data.Migrations
{
    public partial class AddBookInfoAndReviews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookAdditionalInfos",
                columns: table => new
                {
                    BookId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookAdditionalInfos", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_BookAdditionalInfos_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookRatingComments",
                columns: table => new
                {
                    BookAdditionalInfoId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Rating = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookRatingComments", x => x.BookAdditionalInfoId);
                    table.ForeignKey(
                        name: "FK_BookRatingComments_BookAdditionalInfos_BookAdditionalInfoId",
                        column: x => x.BookAdditionalInfoId,
                        principalTable: "BookAdditionalInfos",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookRatingComments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookRatingComments_UserId",
                table: "BookRatingComments",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookRatingComments");

            migrationBuilder.DropTable(
                name: "BookAdditionalInfos");
        }
    }
}
