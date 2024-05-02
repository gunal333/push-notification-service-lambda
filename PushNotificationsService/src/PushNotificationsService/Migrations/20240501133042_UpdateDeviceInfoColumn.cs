using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PushNotificationsService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDeviceInfoColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userName",
                table: "DeviceInfos");

            migrationBuilder.AddColumn<string>(
                name: "customerDeviceEndpoint",
                table: "DeviceInfos",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "customerDeviceEndpoint",
                table: "DeviceInfos");

            migrationBuilder.AddColumn<string>(
                name: "userName",
                table: "DeviceInfos",
                type: "text",
                nullable: true);
        }
    }
}
