using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AssignmentOne.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedVideoGames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "VideoGames",
                columns: new[] { "Id", "CreateDate", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("a1000000-0000-0000-0000-000000000001"), new DateTime(2017, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "An open-world action-adventure game set in Hyrule.", "The Legend of Zelda: Breath of the Wild" },
                    { new Guid("a1000000-0000-0000-0000-000000000002"), new DateTime(2018, 10, 26, 0, 0, 0, 0, DateTimeKind.Utc), "An epic tale of life in America at the dawn of the modern age.", "Red Dead Redemption 2" },
                    { new Guid("a1000000-0000-0000-0000-000000000003"), new DateTime(2015, 5, 19, 0, 0, 0, 0, DateTimeKind.Utc), "A story-driven open world RPG set in a visually stunning fantasy universe.", "The Witcher 3: Wild Hunt" },
                    { new Guid("a1000000-0000-0000-0000-000000000004"), new DateTime(2016, 3, 24, 0, 0, 0, 0, DateTimeKind.Utc), "A challenging action RPG set in a dark, dying world.", "Dark Souls III" },
                    { new Guid("a1000000-0000-0000-0000-000000000005"), new DateTime(2017, 2, 24, 0, 0, 0, 0, DateTimeKind.Utc), "A challenging 2D action-adventure set in a beautifully hand-drawn insect kingdom.", "Hollow Knight" },
                    { new Guid("a1000000-0000-0000-0000-000000000006"), new DateTime(2022, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), "An action RPG set in the Lands Between, created with George R. R. Martin.", "Elden Ring" },
                    { new Guid("a1000000-0000-0000-0000-000000000007"), new DateTime(2018, 4, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Kratos and his son Atreus journey through the Norse realms.", "God of War" },
                    { new Guid("a1000000-0000-0000-0000-000000000008"), new DateTime(2020, 12, 10, 0, 0, 0, 0, DateTimeKind.Utc), "An open-world RPG set in the dystopian Night City.", "Cyberpunk 2077" },
                    { new Guid("a1000000-0000-0000-0000-000000000009"), new DateTime(2020, 9, 17, 0, 0, 0, 0, DateTimeKind.Utc), "A roguelike dungeon crawler where you battle out of the Underworld.", "Hades" },
                    { new Guid("a1000000-0000-0000-0000-000000000010"), new DateTime(2019, 3, 22, 0, 0, 0, 0, DateTimeKind.Utc), "A shinobi action game set in late 1500s Sengoku Japan.", "Sekiro: Shadows Die Twice" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "VideoGames",
                keyColumn: "Id",
                keyValue: new Guid("a1000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "VideoGames",
                keyColumn: "Id",
                keyValue: new Guid("a1000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "VideoGames",
                keyColumn: "Id",
                keyValue: new Guid("a1000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "VideoGames",
                keyColumn: "Id",
                keyValue: new Guid("a1000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "VideoGames",
                keyColumn: "Id",
                keyValue: new Guid("a1000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "VideoGames",
                keyColumn: "Id",
                keyValue: new Guid("a1000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "VideoGames",
                keyColumn: "Id",
                keyValue: new Guid("a1000000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "VideoGames",
                keyColumn: "Id",
                keyValue: new Guid("a1000000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "VideoGames",
                keyColumn: "Id",
                keyValue: new Guid("a1000000-0000-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                table: "VideoGames",
                keyColumn: "Id",
                keyValue: new Guid("a1000000-0000-0000-0000-000000000010"));
        }
    }
}
