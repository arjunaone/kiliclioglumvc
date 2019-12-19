using kiliclioglumvc.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kiliclioglumvc.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            using(var db = new kilicliogluEntities())
            {
                if(db.user.Any(u => u.username == username && u.password == password))
                {
                    Session["username"] = username;
                    return RedirectToAction("menu", "admin");
                }
                Response.Write("Kullanıcı adı ve/veya şifre hatalı!");
                return View();
            }
        }

        public ActionResult Menu()
        {
            if(Session["username"] == null)
                return RedirectToAction("login", "admin");
            return View();
        }

        public ActionResult Notices()
        {
            if(Session["username"] == null)
                return RedirectToAction("login", "admin");
            List<notice> notices;
            using(var db = new kilicliogluEntities())
            {
                notices = db.notice.ToList();
            }
            return View(notices);
        }

        [HttpGet]
        public ActionResult CreateNotice()
        {
            if(Session["username"] == null)
                return RedirectToAction("login", "admin");
            return View();
        }

        [HttpPost]
        public ActionResult CreateNotice(string CreateNoticeTextarea)
        {
            using(var db = new kilicliogluEntities())
            {
                db.notice.Add(new notice { text = CreateNoticeTextarea });
                db.SaveChanges();
            }
            return RedirectToAction("notices", "admin");
        }

        [HttpGet]
        public ActionResult ModifyNotice(int id)
        {
            if(Session["username"] == null)
                return RedirectToAction("login", "admin");
            notice n;
            using(var db = new kilicliogluEntities())
            {
                n = db.notice.Find(id);
            }
            return View(n);
        }

        [HttpPost]
        public ActionResult ModifyNotice(int ModifyNoticeId, string ModifyNoticeTextarea)
        {
            using(var db = new kilicliogluEntities())
            {
                db.notice.Find(ModifyNoticeId).text = ModifyNoticeTextarea;
                db.SaveChanges();
            }
            return RedirectToAction("notices", "admin");
        }

        [HttpGet]
        public ActionResult DeleteNotice(int id)
        {
            if(Session["username"] == null)
                return RedirectToAction("login", "admin");
            notice n;
            using(var db = new kilicliogluEntities())
            {
                n = db.notice.Find(id);
                db.notice.Remove(n);
                db.SaveChanges();
            }
            return RedirectToAction("notices", "admin");
        }

        public ActionResult Products()
        {
            if(Session["username"] == null)
                return RedirectToAction("login", "admin");
            List<product> products;
            using(var db = new kilicliogluEntities())
            {
                products = db.product.ToList();
            }
            return View(products);
        }

        [HttpGet]
        public ActionResult CreateProduct()
        {
            if(Session["username"] == null)
                return RedirectToAction("login", "admin");

            ViewBag.Images = Directory.EnumerateFiles(Server.MapPath("~/img"))
                                .Select(fn => Path.GetFileName(fn));
            return View();
        }

        [HttpPost]
        public ActionResult CreateProduct(string name, string model, bool? availability, int? price, string imageSource, int? indexOrder, string details)
        {
            using(var db = new kilicliogluEntities())
            {
                db.product.Add(new product { name = name, model = model, availability = availability ?? false, price = price, imageSource = imageSource, indexOrder = indexOrder, details = details });
                db.SaveChanges();
            }
            return RedirectToAction("products", "admin");
        }

        [HttpGet]
        public ActionResult AddImage()
        {
            if(Session["username"] == null)
                return RedirectToAction("login", "admin");

            ViewBag.Images = Directory.EnumerateFiles(Server.MapPath("~/img"))
                                .Select(fn => Path.GetFileName(fn));

            return View();
        }

        [HttpPost]
        public ActionResult AddImage(HttpPostedFileBase file)
        {
            if(file != null)
            {
                string path = Path.Combine(Server.MapPath("~/img"),
                                       Path.GetFileName(file.FileName));

                file.SaveAs(path);
                ViewBag.Message = "Dosya başarıyla yüklendi.";
            }
            else
            {
                ViewBag.Message = "Dosya seçmediniz.";
            }

            ViewBag.Images = Directory.EnumerateFiles(Server.MapPath("~/img"))
                                .Select(fn => Path.GetFileName(fn));

            return View();
        }

        [HttpGet]
        public ActionResult DeleteImage()
        {
            ViewBag.Images = Directory.EnumerateFiles(Server.MapPath("~/img"))
                                .Select(fn => Path.GetFileName(fn));

            return View();
        }

        [HttpPost]
        public ActionResult DeleteImage(string imageSource)
        {
            string fullPath = Request.MapPath("~/img/" + imageSource);
            if(System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
                ViewBag.Message = "Dosya silindi.";
            }
            else
            {
                ViewBag.Message = "Dosya silinemedi.";
            }
            
            ViewBag.Images = Directory.EnumerateFiles(Server.MapPath("~/img"))
                                .Select(fn => Path.GetFileName(fn));

            return View();
        }

        [HttpGet]
        public ActionResult ModifyProduct(int id)
        {
            if(Session["username"] == null)
                return RedirectToAction("login", "admin");
            product n;

            using(var db = new kilicliogluEntities())
            {
                n = db.product.Find(id);
            }

            ViewBag.Images = Directory.EnumerateFiles(Server.MapPath("~/img"))
                                .Select(fn => Path.GetFileName(fn));

            return View(n);
        }

        [HttpPost]
        public ActionResult ModifyProduct(int ModifyProductId, string name, string model, bool? availability, int? price, string imageSource, int? indexOrder, string details)
        {
            using(var db = new kilicliogluEntities())
            {
                product p = db.product.Find(ModifyProductId);
                p.name = name;
                p.model = model;
                p.availability = availability ?? false;
                p.price = price;
                p.imageSource = imageSource;
                p.indexOrder = indexOrder;
                p.details = details;
                db.SaveChanges();
            }
            return RedirectToAction("products", "admin");
        }

        [HttpGet]
        public ActionResult DeleteProduct(int id)
        {
            if(Session["username"] == null)
                return RedirectToAction("login", "admin");
            product n;
            using(var db = new kilicliogluEntities())
            {
                n = db.product.Find(id);
                db.product.Remove(n);
                db.SaveChanges();
            }
            return RedirectToAction("products", "admin");
        }
    }
}