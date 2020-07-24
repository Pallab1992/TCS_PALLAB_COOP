using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LHV.Model
{
    public class Student
    {
        [Key]
        [Required]
        public int StudentID {get; set;}
        public string StudentName {get; set;}
        public string StudentRegistrationNo {get; set;}
        public string StudentRoll {get; set;} 
        public string StudentAddress {get; set;}
        public string StudentContactNo {get; set;}
        public int DepartmentID {get; set;}
        public Department Department {get; set;}
        public ICollection<StudentCourse> StudentCourses {get; set;}
    }
}