using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        Context c = new Context();
        public ActionResult Index(int sayfa = 1)
        {
            var degerler = c.Kategoris.ToList().ToPagedList(sayfa, 5);
            return View(degerler);
        }
        [HttpGet]
        public ActionResult KategoriEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult KategoriEkle(Kategori k)
        {
            c.Kategoris.Add(k);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriSil(int id)
        {
            var kategori = c.Kategoris.Find(id);
            c.Kategoris.Remove(kategori);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriGetir(int id)
        {
            var kategori = c.Kategoris.Find(id);
            return View("KategoriGetir",kategori);
        }
        public ActionResult KategoriGuncelle(Kategori k)
        {
            var kategori = c.Kategoris.Find(k.KategoriId);
            kategori.KategoriAd = k.KategoriAd;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriUrun()
        {
            Select select = new Select();
            select.Kategoriler = new SelectList(c.Kategoris, "KategoriId", "KategoriAd");
            select.Urunler = new SelectList(c.Uruns, "UrunId", "UrunAd");
            return View(select);
        }
    }
}