using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VideoRent.Data.Migrations
{
    public partial class AddRentalStatusesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RentalStatusId",
                table: "Rentals",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RentalStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalStatuses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_RentalStatusId",
                table: "Rentals",
                column: "RentalStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_RentalStatuses_RentalStatusId",
                table: "Rentals",
                column: "RentalStatusId",
                principalTable: "RentalStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_RentalStatuses_RentalStatusId",
                table: "Rentals");

            migrationBuilder.DropTable(
                name: "RentalStatuses");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_RentalStatusId",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "RentalStatusId",
                table: "Rentals");
        }
    }
}
