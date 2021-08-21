using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExamCalculator.Data.Migrations
{
    public partial class AddedExams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exams",
                columns: table => new
                {
                    ExamId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exams", x => x.ExamId);
                });

            migrationBuilder.CreateTable(
                name: "ExamTasks",
                columns: table => new
                {
                    ExamTaskId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Number = table.Column<string>(type: "TEXT", nullable: true),
                    MaximumPoints = table.Column<float>(type: "REAL", nullable: false),
                    ExamId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamTasks", x => x.ExamTaskId);
                    table.ForeignKey(
                        name: "FK_ExamTasks_Exams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exams",
                        principalColumn: "ExamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExamTasks_ExamId",
                table: "ExamTasks",
                column: "ExamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExamTasks");

            migrationBuilder.DropTable(
                name: "Exams");
        }
    }
}
