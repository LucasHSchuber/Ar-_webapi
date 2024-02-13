using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aråstock.Migrations
{
    /// <inheritdoc />
    public partial class totalamountinstock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Item",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Item");
        }
    }
}
