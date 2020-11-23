using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Models; 

//using ViewModels;

namespace Helpers
{
    public class BaseViewModelHelper
    {
        private DatabaseContext db = new DatabaseContext();

        public List<Service> GetMenuService()
        {
          
            return db.Services.Where(c=>c.IsDeleted==false&&c.IsActive).OrderBy(c=>c.Order).ToList();
        }

    

        public List<Blog> GetFooterBlogs()
        {
          
            return db.Blogs.Where(c=>c.IsDeleted==false&&c.IsActive).OrderByDescending(c=>c.CreationDate).Take(3).ToList();
        }

    

        public string GetTextItemByName(string name)
        {
            TextItem textItem = db.TextItems.FirstOrDefault(c => c.Name == name);
            if (textItem != null)
                return textItem.Summery;

            return string.Empty;
        }

    }
}