using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace alsyedAcademy.Models
{
    public class Payment
    {
        public int id { get; set; }
        public DateTime dated { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string method { get; set; } // means payment by easy paisa direct bandk account or anything else
        public int uid { get; set; } //by the payment is made
        public double amount { get; set; }
    }

    public class CoursePayment
    {
        public int id { get; set; }
        public int cid { get; set; } //course id
        public int pid { get; set; } // payment id
    }
}