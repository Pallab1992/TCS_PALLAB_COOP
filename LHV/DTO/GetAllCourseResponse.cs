using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LHV.Model;
using System;
using Microsoft.EntityFrameworkCore;

namespace LHV.DTO
{
    public class GetAllCourseResponse
    {
        public int CourseIDResp {get; set;}  
        public string CourseNameResp {get; set;}
        public List<string> AssignedDeptResp {get; set;}
    }
}