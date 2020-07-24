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

        [HttpPost]
        public async Task<ActionResult> AddDepartment([FromBody] Department department)
        {
            try
            {
                int addDepartment = 0;
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
            catch (DuplicateEntryException duplicateEntry)
            {
                return StatusCode(400, new { Success = false, type = duplicateEntry.Message.ToString(), message = "Duplicate Department Found. Insertion Not Possible"});
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { success = false, message = ex.Message});
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddCourse([FromBody] Course course)
        {
            try
            {
                int addcourse = 0;
                if (course != null)
                {
                    addcourse = await _studentservice.CourseAdd(course);
                }
                if (addcourse != 0)
                {
                    return StatusCode(200, new { success = true, message = "Course Added Successfully"});
                }
                else
                {
                    return StatusCode(400, new { success = false, message = "Course Not Added Successfully"});
                }
            }
            catch (DuplicateEntryException duplicateKeyFound)
            {
                return StatusCode(400, new { Success = false, type = duplicateKeyFound.Message.ToString(), message = "Duplicate Course Found. Insertion Not Possible"});
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { success = false, message = ex.Message});
            }
        }

        [HttpPost]
        public async Task<ActionResult> AssignDepartmentToCourse (DepartmentCourse depcourse)
        {
            try
            {
                int assignDepInCourse = 0;
                if (depcourse != null)
                {
                    assignDepInCourse = await _studentservice.AssignDepToCourse(depcourse);
                }
                if (assignDepInCourse != 0)
                {
                    return StatusCode(200, new { success = true, message = "Course Assignment Successfull"});
                }
                else
                {
                    return StatusCode(400, new { success = false, message = "Course Not Assigned Successfully"});
                }
            }
            catch (DuplicateEntryException duplicateEntry)
            {
                return StatusCode(400, new { Success = false, type = duplicateEntry.Message.ToString(), message = "Course is Already assigned to this Department"});
            }
            catch (DepartmentNotExistException deptNotExist)
            {
                return StatusCode(400, new { Success = false, type = deptNotExist.Message.ToString(), message = "Department Not Found"});
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddStudent([FromBody] AddStudentRequest studentReq)
        {
            try
            {
                int studentAdd = 0;
                if (studentReq != null)
                {
                    studentAdd = await _studentservice.StudentAdd(studentReq);
                }
                if (studentAdd != 0)
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
            catch (Exception ex)
            {
                return StatusCode(400, new { success = false, message = ex.Message});
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateStudent([FromBody] UpdateStudentRequest studentUpdtReq)
        {
            try
            {
                int studentAdd = 0;
                if (studentUpdtReq != null)
                {
                    studentAdd = await _studentservice.StudentUpdate(studentUpdtReq);
                }
                if (studentAdd != 0)
                {
                    return StatusCode(200, new { Success = true, message = "Student Updated Successfully"});
                }
                else
                {
                    return StatusCode(400, new { Success = false, message = "Student Updation Failed"});
                }
            }
            catch (DepartmentNotExistException deptNotExist)
            {
                return StatusCode(400, new { Success = false, type = deptNotExist.Message.ToString(), message = "Department Not Found"});
            }
            catch (UpdationNotPossibleException updateNotPossible)
            {
                return StatusCode(400, new { Success = false, type = updateNotPossible.Message.ToString(), message = "Update Not Possible"});
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { success = false, message = ex.Message});
            }
        }

        [HttpPost]
        public async Task<ActionResult> AssignCourseToStudent ([FromBody] AssignCourseToStudentRequest assignStudent)
        {
            try
            {
                int assignStudentToCourse = 0;
                if (assignStudent != null)
                {
                    assignStudentToCourse = await _studentservice.AssignCourseToStudent(assignStudent);
                    if (assignStudentToCourse != 0)
                    {
                        return StatusCode(200, new { Success = true, message = "Course Assigned Successfully to Student"});
                    }
                    else
                    {
                        return StatusCode(400, new { Success = false, message = "Course Not Assigned Successfully to Student"});
                    }
                }
                else
                {
                    return StatusCode(400, new { Success = false, message = "Request Body Does Not Consist Values"});
                }
            }
            catch (CourseNotAssignedToDepartmentException courseNotAssignedToDept)
            {
                return StatusCode(400, new { Success = false, type = courseNotAssignedToDept.Message.ToString(), message = "Course Not Assigned To The Department"});
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { success = false, message = ex.Message});
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetCourseDetailsByCode (string courseCode)
        {
            try
            {
                if (courseCode != null)
                {
                    var courseDetails = await _studentservice.GetCourseByCode(courseCode);
                    if (courseCode != null)
                    {
                        return StatusCode(200,new {success = true, Course = courseCode});
                    }
                    else
                    {
                        return StatusCode(400,new {success = false, message = "Record Not Found"});
                    }
                }
                else
                    {
                        return StatusCode(400,new {success = false, message = "Please Provide Course Code"});
                    }
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { type = "Exception", message = ex.Message, stacktrace = ex.StackTrace });
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAllCourse()
        {
            try
            {
                List<Course> courseAll = await _studentservice.GetAllCourse();
                if (courseAll != null)
                {
                    return StatusCode(200,new {success = true, Course = courseAll});
                }
                else
                {
                    return StatusCode(400, new {success = false, message = "No Courses Found"});
                }
            }
            catch (Exception ex)
            {
                return StatusCode(400, new {success = false, message = ex.Message});
            }
        }

        public async Task<ActionResult> GetDepartmentByCode(string deptCode)
        {
            try
            {
                var deptResult = await _studentservice.GetDeptByCode(deptCode);
                if (deptCode != null)
                {
                    return StatusCode(200, new { success = true, GetAllDepartmentResponse = deptResult});
                }
                else
                {
                    return StatusCode(400, new { success = false, message = "Department Not Found"});
                }
            }
            catch (Exception ex)
            {
                return StatusCode(400, new {success = false, message = ex.Message});  
            }
        }
    }
}