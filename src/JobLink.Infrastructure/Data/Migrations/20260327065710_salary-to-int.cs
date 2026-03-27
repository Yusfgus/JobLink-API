using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobLink.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class salarytoint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SalaryRange_Min",
                table: "Jobs",
                type: "int(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "SalaryRange_Max",
                table: "Jobs",
                type: "int(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "Salary",
                table: "Experiences",
                type: "INTEGER",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "SalaryRange_Min",
                table: "Jobs",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "SalaryRange_Max",
                table: "Jobs",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Salary",
                table: "Experiences",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldPrecision: 18,
                oldScale: 2);
        }
    }
}
