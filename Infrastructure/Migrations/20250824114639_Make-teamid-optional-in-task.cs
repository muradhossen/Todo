using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Maketeamidoptionalintask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Teams_TeamId",
                table: "Tasks");

            migrationBuilder.AlterColumn<int>(
                name: "TeamId",
                table: "Tasks",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Teams_TeamId",
                table: "Tasks",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Teams_TeamId",
                table: "Tasks");

            migrationBuilder.AlterColumn<int>(
                name: "TeamId",
                table: "Tasks",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Teams_TeamId",
                table: "Tasks",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
