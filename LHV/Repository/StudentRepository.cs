using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LHV.DTO;
using LHV.Model;
using Microsoft.EntityFrameworkCore;

namespace LHV.Repository {
    public class StudentRepository : IStudentRepository {
        private StudentDbContext _dbcontext;

        public StudentRepository (StudentDbContext dbcontext) {
            _dbcontext = dbcontext;
        }

        public async Task<int> DepAdd (Department department) {
            try {
                await _dbcontext.Department.AddAsync (department);
                return await _dbcontext.SaveChangesAsync ();
            } catch (Exception ex) {
                throw ex;
            }
        }
        
        public async Task<int> CourseAdd (Course course) {
            try {
                await _dbcontext.Course.AddAsync (course);
                return await _dbcontext.SaveChangesAsync ();
            } catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<Department> GetDepByDepID (int DeptID) {
            try {
                var deptById = await _dbcontext.Department.Where (d => d.DepartmentID == DeptID).ToListAsync ();
                return deptById.FirstOrDefault ();
            } catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<int> AssignDepToCourse (DepartmentCourse depcourse)
        {
            try{
                await _dbcontext.DepartmentCourse.AddAsync(depcourse);
                return await _dbcontext.SaveChangesAsync ();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<int> AddStudentRec (Student student) {
            try {
                await _dbcontext.Student.AddAsync (student);
                return await _dbcontext.SaveChangesAsync ();  
            } catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<int> UpdateStudentRec (Student student) {
            try {
                _dbcontext.Student.Update(student);
                return await _dbcontext.SaveChangesAsync ();  
            } catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<Student> GetStudentID(string StudentRollNo)
        {
            try{
                var studentDet = await _dbcontext.Student.Where(s=> s.StudentRoll == StudentRollNo).ToListAsync();
                return studentDet.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Student> GetStudentByID (int ID) {
            try {
                var stulist = await _dbcontext.Student.Where (s => s.StudentID == ID).ToListAsync ();
                return stulist.FirstOrDefault ();
            } catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<Course> GetCourseID(string CourseCodeNo)
        {
            try {
                var courselist = await _dbcontext.Course.Where (c=> c.CourseCode == CourseCodeNo).ToListAsync();
                return courselist.FirstOrDefault ();
            } catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<int> AssignCourseToStudent (StudentCourse stuCourse)
        {
            try{
                await _dbcontext.StudentCourse.AddAsync(stuCourse);
                return await _dbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DepartmentCourse> GetCourseEligibility (int courseID, int deptID)
        {
            try{
                var result = await _dbcontext.DepartmentCourse.Where(dc=> dc.DepartmentID == deptID && dc.CourseID == courseID).ToListAsync();
                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Course> IfCourseExist (string courseCode)
        {
            try{
                var result = await _dbcontext.Course.Where(c=> c.CourseCode == courseCode).ToListAsync();
                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Department> IfDeptExist (string deptCode)
        {
            try{
                var result = await _dbcontext.Department.Where(d=> d.DepartmentCode == deptCode).ToListAsync();
                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DepartmentCourse> CheckForDuplicateDepForCourse (int deptID, int courseID)
        {
            try{
                var result = await _dbcontext.DepartmentCourse.Where(dc=> dc.DepartmentID == deptID && dc.CourseID == courseID).ToListAsync();
                return result.FirstOrDefault();
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
                var response = await _dbcontext.Course.ToListAsync();
                Course[] course = new Course[response.Count];
                for (int counter = 0; counter < response.Count; counter++)
                {
                    course[counter] = new Course();
                    course[counter].CourseID = response[counter].CourseID;
                    course[counter].CourseName = response[counter].CourseName;
                    course[counter].CourseCode = response[counter].CourseCode;
                }
                return course.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<string>> GetCourseList(int deptID)
        {
            try{
                var reponse = await _dbcontext.DepartmentCourse.Where(dp=> dp.DepartmentID == deptID).ToListAsync();
                string[] str = new string[reponse.Count];
                for (int count = 0; count < reponse.Count; count++)
                {
                    var result = await _dbcontext.Course.Where(c=> c.CourseID == reponse[count].CourseID).ToListAsync();
                    var resFinal = result.FirstOrDefault();
                    str[count] = resFinal.CourseName;
                }
                return str.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}