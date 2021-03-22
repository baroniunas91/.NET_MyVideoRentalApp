using Microsoft.EntityFrameworkCore.Migrations;

namespace VideoRent.Data.Migrations
{
    public partial class SeedRentalStatusIdColumnData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE Rentals SET RentalStatusId = 1 WHERE Returned = 'True'");
            migrationBuilder.Sql("UPDATE Rentals SET RentalStatusId = 2 WHERE Returned = 'False'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
