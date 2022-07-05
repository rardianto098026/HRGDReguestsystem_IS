using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRGDReguestsystem_IS.Models
{
    
        public class PagedList<T>
        {
            public List<T> Content { get; set; }

            //[Required(ErrorMessage = "*")]
            public string TERDAFTAR { get; set; }
            public IEnumerable<SelectListItem> TERDAFTARDDL { get; set; }

            //[Required(ErrorMessage = "*")]
            public string EMPLOYEESTATUS { get; set; }
            public string EMPLOYEESTATUS_FreeText { get; set; }
            public IEnumerable<SelectListItem> EMPLOYEESTATUSDDL { get; set; }

            //[Required(ErrorMessage = "*")]
            public string ENTITY { get; set; }
            public IEnumerable<SelectListItem> ENTITYDDL { get; set; }

            //[Required(ErrorMessage = "*")]
            public string PERSONALTITLE { get; set; }
            public IEnumerable<SelectListItem> PERSONALTITLEDDL { get; set; }

            //[Required(ErrorMessage = "*")]
            public string SITEADD { get; set; }
            public IEnumerable<SelectListItem> SITEADDDDL { get; set; }

            //[Required(ErrorMessage = "*")]
            public string CITY { get; set; }
            public IEnumerable<SelectListItem> CITYDDL { get; set; }


            //[Required(ErrorMessage = "*")]
            public string PROFESSIONALFAMILY { get; set; }
            public IEnumerable<SelectListItem> PROFESSIONALFAMILYDDL { get; set; }

            //[Required(ErrorMessage = "*")]
            public string TITTLE { get; set; }
            public IEnumerable<SelectListItem> TITTLEDDL { get; set; }

            //[Required(ErrorMessage = "*")]
            public string DEPART { get; set; }
            public IEnumerable<SelectListItem> DEPARTDDL { get; set; }

            [Required(ErrorMessage = "Please Fill The Unique ID")]
            [Display(Name = "Email")]
            [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
            ErrorMessage = "Please Enter Correct E-mail Address (Ex : abc@axa.co.id)")]
            public string UNIQUEID { get; set; }

            //[Required(ErrorMessage = "*")]
            public string FIRSTNAME { get; set; }

            //[Required(ErrorMessage = "*")]
            public string LASTNAME { get; set; }
            public string OFFICEFLOOR { get; set; }
            public string OFFICEFLOOR2 { get; set; }
            public IEnumerable<SelectListItem> OFFICEFLOORDDL{ get; set; }
            public string OFFICEPHONE { get; set; }
            public IEnumerable<SelectListItem> OFFICEPHONEDDL { get; set; }
            public string FAX { get; set; }
            public IEnumerable<SelectListItem> FAXDDL { get; set; }

            //[Required(ErrorMessage = "*")]
            public string MOBILE { get; set; }
            public string EMAIL { get; set; }

            //[Required(ErrorMessage = "*")]
            public string MANAGERNAME { get; set; }
            public IEnumerable<SelectListItem> MANAGERNAMEDDL { get; set; }

            [Required(ErrorMessage = "Please Fill The Manager E-mail")]
            [Display(Name = "Email")]
            [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
            ErrorMessage = "Please Enter Correct Email Address (Ex : abc@axa.co.id)")]
            public string MANAGEREMAIL { get; set; }

            //[Required(ErrorMessage = "*")]
          
            public string EXPIRED { get; set; }
            //public DateTime? EXPIRED { get; set; }
            public string COMPANY { get; set; }
            public IEnumerable<SelectListItem> COMPANYDDL { get; set; }
            
            public int idx { get; set; }
            public string emp_name { get; set; }
            public string emp_id { get; set; }
            public bool? isYES { get; set; }

            public string remarksadmin { get; set; }
        


    }

}