using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExamCalculator.Data.Migrations
{
    public partial class PublicRelease : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    GroupId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.GroupId);
                });

            migrationBuilder.CreateTable(
                name: "Pupils",
                columns: table => new
                {
                    PupilId = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pupils", x => x.PupilId);
                });

            migrationBuilder.CreateTable(
                name: "GroupPupil",
                columns: table => new
                {
                    GroupsGroupId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PupilsPupilId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupPupil", x => new { x.GroupsGroupId, x.PupilsPupilId });
                    table.ForeignKey(
                        name: "FK_GroupPupil_Groups_GroupsGroupId",
                        column: x => x.GroupsGroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupPupil_Pupils_PupilsPupilId",
                        column: x => x.PupilsPupilId,
                        principalTable: "Pupils",
                        principalColumn: "PupilId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupPupil_PupilsPupilId",
                table: "GroupPupil",
                column: "PupilsPupilId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupPupil");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Pupils");
        }
    }
}
