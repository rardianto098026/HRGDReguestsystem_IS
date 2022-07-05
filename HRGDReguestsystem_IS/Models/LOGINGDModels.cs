using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HRGDReguestsystem_IS.Models
{
    public class LOGINGDModels
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "username is required.")]
        public string username { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "ID Password is required.")]
        public string password { get; set; }
        public string IDMahasiswa { get; set; }
    }
}