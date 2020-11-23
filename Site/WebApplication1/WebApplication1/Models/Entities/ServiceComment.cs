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
    public class ServiceComment : BaseEntity
    {
        [Display(Name = "نام")]
        [StringLength(200, ErrorMessage = "طول {0} نباید بیشتر از {1} باشد")]
        public string Name { get; set; }

        [Display(Name = "ایمیل")]
        [StringLength(256, ErrorMessage = "طول {0} نباید بیشتر از {1} باشد")]
        public string Email { get; set; }

        [Display(Name = "نظر")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public string Message { get; set; }

        [Display(Name = "پاسخ")]
        [DataType(DataType.Html)]
        [AllowHtml]
        [Column(TypeName = "ntext")]
        public string Response { get; set; }
        public Guid ServiceId { get; set; }
        public virtual Service Service { get; set; }

        internal class Configuration : EntityTypeConfiguration<ServiceComment>
        {
            public Configuration()
            {
                HasRequired(p => p.Service)
                    .WithMany(j => j.ServiceComments)
                    .HasForeignKey(p => p.ServiceId);
            }
        }
    }
}