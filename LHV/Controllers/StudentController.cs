using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using LHV.Service;
using LHV.Model;
using LHV.DTO;
using LHV.Exceptions;

namespace LHV.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IStudentService _studentservice;

        public StudentController(IConfiguration configuration, IStudentService studentservice)
        {
            _configuration = configuration;
            _studentservice = studentservice;
        }

        [HttpGet]
        public async Task<ActionResult> GetStudentByID(int ID)
        {
            try
            {
                var student = await _studentservice.GetStudentByID(ID);
                if (student != null)
                    return StatusCode(200, new { Success = true, Student = student });
                else
                    return StatusCode(200, new { Success = false});
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { type = "Exception", message = ex.Message, stacktrace = ex.StackTrace });
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddStudent([FromBody] Student student)
        {
            try
            {
                var student_add = 0;
                if (student != null)
                {
                    student_add = await _studentservice.StudentAdd(student);
                }
                if (student_add != 0)
                {
                    return StatusCode(200, new { Success = true, message = "Student Added Successfully"});
                }
                else
                {
                    return StatusCode(400, new { Success = false, message = "Student Addtion Failed"});
                }
            }
            catch (DepartmentNotExistException deptNotExist)
            {
                return StatusCode(400, new { Success = false, type = deptNotExist.Message.ToString(), message = "Department Not Found"});
            }
            catch (DuplicateEntryException duplicateEx)
            {
                return StatusCode(400, new { Success = false, type = duplicateEx.Message.ToString(), message = "Duplicate Key Found For This Insertion"});
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { success = false, message = ex.Message});
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddDepartment([FromBody] Department department)
        {
            try
            {
                var addDepartment = 0;
                if (department != null)
                {
                    addDepartment = await _studentservice.DepartmentAdd(department);
                }
                if (addDepartment != 0)
                {
                    return StatusCode(200, new { success = true, message = "Department Added Successfully"});
                }
                else
                {
                    return StatusCode(400, new { success = false, message = "Department Not Added Successfully"});
                }
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { success = false, message = ex.Message});
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAllStudent()
        {
            try
            {
                List<StudentDeptRecordResponse> studentAll = await _studentservice.GetAllStudent();
                if (studentAll.Count != 0)
                {
                    return StatusCode(200, new { Success = true, StudentDeptRecordResponse = studentAll});
                }
                else
                {
                    return StatusCode(200, new { Success = false, message = "No Record Present"});
                }
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { success = false, message = ex.Message});
            }
        }
    }
}