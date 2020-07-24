using Microsoft.EntityFrameworkCore.Migrations;

namespace LHV.Migrations
{
    public partial class GenerateRegistrationRollNo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var RegistrationGenerationTrigger = @"CREATE TRIGGER StudentRegistrationRollNoGenerator ON Student 
                                                INSTEAD OF INSERT
                                                AS
                                                DECLARE @StuNO INT;
                                                DECLARE @DepmntID INT;
                                                DECLARE @DeptCode NVARCHAR(3);
                                                DECLARE @RegNo NVARCHAR(15);
                                                DECLARE @RegisFinal NVARCHAR(15);
                                                DECLARE @StuAddress NVARCHAR(30);
                                                DECLARE @StuContact NVARCHAR(10);
                                                DECLARE @StuName NVARCHAR(30);
                                                DECLARE @RollNo NVARCHAR(10);
                                                DECLARE @RollFinal NVARCHAR(10);
                                                DECLARE @NoOfStudent INT;
                                                BEGIN
                                                SET @StuNO = (SELECT COUNT(*) FROM Student);
                                                SELECT @DepmntID = DepartmentID FROM inserted;
                                                SELECT @DeptCode = DepartmentCode FROM Department WHERE DepartmentID = @DepmntID;
                                                SET @RegNo = YEAR(GETDATE())+''+@DeptCode+''+@StuNO;
                                                SET @RollNo = YEAR(GETDATE())+''+@StuNO;
                                                SET @RegisFinal = @RegNo+''+REPLICATE('0',15-LEN(@RegNo));
                                                SET @RollFinal = @RollNo+''+REPLICATE('0',10-LEN(@RollFinal));
                                                SELECT @StuName = StudentName FROM inserted;
                                                SELECT @StuAddress = StudentAddress FROM inserted;
                                                SELECT @StuContact = StudentContactNo FROM inserted;
                                                INSERT INTO Students (StudentName, StudentRegistrationNo, StudentRoll, StudentAddress, StudentContactNo, DepartmentID) VALUES (@StuName,@RegisFinal,@RollFinal,@StuAddress,@StuContact,@DepmntID);
                                                END";
            migrationBuilder.Sql(RegistrationGenerationTrigger);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
