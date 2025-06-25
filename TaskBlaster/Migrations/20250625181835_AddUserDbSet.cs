using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskBlaster.Migrations
{
    /// <inheritdoc />
    public partial class AddUserDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_User_Uid",
                table: "Categories");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_User_Uid",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Users_Uid",
                table: "Users",
                column: "Uid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Users_Uid",
                table: "Categories",
                column: "Uid",
                principalTable: "Users",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Users_Uid",
                table: "Categories");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Users_Uid",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_User_Uid",
                table: "User",
                column: "Uid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_User_Uid",
                table: "Categories",
                column: "Uid",
                principalTable: "User",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
