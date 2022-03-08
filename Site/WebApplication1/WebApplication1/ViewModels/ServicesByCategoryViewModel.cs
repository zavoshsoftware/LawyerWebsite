using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class ServicesByCategoryViewModel:_BaseViewModel
    {
        public ServicesByCategoryViewModel()
        {
            ChildServices = new List<Service>();
        }
        public Service ParentService { get; set; }
        public List<Service> ChildServices { get; set; }
    }
}