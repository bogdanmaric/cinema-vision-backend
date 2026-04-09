using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieApi.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueConstraintToFavoriteMovie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FavoriteMovies_UserId",
                table: "FavoriteMovies");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteMovies_UserId_MovieId",
                table: "FavoriteMovies",
                columns: new[] { "UserId", "MovieId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FavoriteMovies_UserId_MovieId",
                table: "FavoriteMovies");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteMovies_UserId",
                table: "FavoriteMovies",
                column: "UserId");
        }
    }
}
