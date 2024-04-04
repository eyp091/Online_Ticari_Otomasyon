using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        Context c = new Context();

        [Authorize]
        public ActionResult Index()
        {
            var kullaniciAdi = (string)Session["KullaniciAdi"];
            var mail = c.Admins.Where(x => x.KullaniciAdi == kullaniciAdi).Select(y => y.AdminMail).FirstOrDefault();
            var adminId = c.Admins.Where(x => x.KullaniciAdi == kullaniciAdi).Select(y => y.AdminId).FirstOrDefault();
            var degerler = c.Mesajlars.Where(x => x.Alici == mail).ToList();
            var adsoyad = c.Admins.Where(x => x.AdminId == adminId).Select(y => y.AdminAd + " " + y.AdminSoyad).FirstOrDefault();
            ViewBag.adsoyad = adsoyad;
            ViewBag.mail = mail;
            return View(degerler);
        }
        public PartialViewResult PartialDuyuru()
        {
            
            var admins = c.Admins.Select(x => x.KullaniciAdi).ToList();
            var duyuru = c.Mesajlars.Where(x => admins.Contains(x.Gonderen)).OrderByDescending(y => y.MesajId).ToList();
            return PartialView(duyuru);
        }
        public PartialViewResult PartialDuzenle()
        {
            var kullaniciAdi = (string)Session["KullaniciAdi"];
            var adminId = c.Admins.Where(x => x.KullaniciAdi == kullaniciAdi).Select(y => y.AdminId).FirstOrDefault();
            var adminbul = c.Admins.Find(adminId);
            return PartialView("PartialDuzenle", adminbul);
        }
        [HttpGet]
        public ActionResult YeniDuyuru()
        {
            var kullaniciAdi = (string)Session["KullaniciAdi"];
            return View();
        }
        [HttpPost]
        public ActionResult YeniDuyuru(Mesajlar m)
        {
            var kullaniciAdi = (string)Session["KullaniciAdi"];
            m.Gonderen = kullaniciAdi;
            m.Alici = kullaniciAdi;
            m.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            c.Mesajlars.Add(m);
            c.SaveChanges();
            return RedirectToAction("Index", "Admin");
        }
        public ActionResult AdminGuncelle(Admin ad)
        {
            var admin = c.Admins.Find(ad.AdminId);
            admin.AdminAd = ad.AdminAd;
            admin.AdminSoyad = ad.AdminSoyad;
            admin.KullaniciAdi = ad.KullaniciAdi;
            admin.AdminMail = ad.AdminMail;
            admin.Sifre = ad.Sifre;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult GelenMesaj()
        {
            var kullaniciAdi = (string)Session["KullaniciAdi"];
            var mail = c.Admins.Where(x => x.KullaniciAdi == kullaniciAdi).Select(y => y.AdminMail).FirstOrDefault();
            var mesajlar = c.Mesajlars.Where(x => x.Alici == mail).OrderByDescending(y => y.MesajId).ToList();
            var gelensayisi = c.Mesajlars.Where(x => x.Alici == mail).Count().ToString();
            var gidensayisi = c.Mesajlars.Where(x => x.Gonderen == mail).Count().ToString();
            ViewBag.d1 = gelensayisi;
            ViewBag.d2 = gidensayisi;
            return View(mesajlar);
        }
        public ActionResult GonderilenMesaj()
        {
            var kullaniciAdi = (string)Session["KullaniciAdi"];
            var mail = c.Admins.Where(x => x.KullaniciAdi == kullaniciAdi).Select(y => y.AdminMail).FirstOrDefault();
            var mesajlar = c.Mesajlars.Where(x => x.Gonderen == mail).OrderByDescending(y => y.MesajId).ToList();
            var gelensayisi = c.Mesajlars.Where(x => x.Alici == mail).Count().ToString();
            var gidensayisi = c.Mesajlars.Where(x => x.Gonderen == mail).Count().ToString();
            ViewBag.d1 = gelensayisi;
            ViewBag.d2 = gidensayisi;
            return View(mesajlar);
        }
        public ActionResult MesajDetay(int id)
        {
            var kullaniciAdi = (string)Session["KullaniciAdi"];
            var mail = c.Admins.Where(x => x.KullaniciAdi == kullaniciAdi).Select(y => y.AdminMail).FirstOrDefault();
            var icerik = c.Mesajlars.Where(x => x.MesajId == id).ToList();
            var gelensayisi = c.Mesajlars.Where(x => x.Alici == mail).Count().ToString();
            var gidensayisi = c.Mesajlars.Where(x => x.Gonderen == mail).Count().ToString();
            ViewBag.d1 = gelensayisi;
            ViewBag.d2 = gidensayisi;
            return View(icerik);
        }
        [HttpGet]
        public ActionResult YeniMesaj()
        {
            var kullaniciAdi = (string)Session["KullaniciAdi"];
            var mail = c.Admins.Where(x => x.KullaniciAdi == kullaniciAdi).Select(y => y.AdminMail).FirstOrDefault();
            var gelensayisi = c.Mesajlars.Where(x => x.Alici == mail).Count().ToString();
            var gidensayisi = c.Mesajlars.Where(x => x.Gonderen == mail).Count().ToString();
            ViewBag.d1 = gelensayisi;
            ViewBag.d2 = gidensayisi;
            return View();
        }
        [HttpPost]
        public ActionResult YeniMesaj(Mesajlar m)
        {
            var kullaniciAdi = (string)Session["KullaniciAdi"];
            var mail = c.Admins.Where(x => x.KullaniciAdi == kullaniciAdi).Select(y => y.AdminMail).FirstOrDefault();
            m.Gonderen = mail;
            m.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            c.Mesajlars.Add(m);
            c.SaveChanges();
            return View();
        }
    }
}