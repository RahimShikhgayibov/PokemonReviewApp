using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonReviewApp.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Birthdate",
                table: "Pokemons",
                newName: "BirthDate");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Owners",
                newName: "LastName");

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Reviews",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Owners",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Owners");

            migrationBuilder.RenameColumn(
                name: "BirthDate",
                table: "Pokemons",
                newName: "Birthdate");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Owners",
                newName: "Name");
        }
    }
}
