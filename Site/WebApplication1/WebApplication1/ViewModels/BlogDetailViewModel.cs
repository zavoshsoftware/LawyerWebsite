using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class BlogDetailViewModel : _BaseViewModel
    {
        public List<Service> SidebarServices { get; set; }
        public Blog Blog { get; set; }
        public List<Blog> SidebarBlog { get; set; }
        public List<BlogComment> BlogComments { get; set; }
    }
}