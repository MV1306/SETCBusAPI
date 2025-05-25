using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SETCBusAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddBusRouteServiceStages1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusRouteServiceStages",
                columns: table => new
                {
                    StageID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StageCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StageFlag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StageTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Distance = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusRouteServiceStages", x => x.StageID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusRouteServiceStages");
        }
    }
}
