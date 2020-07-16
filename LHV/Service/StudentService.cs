using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using LHV.Model;
using LHV.Repository;
using LHV.Exceptions;
using LHV.DTO;
using Microsoft.Extensions.Configuration;

namespace LHV.Service
{
    public class StudentService : IStudentService
    {
        private StudentDbContext _dbcontext;
        private IConfiguration _configuration;
        private IStudentRepository _studentrepository;

        public StudentService(StudentDbContext dbContext, IConfiguration configuration)
        {
            _dbcontext = dbContext;
            _configuration = configuration;
            _studentrepository = new StudentRepository(_dbcontext);
        }

        public async Task<Student> GetStudentByID(int Student_ID)
        {
            try
            {
                var student = await _studentrepository.GetStudentByID(Student_ID);
                if (student != null)
                {
                    return student;
                }
                else
                {
                    throw new Exception("No Such Student Present");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> StudentAdd(Student student)
        {
            try
            {
                var student_add = 0;
                if (student != null)
                {
                    Student studentByRoll = await _studentrepository.GetStudentByRoll(student.Stu_Roll);
                    if (studentByRoll != null)
                    {
                        throw new DuplicateEntryException ("Duplicate Key Found");
                    }
                    else
                    {
                        Department depExist = await _studentrepository.GetDepByDepID(student.DepartmentID);
                        if (depExist != null)
                        {
                            student_add = await _studentrepository.AddStudentRec(student);
                        }
                        else
                        {
                            throw new DepartmentNotExistException ("Department Not Exist");
                        }
                    }
                }
                return student_add;
            }
            catch (DepartmentNotExistException deptnotExist)
            {
                throw new DepartmentNotExistException (deptnotExist.Message);
            }
            catch (DuplicateEntryException duplicateKey)
            {
                throw new DuplicateEntryException (duplicateKey.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> DepartmentAdd(Department department)
        {
            try
            {
                var departmentAdd = 0;
                if (department != null)
                {
                    departmentAdd = await _studentrepository.DepAdd(department);
                }
                return departmentAdd;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<StudentDeptRecordResponse>> GetAllStudent()
        {
            try
            {
                List<StudentDeptRecordResponse> studentAll = await _studentrepository.GetAllStudent();
                return studentAll;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}