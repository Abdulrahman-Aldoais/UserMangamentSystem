using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserMangament.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class xcvx : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Jobs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Jobs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "Jobs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Jobs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "Jobs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Jobs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 18, 12, 1, 10, 139, DateTimeKind.Local).AddTicks(9162));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 18, 12, 1, 10, 139, DateTimeKind.Local).AddTicks(9165));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "HireDate" },
                values: new object[] { new DateTime(2023, 12, 18, 12, 1, 10, 142, DateTimeKind.Local).AddTicks(5530), new DateTime(2023, 12, 18, 12, 1, 10, 142, DateTimeKind.Local).AddTicks(5534) });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "HireDate" },
                values: new object[] { new DateTime(2023, 12, 18, 12, 1, 10, 142, DateTimeKind.Local).AddTicks(5537), new DateTime(2023, 12, 18, 12, 1, 10, 142, DateTimeKind.Local).AddTicks(5538) });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedBy", "CreatedDate", "DeletedBy", "IsDeleted", "ModifiedBy", "ModifiedDate" },
                values: new object[] { null, new DateTime(2023, 12, 18, 12, 1, 10, 143, DateTimeKind.Local).AddTicks(2551), null, false, null, null });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedBy", "CreatedDate", "DeletedBy", "IsDeleted", "ModifiedBy", "ModifiedDate" },
                values: new object[] { null, new DateTime(2023, 12, 18, 12, 1, 10, 143, DateTimeKind.Local).AddTicks(2559), null, false, null, null });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedBy", "CreatedDate", "DeletedBy", "IsDeleted", "ModifiedBy", "ModifiedDate" },
                values: new object[] { null, new DateTime(2023, 12, 18, 12, 1, 10, 143, DateTimeKind.Local).AddTicks(2560), null, false, null, null });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 18, 12, 1, 10, 143, DateTimeKind.Local).AddTicks(3970));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Jobs");

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 18, 0, 51, 5, 659, DateTimeKind.Local).AddTicks(6640));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 18, 0, 51, 5, 659, DateTimeKind.Local).AddTicks(6646));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "HireDate" },
                values: new object[] { new DateTime(2023, 12, 18, 0, 51, 5, 667, DateTimeKind.Local).AddTicks(2407), new DateTime(2023, 12, 18, 0, 51, 5, 667, DateTimeKind.Local).AddTicks(2412) });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "HireDate" },
                values: new object[] { new DateTime(2023, 12, 18, 0, 51, 5, 667, DateTimeKind.Local).AddTicks(2417), new DateTime(2023, 12, 18, 0, 51, 5, 667, DateTimeKind.Local).AddTicks(2419) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 18, 0, 51, 5, 672, DateTimeKind.Local).AddTicks(9455));
        }
    }
}
