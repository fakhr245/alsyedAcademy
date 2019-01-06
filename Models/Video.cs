using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace alsyedAcademy.Models
{
    public class Video
    {
        public int id { get; set; }
        public string link { get; set; }
        public DateTime date { get; set; }
        public int status { get; set; } // 1 and 0 for avaible or not
    }
    public class VideoUser
    {
        public int id { get; set; }
        public int uid { get; set; }
        public int vid { get; set; } // video id 
        public int status { get; set; } // means payment is made for him will the lin is workable for him or not
    }
}