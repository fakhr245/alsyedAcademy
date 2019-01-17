using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace alsyedAcademy.Models
{
    public class Tutorial
    {
        public int id { get; set; }
        public string Title { get; set; }
        public  string link { get; set; }
        public string description { get; set; }
        public string tags { get; set; }
    }

    public class TutorialVM
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string link { get; set; }
        public string description { get; set; }
        public List<Tag> tags { get; set; }
        public List<Comment> commments { get; set;  }
    }
}