using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kiliclioglumvc.Models
{
    public class IndexModel
    {
        public List<product> productList { get; set; }
        public List<notice> noticeList { get; set; }
    }
}