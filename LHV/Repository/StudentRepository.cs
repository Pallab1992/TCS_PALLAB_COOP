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

        public async Task<Student> GetStudentByID (int ID) {
            try {
                var stulist = await _dbcontext.Student.Where (s => s.Stu_ID == ID).ToListAsync ();
                return stulist.FirstOrDefault ();
            } catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<int> AddStudentRec (Student student) {
            try {
                await _dbcontext.Student.AddAsync (student);
                await _dbcontext.SaveChangesAsync ();
                return student.Stu_ID;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public async Task<Student> GetStudentByRoll (int studentRoll) {
            try {
                var stuListByRoll = await _dbcontext.Student.Where (s => s.Stu_Roll == studentRoll).ToListAsync ();
                return stuListByRoll.FirstOrDefault ();
            } catch (Exception ex) {
                throw ex;
            }
        }
        public async Task<int> DepAdd (Department department) {
            try {
                await _dbcontext.Department.AddAsync (department);
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

        public async Task<List<StudentDeptRecordResponse>> GetAllStudent () {
            try {
                //StudentDeptRecordResponse stuDeptResp = new StudentDeptRecordResponse();

                var studentDep = await _dbcontext.Student.Join (_dbcontext.Department, stu => stu.DepartmentID, dep => dep.DepartmentID, (stu, dep) => new { SID = stu.Stu_ID, SNAME = stu.Stu_Name, SROLL = stu.Stu_Roll, SDEP = dep.DeptName }).ToListAsync ();
                StudentDeptRecordResponse[] studentDeptRecordResponses = new StudentDeptRecordResponse[studentDep.Count];
                for (int count = 0; count < studentDep.Count; count++) {
                    studentDeptRecordResponses[count] = new StudentDeptRecordResponse ();
                    studentDeptRecordResponses[count].StudentID = studentDep[count].SID;
                    studentDeptRecordResponses[count].StudentName = studentDep[count].SNAME;
                    studentDeptRecordResponses[count].StudentRoll = studentDep[count].SROLL;
                    studentDeptRecordResponses[count].DepartmentName = studentDep[count].SDEP;
                }
                return studentDeptRecordResponses.ToList<StudentDeptRecordResponse> ();
            } catch (Exception ex) {
                throw ex;
            }
        }
    }
}