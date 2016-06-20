using kiliclioglumvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kiliclioglumvc.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            using(var pc = new kilicliogluEntities())
            {
                IndexModel indexModel = new IndexModel();
                indexModel.productList = pc.product.ToList();
                indexModel.noticeList = pc.notice.ToList();
                return View(indexModel);
            }
        }
            
        public ActionResult Contact()
        {
            return View();
        }
    }
}