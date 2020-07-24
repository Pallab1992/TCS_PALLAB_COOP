using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LHV.Model
{
    public class DepartmentCourse
    {
        [Key]
        [Required]
        public int DepartmentCourseID {get; set;}
        public int DepartmentID {get; set;}
        public int CourseID {get; set;}
        public Department Department {get; set;}
        public Course Course {get; set;}
    }
}