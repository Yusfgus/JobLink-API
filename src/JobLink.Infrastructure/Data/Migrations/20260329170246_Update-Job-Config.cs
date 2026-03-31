using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobLink.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateJobConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SalaryRange_Min",
                table: "Jobs",
                newName: "MinSalary");

            migrationBuilder.RenameColumn(
                name: "SalaryRange_Max",
                table: "Jobs",
                newName: "MaxSalary");

            migrationBuilder.RenameColumn(
                name: "Location_Country",
                table: "Jobs",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "Location_City",
                table: "Jobs",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "Location_Area",
                table: "Jobs",
                newName: "Area");

            migrationBuilder.RenameColumn(
                name: "ExpirationDateUtc",
                table: "Jobs",
                newName: "ExpirationDate");

            migrationBuilder.RenameColumn(
                name: "ClosedAtUtc",
                table: "Jobs",
                newName: "ClosedAt");

            migrationBuilder.AlterColumn<int>(
                name: "MinSalary",
                table: "Jobs",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "MaxSalary",
                table: "Jobs",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Area",
                table: "Jobs",
                type: "TEXT",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 100);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MinSalary",
                table: "Jobs",
                newName: "SalaryRange_Min");

            migrationBuilder.RenameColumn(
                name: "MaxSalary",
                table: "Jobs",
                newName: "SalaryRange_Max");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Jobs",
                newName: "Location_Country");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Jobs",
                newName: "Location_City");

            migrationBuilder.RenameColumn(
                name: "Area",
                table: "Jobs",
                newName: "Location_Area");

            migrationBuilder.RenameColumn(
                name: "ExpirationDate",
                table: "Jobs",
                newName: "ExpirationDateUtc");

            migrationBuilder.RenameColumn(
                name: "ClosedAt",
                table: "Jobs",
                newName: "ClosedAtUtc");

            migrationBuilder.AlterColumn<int>(
                name: "SalaryRange_Min",
                table: "Jobs",
                type: "int(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "SalaryRange_Max",
                table: "Jobs",
                type: "int(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "Location_Area",
                table: "Jobs",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
