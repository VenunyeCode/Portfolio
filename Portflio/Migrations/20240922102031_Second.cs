using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portflio.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Technologies",
                table: "Projects");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Technologies",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
