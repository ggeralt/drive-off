using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentACarApi.Migrations
{
    /// <inheritdoc />
    public partial class AddNullableRelationsToVehicle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VehicleId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VehicleId",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VehicleId",
                table: "Pictures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_VehicleId",
                table: "Reviews",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_VehicleId",
                table: "Reservations",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_VehicleId",
                table: "Pictures",
                column: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Vehicles_VehicleId",
                table: "Pictures",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Vehicles_VehicleId",
                table: "Reservations",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Vehicles_VehicleId",
                table: "Reviews",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Vehicles_VehicleId",
                table: "Pictures");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Vehicles_VehicleId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Vehicles_VehicleId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_VehicleId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_VehicleId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Pictures_VehicleId",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "Pictures");
        }
    }
}
