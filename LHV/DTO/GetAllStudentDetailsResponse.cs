using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LHV.Model;
using System;
using Microsoft.EntityFrameworkCore;

namespace LHV.DTO
{
    public class GetAllStudentDetailsResponse
    {
        public int StudentIDResp {get; set;}  
        public string StudentNameResp {get; set;}
        public string StudentRegistrationResp {get; set;}
        public string StudentRollResp {get; set;}
        public string StudentContact {get; set;}
        public string DepartmentName {get; set;}
        public List<string> CourseNameResp {get; set;}
    }
}