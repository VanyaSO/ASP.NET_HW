using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hw_5_dop_2.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCoursesSeats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AvailableSeats",
                table: "Courses",
                newName: "BusySeats");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BusySeats",
                table: "Courses",
                newName: "AvailableSeats");
        }
    }
}
