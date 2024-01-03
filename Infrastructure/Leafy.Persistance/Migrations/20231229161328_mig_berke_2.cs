﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Leafy.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class mig_berke_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Diseases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Diseases");
        }
    }
}