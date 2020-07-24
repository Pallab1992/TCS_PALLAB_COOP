using Microsoft.EntityFrameworkCore.Migrations;

namespace LHV.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    CourseID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseName = table.Column<string>(maxLength: 30, nullable: true),
                    CourseCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.CourseID);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    DepartmentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(maxLength: 30, nullable: true),
                    DepartmentCode = table.Column<string>(maxLength: 3, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.DepartmentID);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentCourse",
                columns: table => new
                {
                    DepartmentCourseID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentID = table.Column<int>(nullable: false),
                    CourseID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentCourse", x => x.DepartmentCourseID);
                    table.ForeignKey(
                        name: "FK_DepartmentCourse_Course_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Course",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentCourse_Department_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Department",
                        principalColumn: "DepartmentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    StudentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentName = table.Column<string>(maxLength: 30, nullable: true),
                    StudentRegistrationNo = table.Column<string>(maxLength: 15, nullable: true),
                    StudentRoll = table.Column<string>(maxLength: 10, nullable: true),
                    StudentAddress = table.Column<string>(maxLength: 30, nullable: true),
                    StudentContactNo = table.Column<string>(maxLength: 10, nullable: true),
                    DepartmentID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.StudentID);
                    table.ForeignKey(
                        name: "FK_Student_Department_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Department",
                        principalColumn: "DepartmentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentCourse",
                columns: table => new
                {
                    StudentCourseID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<int>(nullable: false),
                    CourseID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCourse", x => x.StudentCourseID);
                    table.ForeignKey(
                        name: "FK_StudentCourse_Course_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Course",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentCourse_Student_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Student",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Course_CourseCode",
                table: "Course",
                column: "CourseCode",
                unique: true,
                filter: "[CourseCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Department_DepartmentCode",
                table: "Department",
                column: "DepartmentCode",
                unique: true,
                filter: "[DepartmentCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentCourse_CourseID",
                table: "DepartmentCourse",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentCourse_DepartmentID",
                table: "DepartmentCourse",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Student_DepartmentID",
                table: "Student",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Student_StudentRegistrationNo",
                table: "Student",
                column: "StudentRegistrationNo",
                unique: true,
                filter: "[StudentRegistrationNo] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Student_StudentRoll",
                table: "Student",
                column: "StudentRoll",
                unique: true,
                filter: "[StudentRoll] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourse_CourseID",
                table: "StudentCourse",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourse_StudentID",
                table: "StudentCourse",
                column: "StudentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentCourse");

            migrationBuilder.DropTable(
                name: "StudentCourse");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Department");
        }
    }
}
