using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskBlaster.Migrations
{
    /// <inheritdoc />
    public partial class UidOnDuty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Uid",
                table: "Duties",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Duties",
                keyColumn: "Id",
                keyValue: 1,
                column: "Uid",
                value: "a");

            migrationBuilder.UpdateData(
                table: "Duties",
                keyColumn: "Id",
                keyValue: 2,
                column: "Uid",
                value: "a");

            migrationBuilder.UpdateData(
                table: "Duties",
                keyColumn: "Id",
                keyValue: 3,
                column: "Uid",
                value: "a");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Uid",
                table: "Duties");
        }
    }
}
