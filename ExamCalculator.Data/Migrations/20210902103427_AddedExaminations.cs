using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExamCalculator.Data.Migrations
{
    public partial class AddedExaminations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Examination",
                columns: table => new
                {
                    ExaminationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ExamId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TakenOn = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examination", x => x.ExaminationId);
                    table.ForeignKey(
                        name: "FK_Examination_Exams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exams",
                        principalColumn: "ExamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExaminationTaskResult",
                columns: table => new
                {
                    ExaminationTaskResultId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PupilId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ExaminationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ExamTaskId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Score = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationTaskResult", x => x.ExaminationTaskResultId);
                    table.ForeignKey(
                        name: "FK_ExaminationTaskResult_Examination_ExaminationId",
                        column: x => x.ExaminationId,
                        principalTable: "Examination",
                        principalColumn: "ExaminationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExaminationTaskResult_ExamTasks_ExamTaskId",
                        column: x => x.ExamTaskId,
                        principalTable: "ExamTasks",
                        principalColumn: "ExamTaskId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExaminationTaskResult_Pupils_PupilId",
                        column: x => x.PupilId,
                        principalTable: "Pupils",
                        principalColumn: "PupilId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Examination_ExamId",
                table: "Examination",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationTaskResult_ExaminationId",
                table: "ExaminationTaskResult",
                column: "ExaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationTaskResult_ExamTaskId",
                table: "ExaminationTaskResult",
                column: "ExamTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationTaskResult_PupilId",
                table: "ExaminationTaskResult",
                column: "PupilId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExaminationTaskResult");

            migrationBuilder.DropTable(
                name: "Examination");
        }
    }
}
