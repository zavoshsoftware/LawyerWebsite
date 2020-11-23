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
    public class ServiceBlog : BaseEntity
    {

        public Guid ServiceId { get; set; }
        public virtual Service Service { get; set; }
        public Guid BlogId  { get; set; }
        public virtual Blog Blog { get; set; }

        internal class Configuration : EntityTypeConfiguration<ServiceBlog>
        {
            public Configuration()
            {
                HasRequired(p => p.Service)
                    .WithMany(j => j.ServiceBlogs)
                    .HasForeignKey(p => p.ServiceId);

                HasRequired(p => p.Blog)
                    .WithMany(j => j.ServiceBlogs)
                    .HasForeignKey(p => p.BlogId);
            }
        }
    }
}