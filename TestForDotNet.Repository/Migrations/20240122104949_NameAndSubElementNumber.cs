using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestForDotNet.Repository.Migrations
{
    /// <inheritdoc />
    public partial class NameAndSubElementNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Windows",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Element",
                table: "SubElements",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Windows");

            migrationBuilder.DropColumn(
                name: "Element",
                table: "SubElements");
        }
    }
}
