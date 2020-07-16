using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LHV.Model;
using System;
using Microsoft.EntityFrameworkCore;

namespace LHV.DTO
{
    public class StudentDeptRecordResponse
    {
        public int StudentID {get; set;}  
        public string StudentName {get; set;}
        public int StudentRoll {get; set;}
        public string DepartmentName {get; set;}
    }
}