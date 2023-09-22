using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LinkShortener.Data.Migrations
{
    /// <inheritdoc />
    public partial class FinalTouches : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Viewed",
                table: "Links",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Links",
                keyColumn: "RawUrl",
                keyValue: null,
                column: "RawUrl",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "RawUrl",
                table: "Links",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Links",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "Id", "Date", "RawUrl", "ShortUrl", "Viewed" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 9, 23, 0, 9, 8, 529, DateTimeKind.Local).AddTicks(5544), "https://google.com", "38fh43", 0 },
                    { 2, new DateTime(2023, 9, 23, 0, 9, 8, 529, DateTimeKind.Local).AddTicks(5557), "https://youtube.com", "fG47Kd", 420 },
                    { 3, new DateTime(2023, 9, 23, 0, 9, 8, 529, DateTimeKind.Local).AddTicks(5558), "https://avtobus1.ru/avtopark/avtobus/45-mest", "98CsfD", 1337 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Links",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Links",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Links",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AlterColumn<int>(
                name: "Viewed",
                table: "Links",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "RawUrl",
                table: "Links",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Links",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");
        }
    }
}
