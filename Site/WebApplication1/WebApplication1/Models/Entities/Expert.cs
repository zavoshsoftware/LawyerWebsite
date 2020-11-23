using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Models
{
    public class Expert:BaseEntity
    {
        [Display(Name = "اولویت نمایش")]
        public int Order { get; set; }
        [Display(Name="نام وکیل")]
        public string FullName { get; set; }
        [Display(Name="مدرک")]
        public string Degree { get; set; }

        [Display(Name="توضیحات رزومه")]
        [DataType(DataType.Html)]
        [AllowHtml]
        [Column(TypeName = "ntext")]
        [UIHint("RichText")]
        public string Body { get; set; }

        [Display(Name="تصویر")]
        public string ImageUrl { get; set; }

        [Display(Name="نمایش در صفحه اصلی")]
        public bool IsInHome { get; set; }

        [Display(Name="کد")]
        public int Code { get; set; }
      
    }
}