using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class ServicesViewModel : _BaseViewModel
    {
        public ServicesViewModel()
        {
            ParentServices = new List<Service>();
            ChildServices = new Dictionary<Guid, List<Service>>();
        }
        public List<Service> ParentServices { get; set; }
        public Dictionary<Guid,List<Service>> ChildServices { get; set; }
    }
}