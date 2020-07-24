using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LHV.Model;
using System;
using Microsoft.EntityFrameworkCore;

namespace LHV.DTO
{
    public class AssignCourseToStudentRequest
    {
        public string StudentRollReq {get; set;}
        public string CourseCodeReq {get; set;}
    }
}