using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentParent_WebApI.Migrations
{
    public partial class subjectNamefixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubject_Stubjects_SubjectId",
                table: "StudentSubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stubjects",
                table: "Stubjects");

            migrationBuilder.RenameTable(
                name: "Stubjects",
                newName: "Subjects");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subjects",
                table: "Subjects",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubject_Subjects_SubjectId",
                table: "StudentSubject",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSubject_Subjects_SubjectId",
                table: "StudentSubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subjects",
                table: "Subjects");

            migrationBuilder.RenameTable(
                name: "Subjects",
                newName: "Stubjects");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stubjects",
                table: "Stubjects",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSubject_Stubjects_SubjectId",
                table: "StudentSubject",
                column: "SubjectId",
                principalTable: "Stubjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
