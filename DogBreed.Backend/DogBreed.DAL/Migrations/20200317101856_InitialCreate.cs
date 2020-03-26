using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DogBreed.DAL.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    DogImageId = table.Column<Guid>(nullable: false),
                    DogImageEntityId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PredictionResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PredictionResults_DogImages_DogImageEntityId",
                        column: x => x.DogImageEntityId,
                        principalTable: "DogImages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PredictionResults_DogImageEntityId",
                table: "PredictionResults",
                column: "DogImageEntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PredictionResults");

            migrationBuilder.DropTable(
                name: "DogImages");
        }
    }
}
