using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Udemy.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemoveImageUrlFromProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestProperty",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "imageUrl",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TestProperty",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "imageUrl",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "TestProperty", "imageUrl" },
                values: new object[] { 0, "" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "TestProperty", "imageUrl" },
                values: new object[] { 0, "" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "TestProperty", "imageUrl" },
                values: new object[] { 0, "" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "TestProperty", "imageUrl" },
                values: new object[] { 0, "" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "TestProperty", "imageUrl" },
                values: new object[] { 0, "" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "TestProperty", "imageUrl" },
                values: new object[] { 0, "" });
        }
    }
}
