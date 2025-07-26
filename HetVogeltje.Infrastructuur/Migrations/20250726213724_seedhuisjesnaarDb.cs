using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HetVogeltje.Infrastructuur.Migrations
{
    /// <inheritdoc />
    public partial class seedhuisjesnaarDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "CreatedDate", "Description", "ImagePath", "Name", "Occupancy", "Price", "Sqft", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, null, "Villa het Vogeltje, is echt vogeltje", "https://placehold.co/600x400", "Vogeltje Villa", 6, 250.0, 1500, null },
                    { 2, null, "Zon Villa, is echt zon", "https://placehold.co/600x401", "Zon Villa", 8, 300.0, 1800, null },
                    { 3, null, "Ster Villa, is echt ster", "https://placehold.co/600x402", "Ster Villa", 10, 350.0, 2000, null },
                    { 4, null, "Maan Villa, is echt maan", "https://placehold.co/600x403", "Maan Villa", 12, 400.0, 2200, null },
                    { 5, null, "Budget Villa, voor de minder bedeelden onder ons", "https://placehold.co/600x404", "Budget Villa", 3, 150.0, 100, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
