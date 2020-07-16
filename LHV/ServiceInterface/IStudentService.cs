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
        Task<Student> GetStudentByID(int Student_ID);
        Task<int> StudentAdd(Student student);
        Task<int> DepartmentAdd(Department department);
        Task<List<StudentDeptRecordResponse>> GetAllStudent();
    }
}