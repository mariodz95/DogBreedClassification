using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DogBreed.DAL.Migrations
{
    public partial class UpdateDogImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PredictionResults_DogImages_DogImageEntityId",
                table: "PredictionResults");

            migrationBuilder.DropIndex(
                name: "IX_PredictionResults_DogImageEntityId",
                table: "PredictionResults");

            migrationBuilder.DropColumn(
                name: "DogImageEntityId",
                table: "PredictionResults");

            migrationBuilder.AddColumn<Guid>(
                name: "PredictionResultsId",
                table: "DogImages",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DogImages_PredictionResultsId",
                table: "DogImages",
                column: "PredictionResultsId");

            migrationBuilder.AddForeignKey(
                name: "FK_DogImages_PredictionResults_PredictionResultsId",
                table: "DogImages",
                column: "PredictionResultsId",
                principalTable: "PredictionResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DogImages_PredictionResults_PredictionResultsId",
                table: "DogImages");

            migrationBuilder.DropIndex(
                name: "IX_DogImages_PredictionResultsId",
                table: "DogImages");

            migrationBuilder.DropColumn(
                name: "PredictionResultsId",
                table: "DogImages");

            migrationBuilder.AddColumn<Guid>(
                name: "DogImageEntityId",
                table: "PredictionResults",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PredictionResults_DogImageEntityId",
                table: "PredictionResults",
                column: "DogImageEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_PredictionResults_DogImages_DogImageEntityId",
                table: "PredictionResults",
                column: "DogImageEntityId",
                principalTable: "DogImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
