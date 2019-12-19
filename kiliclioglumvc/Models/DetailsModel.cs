using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kiliclioglumvc.Models
{
    public class DetailsModel
    {
        public product product { get; set; }
        public List<notice> noticeList { get; set; }
    }
}