using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class Slide:BaseEntity
    {
        public string ImgUrl { get; set; }
        public string HeadTitle1 { get; set; }
        public string HeadTitle2 { get; set; }
        public string Headtext { get; set; }
        public string BtnLink { get; set; }
        public string BtnText { get; set; }
    }
}