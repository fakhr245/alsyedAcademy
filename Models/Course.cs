using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace alsyedAcademy.Models
{
    public class Course
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string duration { get; set; }
        public double fee { get; set; }
        public int status { get; set; } //available or not 1 and 0
        public string photo { get; set; }

    }
    public class CourseVM
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string duration { get; set; }
        public double fee { get; set; }
        public int status { get; set; } //available or not 1 and 0
        public HttpPostedFileBase photo { get; set; }

    }

    public class CourseOutlineViewModel
    {
        public int id { get; set; }
        public string  name { get; set; }
        public List<Outline> outlines { get; set; }


    }
    public class Outline
    {
        public int id { get; set; }
        public int cid { get; set; } // course id
        public string description { get; set; }
        
    }
}