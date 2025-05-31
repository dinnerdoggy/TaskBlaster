using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskBlaster.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Uid" },
                values: new object[] { 2, "b" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
