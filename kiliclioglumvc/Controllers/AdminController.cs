using kiliclioglumvc.Models;
using System;
using System.Collections.Generic;
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
            notice n;
            using(var db = new kilicliogluEntities())
            {
                n = db.notice.Find(id);
                db.notice.Remove(n);
                db.SaveChanges();
            }
            return RedirectToAction("notices", "admin");
        }
    }
}