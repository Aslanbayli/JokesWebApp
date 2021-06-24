using Microsoft.EntityFrameworkCore.Migrations;

namespace JokesWebApp.Data.Migrations
{
    public partial class RelationsForUserAndJoke : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Joke",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Joke_UserId",
                table: "Joke",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Joke_AspNetUsers_UserId",
                table: "Joke",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Joke_AspNetUsers_UserId",
                table: "Joke");

            migrationBuilder.DropIndex(
                name: "IX_Joke_UserId",
                table: "Joke");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Joke");
        }
    }
}
