using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace StockArt.Data.Migrations
{
    public partial class issubject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImageSetSubject",
                columns: table => new
                {
                    ImageSetName = table.Column<string>(nullable: false),
                    SubjectID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageSetSubject", x => new { x.ImageSetName, x.SubjectID });
                    table.ForeignKey(
                        name: "FK_ImageSetSubject_ImageSets_ImageSetName",
                        column: x => x.ImageSetName,
                        principalTable: "ImageSets",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImageSetSubject_Subjects_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subjects",
                        principalColumn: "SubjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImageSetSubject_SubjectID",
                table: "ImageSetSubject",
                column: "SubjectID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImageSetSubject");
        }
    }
}
