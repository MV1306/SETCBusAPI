using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SETCBusAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddBusRouteServiceStages2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StageOrder",
                table: "BusRouteServiceStages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StageOrder",
                table: "BusRouteServiceStages");
        }
    }
}
