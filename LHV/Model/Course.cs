using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LHV.Model
{
    public class Course
    {
        [Key]
        [Required]
        public int CourseID {get; set;}
        public string CourseName {get; set;}
        public string CourseCode {get; set;}
        public ICollection<StudentCourse> StudentCourses {get; set;}
        public ICollection<DepartmentCourse> DepartmentCourses {get; set;}
    }
}