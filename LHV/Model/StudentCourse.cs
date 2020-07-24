using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LHV.Model
{
    public class StudentCourse
    {
        [Key]
        [Required]
        public int StudentCourseID {get; set;}
        public int StudentID {get; set;}
        public int CourseID {get; set;}
        public Student Student {get; set;}
        public Course Course {get; set;}
    }
}