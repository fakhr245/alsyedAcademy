using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace alsyedAcademy.Models
{
    public class News
    {
        public int id { get; set; }
        public string title { get; set; }
        public string photo { get; set; }
        public int status { get; set; } // 0 for off 1 for on        
        public DateTime dated { get; set; }        
        public string description { get; set; }
       // public List<Tag> tags { get; set; }
    }

    public class NewsVM
    {
        public int id { get; set; }
        public string title { get; set; }
        public HttpPostedFileBase photo { get; set; }
        public int status { get; set; } // 0 for off 1 for on      
        public DateTime dated { get; set; }           
        public string description { get; set; }
        public string tags { get; set; }
    }


    public class NewsVMTwo
    {
        public int id { get; set; }
        public string title { get; set; }
        public string photo { get; set; }
        public int status { get; set; } // 0 for off 1 for on      
        public DateTime dated { get; set; }
        public string description { get; set; }
        public string tags { get; set; }
    }

}