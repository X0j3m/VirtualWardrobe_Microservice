using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualWardrobe_ClothesTypes.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClothesLayers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothesLayers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClothesTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothesTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClothesTypes_ClothesLayers_LayerId",
                        column: x => x.LayerId,
                        principalTable: "ClothesLayers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClothesLayers_Name",
                table: "ClothesLayers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClothesTypes_LayerId",
                table: "ClothesTypes",
                column: "LayerId");

            migrationBuilder.CreateIndex(
                name: "IX_ClothesTypes_Name",
                table: "ClothesTypes",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClothesTypes");

            migrationBuilder.DropTable(
                name: "ClothesLayers");
        }
    }
}
