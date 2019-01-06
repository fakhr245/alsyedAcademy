using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace alsyedAcademy.Models
{
    public class Gallery
    {
        public int id { get; set; }
        public int eid { get; set; } // event ID
        public string path { get; set; }             
        public  int VideoPhoto { get; set; } // 0 for photo and 1 for video

    }

    public class Event
    {
        public int  id { get; set; }
        public string name { get; set; }
        public DateTime dated { get; set; }
        public string description { get; set; }
    }
    
    public class GalleryEventViewModel
    {
        public int eid { get; set; } // event id
        public string description { get; set; }
        public string name { get; set; }
        public DateTime dated { get; set; }
        public List<Gallery> VideoOrPhoto { get; set; }
    }
}