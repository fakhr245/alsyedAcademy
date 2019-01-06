using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace alsyedAcademy.Models
{
    public class Tag
    {
        public int id { get; set; }
        public int fid { get; set; } // the id of the concern entity means either  news ot tutorial for search
        public int dealsWith { get; set; } // 0 for news 1 for tutorial
        public string text { get; set; } // the tag text.
       // public int News_id { get; set; }
    }
   
}