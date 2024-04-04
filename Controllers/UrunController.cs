using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        Context c = new Context();
        public ActionResult Index(string p)
        {
            var urunler = from x in c.Uruns select x;
            if (!string.IsNullOrEmpty(p))
            {
                urunler = urunler.Where(y => y.UrunAd.Contains(p));
            }
            return View(urunler.ToList());
        }
        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> kategoriler = (from x in c.Kategoris.ToList()
                                                select new SelectListItem
                                                {
                                                    Text = x.KategoriAd,
                                                    Value = x.KategoriId.ToString()
                                                }).ToList();
            //Controllerdan view'a veri taşımak için kullanılan dinamik nesnedir.
            ViewBag.ktgr = kategoriler;
            return View();
        }
        [HttpPost]
        public ActionResult YeniUrun(Urun u)
        {
            c.Uruns.Add(u);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunSil(int id)
        {
            var urun = c.Uruns.Find(id);
            urun.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(int id)
        {
            List<SelectListItem> kategoriler = (from x in c.Kategoris.ToList()
                                                select new SelectListItem
                                                {
                                                    Text = x.KategoriAd,
                                                    Value = x.KategoriId.ToString()
                                                }).ToList();
            //Controllerdan view'a veri taşımak için kullanılan dinamik nesnedir.
            ViewBag.ktgr = kategoriler;
            var urun = c.Uruns.Find(id);
            return View("UrunGetir",urun);
        }
        public ActionResult UrunGuncelle(Urun u)
        {
            var urun = c.Uruns.Find(u.UrunId);
            urun.AlisFiyat = u.AlisFiyat;
            urun.SatisFiyat = u.SatisFiyat;
            urun.Stok = u.Stok;
            urun.UrunAd = u.UrunAd;
            urun.UrunGorsel = u.UrunGorsel;
            urun.Durum = u.Durum;
            urun.KategoriId = u.KategoriId;
            urun.Marka = u.Marka;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunListesi()
        {
            var urun = c.Uruns.ToList();
            return View(urun);
        }
        [HttpGet]
        public ActionResult SatisYap(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Personels.ToList() 
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd + " " + x.PersonelSoyad,
                                               Value = x.PersonelId.ToString()
                                           }).ToList();
            var deger2 = c.Uruns.Find(id);
            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2.UrunId;
            ViewBag.dgr3 = deger2.SatisFiyat;
            return View();
        }
        [HttpPost]
        public ActionResult SatisYap(SatisHareket p)
        {
            p.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            c.SatisHarekets.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index", "Satis");
        }
    }
}