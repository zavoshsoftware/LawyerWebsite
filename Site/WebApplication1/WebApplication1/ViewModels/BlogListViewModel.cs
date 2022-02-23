using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class BlogListViewModel : _BaseViewModel
    {
        public BlogListViewModel()
        {
            Blogs = new List<Blog>();
        }
        public List<Blog> Blogs { get; set; }
    }
}