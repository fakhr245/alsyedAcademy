using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace alsyedAcademy.Models
{
    public class Addmission
    {
        public int id { get; set; }
        public int uid { get; set; }
        public DateTime date { get; set; }
        public int cid { get; set; } //course id
    }
}