using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class ConsultantViewModel : _BaseViewModel
    {
        public TextItem section1 { get; set; }
        public TextItem section2 { get; set; }
        public TextItem section3 { get; set; }
    }
}