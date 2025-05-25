using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SETCBusAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemoveServiceCodeToBusRoute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceCode",
                table: "BusRoutes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ServiceCode",
                table: "BusRoutes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
