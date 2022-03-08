using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class AboutViewModel:_BaseViewModel
    {              
        public TextItem section1{ get; set; }
        public TextItem section2{ get; set; }
        public TextItem section3{ get; set; }
    }
}