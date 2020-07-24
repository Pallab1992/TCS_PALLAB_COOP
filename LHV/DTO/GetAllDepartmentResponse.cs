using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LHV.Model;
using System;
using Microsoft.EntityFrameworkCore;

namespace LHV.DTO
{
    public class GetAllDepartmentResponse
    {
        public int DepartmentIDResp {get; set;}
        public string DepartmentNameResp {get; set;}
        public string DepartmentCodeResp {get; set;}
        public string[] CourseNameResp {get; set;}
    }
}