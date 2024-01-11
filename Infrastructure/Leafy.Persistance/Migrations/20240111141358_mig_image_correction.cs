using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Leafy.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class mig_image_correction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "UserPlants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "UserPlants");
        }
    }
}
