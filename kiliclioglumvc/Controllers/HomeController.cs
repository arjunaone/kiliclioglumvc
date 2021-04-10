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
            using(var db = new kilicliogluEntities())
            {
                IndexModel indexModel = new IndexModel();
                indexModel.productList = db.product.OrderBy(x => x.indexOrder).ToList();
                indexModel.noticeList = db.notice.ToList();
                return View(indexModel);
            }
        }
            
        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ProductDetails(int id)
        {
            DetailsModel detailsModel = new DetailsModel(); ;

            using (var db = new kilicliogluEntities())
            {
                detailsModel.product = db.product.Find(id);
                detailsModel.noticeList = db.notice.ToList();
            }

            return View(detailsModel);
        }
    }
}
