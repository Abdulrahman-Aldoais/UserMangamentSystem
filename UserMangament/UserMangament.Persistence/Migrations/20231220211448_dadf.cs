using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserMangament.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class dadf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "WorkingHours",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "WorkingHours",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "WorkingHours",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "WorkingHours",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "WorkingHours",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "WorkingHours",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "WorkingHours",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 21, 0, 14, 47, 370, DateTimeKind.Local).AddTicks(3315));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 21, 0, 14, 47, 370, DateTimeKind.Local).AddTicks(3323));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "HireDate" },
                values: new object[] { new DateTime(2023, 12, 21, 0, 14, 47, 383, DateTimeKind.Local).AddTicks(5102), new DateTime(2023, 12, 21, 0, 14, 47, 383, DateTimeKind.Local).AddTicks(5104) });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "HireDate" },
                values: new object[] { new DateTime(2023, 12, 21, 0, 14, 47, 383, DateTimeKind.Local).AddTicks(5110), new DateTime(2023, 12, 21, 0, 14, 47, 383, DateTimeKind.Local).AddTicks(5113) });

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 21, 0, 14, 47, 385, DateTimeKind.Local).AddTicks(6478));

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 21, 0, 14, 47, 385, DateTimeKind.Local).AddTicks(6491));

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 21, 0, 14, 47, 385, DateTimeKind.Local).AddTicks(6494));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 21, 0, 14, 47, 386, DateTimeKind.Local).AddTicks(398));

            migrationBuilder.UpdateData(
                table: "WorkingHours",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedBy", "CreatedDate", "DeletedBy", "IsDeleted", "ModifiedBy", "ModifiedDate", "Name" },
                values: new object[] { null, new DateTime(2023, 12, 21, 0, 14, 47, 386, DateTimeKind.Local).AddTicks(3964), null, false, null, null, " " });

            migrationBuilder.UpdateData(
                table: "WorkingHours",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedBy", "CreatedDate", "DeletedBy", "IsDeleted", "ModifiedBy", "ModifiedDate", "Name" },
                values: new object[] { null, new DateTime(2023, 12, 21, 0, 14, 47, 386, DateTimeKind.Local).AddTicks(3972), null, false, null, null, " " });

            migrationBuilder.UpdateData(
                table: "WorkingHours",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedBy", "CreatedDate", "DeletedBy", "IsDeleted", "ModifiedBy", "ModifiedDate", "Name" },
                values: new object[] { null, new DateTime(2023, 12, 21, 0, 14, 47, 386, DateTimeKind.Local).AddTicks(3974), null, false, null, null, " " });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "WorkingHours");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "WorkingHours");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "WorkingHours");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "WorkingHours");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "WorkingHours");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "WorkingHours");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "WorkingHours");

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
                column: "CreatedDate",
                value: new DateTime(2023, 12, 18, 12, 1, 10, 143, DateTimeKind.Local).AddTicks(2551));

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 18, 12, 1, 10, 143, DateTimeKind.Local).AddTicks(2559));

            migrationBuilder.UpdateData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 18, 12, 1, 10, 143, DateTimeKind.Local).AddTicks(2560));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 12, 18, 12, 1, 10, 143, DateTimeKind.Local).AddTicks(3970));
        }
    }
}
