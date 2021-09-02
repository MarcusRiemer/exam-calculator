using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExamCalculator.Data.Migrations
{
    public partial class AddedExaminations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Examinations",
                columns: table => new
                {
                    ExaminationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ExamId = table.Column<Guid>(type: "TEXT", nullable: false),
                    GroupId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TakenOn = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examinations", x => x.ExaminationId);
                    table.ForeignKey(
                        name: "FK_Examinations_Exams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exams",
                        principalColumn: "ExamId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Examinations_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExaminationTaskResults",
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
                    table.PrimaryKey("PK_ExaminationTaskResults", x => x.ExaminationTaskResultId);
                    table.ForeignKey(
                        name: "FK_ExaminationTaskResults_Examinations_ExaminationId",
                        column: x => x.ExaminationId,
                        principalTable: "Examinations",
                        principalColumn: "ExaminationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExaminationTaskResults_ExamTasks_ExamTaskId",
                        column: x => x.ExamTaskId,
                        principalTable: "ExamTasks",
                        principalColumn: "ExamTaskId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExaminationTaskResults_Pupils_PupilId",
                        column: x => x.PupilId,
                        principalTable: "Pupils",
                        principalColumn: "PupilId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_ExamId",
                table: "Examinations",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_GroupId",
                table: "Examinations",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationTaskResults_ExaminationId",
                table: "ExaminationTaskResults",
                column: "ExaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationTaskResults_ExamTaskId",
                table: "ExaminationTaskResults",
                column: "ExamTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationTaskResults_PupilId",
                table: "ExaminationTaskResults",
                column: "PupilId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExaminationTaskResults");

            migrationBuilder.DropTable(
                name: "Examinations");
        }
    }
}
