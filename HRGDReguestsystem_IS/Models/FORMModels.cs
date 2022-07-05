using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRGDReguestsystem_IS.Models
{
    public class FORMModels
    {
        //[Required(ErrorMessage = "*")]
        public string TERDAFTAR { get; set; }

        //[Required(ErrorMessage = "*")]
        public string EMPLOYEESTATUS { get; set; }
        public string EMPLOYEESTATUS_FreeText { get; set; }

        //[Required(ErrorMessage = "*")]
        public string ENTITY { get; set; }

        [Required(ErrorMessage = "Please Fill The Unique ID")]
        [Display(Name = "Email")]
        //[RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
        //ErrorMessage = "Please Enter Correct E-mail Address Ex : abc@axa.co.id")]
        public string UNIQUEID { get; set; }

        //[Required(ErrorMessage = "*")]
        public string PERSONALTITLE { get; set; }

        //[Required(ErrorMessage = "*")]
        public string FIRSTNAME { get; set; }

        //[Required(ErrorMessage = "*")]
        public string LASTNAME { get; set; }
        public string COMPANY { get; set; }

        //[Required(ErrorMessage = "*")]
        public string SITEADD { get; set; }
        public string OFFICEFLOOR { get; set; }
        public string OFFICEFLOOR2 { get; set; }

        //[Required(ErrorMessage = "*")]
        public string CITY { get; set; }
        public string OFFICEPHONE { get; set; }
        //public string OFFICEPHONE
        //{
        //    get { return OFFICEPHONE ?? "Initial Value"; }
        //    set { OFFICEPHONE = value; }
        //}
        public string FAX { get; set; }

        //[Required(ErrorMessage = "*")]
        public string PROFESSIONALFAMILY { get; set; }

        //[Required(ErrorMessage = "*")]
        public string TITTLE { get; set; }

        //[Required(ErrorMessage = "*")]
        public string DEPART { get; set; }

        //[Required(ErrorMessage = "*")]
        public string MOBILE { get; set; }
        public string EMAIL { get; set; }

        //[Required(ErrorMessage = "*")]
        public string MANAGERNAME { get; set; }

        [Required(ErrorMessage = "Please Fill The Manager E-mail")]
        [Display(Name = "Email")]
        //[RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
        //ErrorMessage = "Please Enter Correct E-mail Address Ex : abc@axa.co.id")]
        public string MANAGEREMAIL { get; set; }

        //[Required(ErrorMessage = "*")]
        //[Required(ErrorMessage = "*")]
        [Display(Name = "EXPIRED")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EXPIRED { get; set; }
        public int idx { get; set; }
        public string emp_name { get; set; }
        public string emp_id { get; set; }
        public bool? isYES { get; set; }

        //[Required(ErrorMessage = "Please Fill The Remarks Admin")]
        public string remarksadmin { get; set; }
       


    }
}