using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace alsyedAcademy.Models
{
    public class FreeStudents
    {
        public int id { get; set; }
        public string name { get; set; }
        public string workingAt { get; set; }
        public string photo { get; set; }
        public string Designation { get; set; }
    }

    public class FreeStudentsVM
    {
        public int id { get; set; }
        public string name { get; set; }
        public string workingAt { get; set; }
        public HttpPostedFileBase photo { get; set; }
        public string Designation { get; set; }
    }
}