using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HetVogeltje.Infrastructuur.Migrations
{
    /// <inheritdoc />
    public partial class VoorzieningenToevoeging : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Voorzieningen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpecialeDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VillaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voorzieningen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Voorzieningen_Villas_VillaId",
                        column: x => x.VillaId,
                        principalTable: "Villas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "VillaNumbers",
                keyColumn: "Villa_Number",
                keyValue: 105,
                column: "VillaId",
                value: 3);

            migrationBuilder.InsertData(
                table: "Voorzieningen",
                columns: new[] { "Id", "Naam", "SpecialeDetails", "VillaId" },
                values: new object[,]
                {
                    { 1, "Zwembad", "Groot zwembad", 1 },
                    { 2, "Tennisbaan", "Buiten tennisbaan", 2 },
                    { 3, "Fitnessruimte", "Volledig uitgeruste fitnessruimte", 3 },
                    { 4, "Spa", "Ontspannende spa faciliteiten", 4 },
                    { 5, "Barbecueplaats", "Buiten barbecueplaats", 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Voorzieningen_VillaId",
                table: "Voorzieningen",
                column: "VillaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Voorzieningen");

            migrationBuilder.UpdateData(
                table: "VillaNumbers",
                keyColumn: "Villa_Number",
                keyValue: 105,
                column: "VillaId",
                value: 5);
        }
    }
}
