using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class CariController : Controller
    {
        // GET: Cari
        Context c = new Context();
        public ActionResult Index()
        {
            var cariler = c.Caris.Where(x => x.Durum == true).ToList();

            return View(cariler);
        }
        [HttpGet]
        public ActionResult YeniCari() 
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniCari(Cari ca)
        {
            ca.Durum = true;
            c.Caris.Add(ca);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult CariSil(int id)
        {
            var cari = c.Caris.Find(id);
            cari.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult CariGetir(int id)
        {
            var cari = c.Caris.Find(id);
            return View("CariGetir",cari);
        }
        public ActionResult CariGuncelle(Cari ca)
        {
            var cari = c.Caris.Find(ca.CariId);
            cari.CariAd = ca.CariAd;
            cari.CariSoyad = ca.CariSoyad;
            cari.CariSehir = ca.CariSehir;
            cari.CariMail = ca.CariMail;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MusteriSatıs(int id)
        {
            var degerler = c.SatisHarekets.Where(x => x.CariId == id).ToList();
            var ca = c.Caris.Where(x => x.CariId == id).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.cari = ca;
            return View(degerler);
        }
    }
}