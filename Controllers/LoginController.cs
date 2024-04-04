using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        // GET: Login
        Context c = new Context();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public PartialViewResult Partial1()
        {
            return PartialView();
        }
        [HttpPost]
        public PartialViewResult Partial1(Cari ca)
        {
            c.Caris.Add(ca);
            c.SaveChanges();
            return PartialView();
        }
        [HttpGet]
        public ActionResult CariLogin()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult CariLogin(Cari ca)
        {
            var bilgi = c.Caris.FirstOrDefault(x => x.CariMail == ca.CariMail && x.CariSifre == ca.CariSifre);
            if(bilgi != null)
            {
                FormsAuthentication.SetAuthCookie(bilgi.CariMail, false);
                Session["CariMail"] = bilgi.CariMail.ToString();
                return RedirectToAction("Index", "CariPanel");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        [HttpGet]
        public ActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminLogin(Admin p)
        {
            var bilgiler = c.Admins.FirstOrDefault(x => x.KullaniciAdi == p.KullaniciAdi && x.Sifre == p.Sifre);
            if(bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.KullaniciAdi, false);
                Session["KullaniciAdi"] = bilgiler.KullaniciAdi.ToString();
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        [HttpGet]
        public ActionResult AdminKayit()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminKayit(Admin p)
        {
            c.Admins.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index", "Kategori");
        }
    }
}