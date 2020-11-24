using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class HomeViewModel : _BaseViewModel
    {
        public List<Service> HomeServices { get; set; }
        public List<Expert> HomeExperts { get; set; }
        public List<Blog> HomeBlogs { get; set; }
    }
}