using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankSystem.API.Migrations
{
    /// <inheritdoc />
    public partial class AddContractNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Contract",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "Contract");
        }
    }
}
