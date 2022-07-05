using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRGDReguestsystem_IS.Models
{
    public class ListGDModels
    {
        public IEnumerable<SelectListItem> ENTITYY { get; set; }
        public IEnumerable<SelectListItem> FLAGG { get; set; }
        public string Emp_ID { get; set; }
        public string Name { get; set; }
        public string Entity { get; set; }
        public string Req_For { get; set; }
        public string Flag { get; set; }
        public string idx { get; set; }
        public string conFlag { get; set; }
        public string no { get; set; }
        public string emp_name { get; set; }
    }
}