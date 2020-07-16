using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LHV.Model;
using LHV.DTO;

namespace LHV.Repository
{
  public interface IStudentRepository : IBaseRepository<Student> 
  {
      Task<Student> GetStudentByID(int ID);
      Task<int> AddStudentRec(Student student);
      Task<Student> GetStudentByRoll(int Roll);
      Task<int> DepAdd(Department department);
      Task<Department> GetDepByDepID(int DeptID);
      Task<List<StudentDeptRecordResponse>> GetAllStudent();
  }
}