using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LHV.Model;
using LHV.DTO;

namespace LHV.Repository
{
  public interface IStudentRepository : IBaseRepository<Student> 
  {
    Task<int> DepAdd(Department department);
    Task<int> CourseAdd(Course course);
    Task<Department> GetDepByDepID(int DeptID);
    Task<int> AssignDepToCourse (DepartmentCourse depcourse);
    Task<int> AddStudentRec(Student student);
    Task<int> UpdateStudentRec(Student student);
    Task<Student> GetStudentByID(int ID);
    Task<Student> GetStudentID(string StudentRoll);
    Task<Course> GetCourseID(string CourseCode);
    Task<int> AssignCourseToStudent(StudentCourse stuCourse);
    Task<DepartmentCourse> GetCourseEligibility(int courseID, int deptID);
    Task<Course> IfCourseExist (string courseCode);
    Task<Department> IfDeptExist (string deptCode);
    Task<DepartmentCourse> CheckForDuplicateDepForCourse(int deptID, int courseID);
    Task<List<Course>> GetAllCourse();
    Task<string[]> GetCourseList(int deptID);
  }
}