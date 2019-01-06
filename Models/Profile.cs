using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace alsyedAcademy.Models
{
    public class Profile
    {
        public int id { get; set; }
        public string uid { get; set; }
        public string  fullName { get; set; }
        public string fatherName { get; set; }
        public string education { get; set; }
        public DateTime dob { get; set; }
        public string address { get; set; }
        public string profession { get; set; }
        public string photo { get; set; }
        public string phone { get; set; }
       
    }

    public class ProfileViewModel
    {
        public int id { get; set; }
        public string uid { get; set; }
        public string fullName { get; set; }
        public string fatherName { get; set; }
        public string email { get; set; }
        public string education { get; set; }
        public DateTime dob { get; set; }
        public string address { get; set; }
        public string profession { get; set; }
        public string photo { get; set; }
        public string phone { get; set; }
    }
}