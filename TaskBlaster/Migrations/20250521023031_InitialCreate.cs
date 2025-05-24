using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskBlaster.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: true),
                    Url = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Uid = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.UniqueConstraint("AK_User_Uid", x => x.Uid);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Uid = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_User_Uid",
                        column: x => x.Uid,
                        principalTable: "User",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Duties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: true),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Priority = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Duties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Duties_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Content = table.Column<string>(type: "text", nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DutyId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Duties_DutyId",
                        column: x => x.DutyId,
                        principalTable: "Duties",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DutyResource",
                columns: table => new
                {
                    DutiesId = table.Column<int>(type: "integer", nullable: false),
                    ResourcesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DutyResource", x => new { x.DutiesId, x.ResourcesId });
                    table.ForeignKey(
                        name: "FK_DutyResource_Duties_DutiesId",
                        column: x => x.DutiesId,
                        principalTable: "Duties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DutyResource_Resources_ResourcesId",
                        column: x => x.ResourcesId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Resources",
                columns: new[] { "Id", "Description", "Title", "Type", "Url" },
                values: new object[,]
                {
                    { 1, null, "Laptop", null, null },
                    { 2, null, "Vacuum Cleaner", null, null },
                    { 3, null, "Notebook", null, null },
                    { 4, null, "Car", null, null }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Uid" },
                values: new object[] { 1, "a" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Title", "Uid" },
                values: new object[,]
                {
                    { 1, "Work", "a" },
                    { 2, "Home", "a" },
                    { 3, "Personal Goals", "a" },
                    { 4, "Errands", "a" }
                });

            migrationBuilder.InsertData(
                table: "Duties",
                columns: new[] { "Id", "CategoryId", "Description", "DueDate", "IsCompleted", "Priority", "Title" },
                values: new object[,]
                {
                    { 1, 1, "Gather all department data and submit the final report to the manager.", null, false, "High", "Submit monthly report" },
                    { 2, 2, "Organize tools and dispose of unwanted items.", null, false, "Medium", "Clean the garage" },
                    { 3, 3, "Jog around the neighborhood for cardio.", null, true, "Low", "Run 5K" }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "DutyId", "TimeStamp" },
                values: new object[,]
                {
                    { 1, "Waiting on data from finance team.", 1, null },
                    { 2, "Don't forget to recycle old paint cans.", 2, null },
                    { 3, "Completed it in 28 minutes!", 3, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Uid",
                table: "Categories",
                column: "Uid");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_DutyId",
                table: "Comments",
                column: "DutyId");

            migrationBuilder.CreateIndex(
                name: "IX_Duties_CategoryId",
                table: "Duties",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_DutyResource_ResourcesId",
                table: "DutyResource",
                column: "ResourcesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "DutyResource");

            migrationBuilder.DropTable(
                name: "Duties");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
