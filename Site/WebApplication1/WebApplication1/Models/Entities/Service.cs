using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Models
{
    public class Service : BaseEntity
    {
        public Service()
        {
            ServiceBlogs = new List<ServiceBlog>();
            Services = new List<Service>();
            ServiceComments = new List<ServiceComment>();
        }
        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Display(Name = "پارامتر صفحه")]
        public string UrlParam { get; set; }

        [Display(Name = "تصویر داخلی")]
        public string ImageUrl { get; set; }

         
        [Display(Name = "خلاصه")]
        public string Summery { get; set; }
        [Display(Name = "متن صفحه")]
        [DataType(DataType.Html)]
        [AllowHtml]
        [Column(TypeName = "ntext")]
        [UIHint("RichText")]
        public string Body { get; set; }

        [Display(Name = "اولویت نمایش")]
        public int? Order { get; set; }

        [Display(Name = "نمایش در صفحه اصلی")]
        public bool IsInHome { get; set; }

        public Guid? ParentId { get; set; }
        public virtual Service Parent { get; set; }
        public virtual ICollection<Service> Services { get; set; }
        public virtual ICollection<ServiceComment> ServiceComments { get; set; }
        
        public virtual ICollection<ServiceBlog> ServiceBlogs { get; set; }


        internal class Configuration : EntityTypeConfiguration<Service>
        {
            public Configuration()
            {
                HasRequired(p => p.Parent)
                    .WithMany(j => j.Services)
                    .HasForeignKey(p => p.ParentId);
            }
        }
    }
}