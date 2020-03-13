using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DogBreed.DAL.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DogBreeds",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Abrv = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DogBreeds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DogImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Abrv = table.Column<string>(nullable: true),
                    Score = table.Column<float>(nullable: false),
                    File = table.Column<byte[]>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DogImages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PredictionResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Abrv = table.Column<string>(nullable: true),
                    Score = table.Column<float>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateUpdated = table.Column<DateTime>(nullable: false),
                    DogImageId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PredictionResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PredictionResults_DogImages_DogImageId",
                        column: x => x.DogImageId,
                        principalTable: "DogImages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PredictionResults_DogImageId",
                table: "PredictionResults",
                column: "DogImageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DogBreeds");

            migrationBuilder.DropTable(
                name: "PredictionResults");

            migrationBuilder.DropTable(
                name: "DogImages");
        }
    }
}
