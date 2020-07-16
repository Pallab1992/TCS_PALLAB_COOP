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
        public int Stu_ID {get; set;}
        public string Stu_Name {get; set;}
        public int Stu_Roll {get; set;}
        public int DepartmentID {get; set;}
        public Department Department {get; set;}
    }
}