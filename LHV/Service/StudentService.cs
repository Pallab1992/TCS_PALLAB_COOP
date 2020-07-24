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

        public async Task<int> DepartmentAdd(Department department)
        {
            try
            {
                int departmentAdd = 0;
                if (department != null)
                {
                    var ifDeptExist = await _studentrepository.IfDeptExist(department.DepartmentCode);
                    if (ifDeptExist == null)
                    {
                        departmentAdd = await _studentrepository.DepAdd(department);
                    }
                    else
                    {
                        throw new DuplicateEntryException ("Duplicate Department Found");
                    }
                }
                return departmentAdd;
            }
            catch (DuplicateEntryException duplicateEntry)
            {
                throw new DuplicateEntryException(duplicateEntry.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> CourseAdd (Course course)
        {
            try
            {
                int courseAdd = 0;
                if (course != null)
                {
                    var ifCourseCodeExist = await _studentrepository.IfCourseExist(course.CourseCode);
                    if (ifCourseCodeExist == null)
                    {
                        courseAdd = await _studentrepository.CourseAdd(course);
                    }
                    else
                    {
                        throw new DuplicateEntryException("Duplicate Course Found");
                    }
                }
                return courseAdd;
            }
            catch (DuplicateEntryException dupliateKey)
            {
                throw new DuplicateEntryException (dupliateKey.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> AssignDepToCourse(DepartmentCourse depcourse)
        {
            try
            {
                int assignDep = 0;
                Department depExist = await _studentrepository.GetDepByDepID(depcourse.DepartmentID);
                if (depExist != null)
                {
                    var checkForDuplicate = await _studentrepository.CheckForDuplicateDepForCourse(depcourse.DepartmentID, depcourse.CourseID);
                    if (checkForDuplicate == null)
                    {
                        assignDep = await _studentrepository.AssignDepToCourse(depcourse);
                    }
                    else
                    {
                        throw new DuplicateEntryException ("Course is Already Assigned to this Department");
                    }
                }
                else
                {
                    throw new DepartmentNotExistException ("Department Not Exist");
                }
                return assignDep;
            }
            catch (DuplicateEntryException duplicateEntry)
            {
                throw new DuplicateEntryException(duplicateEntry.Message);
            }
            catch (DepartmentNotExistException deptnotExist)
            {
                throw new DepartmentNotExistException (deptnotExist.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> StudentAdd(AddStudentRequest studentReq)
        {
            try
            {
                int studentAdd = 0;
                if (studentReq != null)
                {
                    Department depExist = await _studentrepository.GetDepByDepID(studentReq.DepartmentIDReq);
                    if (depExist != null)
                    {
                        Student student = new Student();

                        student.StudentName = studentReq.StudentNameReq;
                        student.StudentAddress = studentReq.StudentaddressReq;
                        student.StudentContactNo = studentReq.StudentContactNoReq;
                        student.DepartmentID = studentReq.DepartmentIDReq;

                        studentAdd = await _studentrepository.AddStudentRec(student);
                    }
                    else
                    {
                        throw new DepartmentNotExistException ("Department Not Exist");
                    }
                }
                return studentAdd;
            }
            catch (DepartmentNotExistException deptnotExist)
            {
                throw new DepartmentNotExistException (deptnotExist.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> StudentUpdate(UpdateStudentRequest studentUpdtReq)
        {
            try
            {
                int studentUpdt = 0;
                int UpdtFlag = 0;
                if (studentUpdtReq != null)
                {
                    Department depExist = await _studentrepository.GetDepByDepID(studentUpdtReq.DepartmentIDReq);
                    if (depExist != null)
                    {
                        Student student = new Student();
                        var getStudentByID = await _studentrepository.GetStudentByID(studentUpdtReq.StudentIDReq);
                        student.StudentID = getStudentByID.StudentID;
                        if ( getStudentByID.StudentAddress.Equals(studentUpdtReq.StudentaddressReq) == false )
                        {
                            student.StudentAddress = studentUpdtReq.StudentaddressReq;
                            UpdtFlag = 1;
                        }
                        if ( getStudentByID.StudentContactNo.Equals(studentUpdtReq.StudentContactNoReq) == false )
                        {
                            student.StudentContactNo = studentUpdtReq.StudentContactNoReq;
                            UpdtFlag = 1;
                        }
                        if ( getStudentByID.DepartmentID != studentUpdtReq.DepartmentIDReq )
                        {
                            student.DepartmentID = studentUpdtReq.DepartmentIDReq;
                            UpdtFlag = 1;
                        }
                        if (UpdtFlag != 0)
                        {
                            studentUpdt = await _studentrepository.UpdateStudentRec(student);
                        }
                        else
                        {
                            throw new UpdationNotPossibleException ("Update Not Possible");
                        }
                    }
                    else
                    {
                        throw new DepartmentNotExistException ("Department Not Exist");
                    }
                }
                return studentUpdt;
            }
            catch (DepartmentNotExistException deptnotExist)
            {
                throw new DepartmentNotExistException (deptnotExist.Message);
            }
            catch (UpdationNotPossibleException updateNotPossible)
            {
                throw new UpdationNotPossibleException (updateNotPossible.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> AssignCourseToStudent (AssignCourseToStudentRequest assignReq)
        {
            try
            {
                int assignSuccess = 0;
                int stuID = 0;
                int courseID = 0;
                if (assignReq != null)
                {
                    var getStudentID = await _studentrepository.GetStudentID(assignReq.StudentRollReq);
                    var getCourseID = await _studentrepository.GetCourseID(assignReq.CourseCodeReq);
                    var getCourseEligibility = await _studentrepository.GetCourseEligibility(getCourseID.CourseID,getStudentID.DepartmentID);
                    stuID = getStudentID.StudentID;
                    courseID = getCourseID.CourseID;
                    if ( getCourseEligibility != null )
                    {
                        StudentCourse stuCourse = new StudentCourse();
                        stuCourse.StudentID = stuID;
                        stuCourse.CourseID = courseID;
                        assignSuccess = await _studentrepository.AssignCourseToStudent(stuCourse);
                    }
                    else
                    {
                        throw new CourseNotAssignedToDepartmentException ("Course Not Assigned For This Department");
                    }
                }
                return assignSuccess;
            }
            catch (CourseNotAssignedToDepartmentException courseNotAssignedToDept)
            {
                throw new CourseNotAssignedToDepartmentException(courseNotAssignedToDept.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Course> GetCourseByCode(string courseCode)
        {
            try
            {
               var courseDetails = await _studentrepository.GetCourseID(courseCode);
               if (courseDetails != null)
               {
                    return courseDetails;
               }
               else
               {
                   throw new Exception ("No Such Course Present");
               }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Course>> GetAllCourse()
        {
            try
            {
                List<Course> courseDetail = await _studentrepository.GetAllCourse();
                return courseDetail;               
            }
            catch (Exception ex)
            {
                throw ex;               
            }
        }

        public async Task<GetAllDepartmentResponse> GetDeptByCode(string deptCode)
        {
            try
            {
                if (deptCode != null)
                {
                    var deptResult = await _studentrepository.IfDeptExist(deptCode);
                    if (deptResult != null)
                    {
                        var courseList = await _studentrepository.GetCourseList(deptResult.DepartmentID);
                        var courseList1 = courseList.ToArray();
                        if (courseList != null)
                        {
                            GetAllDepartmentResponse getResult = new GetAllDepartmentResponse();
                            getResult.DepartmentIDResp = deptResult.DepartmentID;
                            getResult.DepartmentNameResp = deptResult.DepartmentName;
                            getResult.DepartmentCodeResp = deptResult.DepartmentCode;
                            for(int count = 0; count < courseList1.Length; count++)
                            {
                                getResult.CourseNameResp[count] = courseList1[count];
                            }
                            return getResult;
                        }
                        else
                        {
                            throw new Exception ("No Record Found");
                        }
                    }
                    else
                    {
                        throw new Exception ("Department Not Found");
                    }
                }
                else
                {
                    throw new Exception ("Please Provide Department Code");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}