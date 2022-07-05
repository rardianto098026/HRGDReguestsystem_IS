using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRGDReguestsystem_IS.Models
{
    public class PagedListTR<T>
    {
        public List<T> Content { get; set; }
        public int CurrentPageIndex { get; set; }
        public int PageCount { get; set; }

        //SELECTION
        public string Emp_ID { get; set; }
        public string emp_name { get; set; }
        public string emp_numb { get; set; }
        public string Name { get; set; }
        public string Flag { get; set; }
        public string Entity { get; set; }
        public string No { get; set; }
        public string f_name { get; set; }
        public string l_name { get; set; }


        public List<SelectListItem> ENTITYY { get; internal set; }
        public List<SelectListItem> FLAGG { get; internal set; }
        
    }
}