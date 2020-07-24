using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using LHV.Model;
using LHV.DTO;
namespace LHV.Service
{
    public interface IStudentService
    {
        Task<int> DepartmentAdd(Department department);
        Task<int> CourseAdd (Course course);
        Task<int> AssignDepToCourse (DepartmentCourse depcourse);
        Task<int> StudentAdd(AddStudentRequest studentReq);
        Task<int> StudentUpdate(UpdateStudentRequest studentUpdtReq);
        Task<int> AssignCourseToStudent(AssignCourseToStudentRequest assignReq);
        Task<Course> GetCourseByCode(string courseCode);
        Task<List<Course>> GetAllCourse();
        Task<GetAllDepartmentResponse> GetDeptByCode(string deptCode);
    }
}