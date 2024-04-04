using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class SatisController : Controller
    {
        // GET: Satis
        Context c = new Context();
        public ActionResult Index()
        {
            var satislar = c.SatisHarekets.ToList();
            return View(satislar);
        }
        [HttpGet]
        public ActionResult YeniSatis()
        {
            List<SelectListItem> urunler = (from x in c.Uruns.ToList()
                                            select new SelectListItem
                                            {
                                                Text = x.UrunAd,
                                                Value = x.UrunId.ToString()
                                            }).ToList();
            List<SelectListItem> cariler = (from x in c.Caris.ToList()
                                            select new SelectListItem
                                            {
                                                Text = x.CariAd + " " + x.CariSoyad,
                                                Value = x.CariId.ToString()
                                            }).ToList();
            List<SelectListItem> personeller = (from x in c.Personels.ToList()
                                            select new SelectListItem
                                            {
                                                Text = x.PersonelAd + " " + x.PersonelSoyad,
                                                Value = x.PersonelId.ToString()
                                            }).ToList();
            ViewBag.urn = urunler;
            ViewBag.cr = cariler;
            ViewBag.prsn = personeller;
            return View();
        }
        [HttpPost]
        public ActionResult YeniSatis(SatisHareket s)
        {
            s.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            c.SatisHarekets.Add(s);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SatisGetir(int id)
        {
            List<SelectListItem> urunler = (from x in c.Uruns.ToList()
                                            select new SelectListItem
                                            {
                                                Text = x.UrunAd,
                                                Value = x.UrunId.ToString()
                                            }).ToList();
            List<SelectListItem> cariler = (from x in c.Caris.ToList()
                                            select new SelectListItem
                                            {
                                                Text = x.CariAd + " " + x.CariSoyad,
                                                Value = x.CariId.ToString()
                                            }).ToList();
            List<SelectListItem> personeller = (from x in c.Personels.ToList()
                                                select new SelectListItem
                                                {
                                                    Text = x.PersonelAd + " " + x.PersonelSoyad,
                                                    Value = x.PersonelId.ToString()
                                                }).ToList();
            ViewBag.urn = urunler;
            ViewBag.cr = cariler;
            ViewBag.prsn = personeller;
            var satis = c.SatisHarekets.Find(id);
            return View("SatisGetir", satis);
        }
        public ActionResult SatisGuncelle(SatisHareket s)
        {
            var satis = c.SatisHarekets.Find(s.SatisId);
            satis.UrunId = s.UrunId;
            satis.CariId = s.CariId;
            satis.PersonelId = s.PersonelId;
            satis.Adet = s.Adet;
            satis.Tutar = s.Tutar;
            satis.Fiyat = s.Fiyat;
            satis.Tarih = s.Tarih;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SatisDetay(int id)
        {
            var satis = c.SatisHarekets.Where(x => x.SatisId == id).ToList();
            return View(satis);
        }
    }
}