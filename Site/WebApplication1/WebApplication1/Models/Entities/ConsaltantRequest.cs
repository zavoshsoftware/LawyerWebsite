using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class ConsaltantRequest:BaseEntity
    {
        [Display(Name="نام")]
        public string FullName { get; set; }

        [Display(Name = "ایمیل")]
        public string Email { get; set; }
        [Display(Name = "موضوع")]
        public string Subject { get; set; }

        [Display(Name = "شماره موبایل")]
        public string CellNumber { get; set; }
        [Display(Name = "متن درخواست")]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }
        [Display(Name = "پاسخ درخواست")]
        [DataType(DataType.MultilineText)]
        public string Response { get; set; }
    }
}