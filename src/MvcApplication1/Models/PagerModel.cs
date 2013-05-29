using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class PagerModel
    {
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalItemCount { get; set; }
    }

}