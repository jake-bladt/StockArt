using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace StockArt.Data.Migrations
{
    public partial class catimg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CatalogImagePath",
                table: "Subjects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CatalogImageType",
                table: "Subjects",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CatalogImagePath",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "CatalogImageType",
                table: "Subjects");
        }
    }
}
