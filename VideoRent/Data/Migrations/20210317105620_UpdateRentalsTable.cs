﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace VideoRent.Data.Migrations
{
    public partial class UpdateRentalsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Customers_CustomerId1",
                table: "Rentals");

            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Movies_MovieId1",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_CustomerId1",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_MovieId1",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "CustomerId1",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "MovieId1",
                table: "Rentals");

            migrationBuilder.AlterColumn<int>(
                name: "MovieId",
                table: "Rentals",
                nullable: false,
                oldClrType: typeof(byte));

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Rentals",
                nullable: false,
                oldClrType: typeof(byte));

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_CustomerId",
                table: "Rentals",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_MovieId",
                table: "Rentals",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Customers_CustomerId",
                table: "Rentals",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Movies_MovieId",
                table: "Rentals",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Customers_CustomerId",
                table: "Rentals");

            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Movies_MovieId",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_CustomerId",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_MovieId",
                table: "Rentals");

            migrationBuilder.AlterColumn<byte>(
                name: "MovieId",
                table: "Rentals",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<byte>(
                name: "CustomerId",
                table: "Rentals",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "CustomerId1",
                table: "Rentals",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MovieId1",
                table: "Rentals",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_CustomerId1",
                table: "Rentals",
                column: "CustomerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_MovieId1",
                table: "Rentals",
                column: "MovieId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Customers_CustomerId1",
                table: "Rentals",
                column: "CustomerId1",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Movies_MovieId1",
                table: "Rentals",
                column: "MovieId1",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
