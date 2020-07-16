using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LHV.Model
{
    public class Department
    {
        [Key]
        [Required]
        public int DepartmentID {get; set;}
        public string DeptName {get; set;}

        public ICollection<Student> Students {get; set;}
    }
}