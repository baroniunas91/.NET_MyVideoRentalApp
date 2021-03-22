using Microsoft.EntityFrameworkCore.Migrations;

namespace VideoRent.Data.Migrations
{
    public partial class SeedRentalStatusesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SET IDENTITY_INSERT RentalStatuses ON");
            migrationBuilder.Sql("INSERT INTO RentalStatuses (Id, Name) VALUES (1, 'Returned')");
            migrationBuilder.Sql("INSERT INTO RentalStatuses (Id, Name) VALUES (2, 'Not Returned')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
