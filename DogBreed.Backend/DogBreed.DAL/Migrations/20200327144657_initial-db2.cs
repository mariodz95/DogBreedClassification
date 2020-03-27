using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DogBreed.DAL.Migrations
{
    public partial class initialdb2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DogImageEntityId",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_DogImageEntityId",
                table: "Users",
                column: "DogImageEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_DogImages_DogImageEntityId",
                table: "Users",
                column: "DogImageEntityId",
                principalTable: "DogImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_DogImages_DogImageEntityId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_DogImageEntityId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DogImageEntityId",
                table: "Users");
        }
    }
}
