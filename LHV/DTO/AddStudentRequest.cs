using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LHV.Model;
using System;
using Microsoft.EntityFrameworkCore;

namespace LHV.DTO
{
    public class AddStudentRequest
    {
        public string StudentNameReq {get; set;}
        public string StudentaddressReq {get; set;}
        public string StudentContactNoReq {get; set;}
        public int DepartmentIDReq {get; set;}
    }
}
