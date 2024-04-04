using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class FaturaController : Controller
    {
        // GET: Fatura
        Context c = new Context();
        public ActionResult Index()
        {
            var liste = c.Faturalars.ToList();
            return View(liste);
        }
        [HttpGet]
        public ActionResult FaturaEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FaturaEkle(Faturalar f)
        {
            c.Faturalars.Add(f);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult FaturaGetir(int id)
        {
            var fatura = c.Faturalars.Find(id);
            return View("FaturaGetir", fatura);
        }
        public ActionResult FaturaGuncelle(Faturalar f)
        {
            var fatura = c.Faturalars.Find(f.FaturaId);
            fatura.FaturaSeriNo = f.FaturaSeriNo;
            fatura.FaturaSıraNo = f.FaturaSıraNo;
            fatura.Tarih = f.Tarih;
            fatura.VergiDairesi = f.VergiDairesi;
            fatura.Saat = f.Saat;
            fatura.TeslimEden = f.TeslimEden;
            fatura.TeslimAlan = f.TeslimAlan;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult FaturaDetay(int id)
        {
            var kalemler = c.FaturaKalems.Where(x => x.FaturaId == id).ToList();
            return View(kalemler);
        }
        [HttpGet]
        public ActionResult YeniKalem()
        {
            List<SelectListItem> urunler = (from x in c.Uruns.ToList()
                                                select new SelectListItem
                                                {
                                                    Text = x.UrunAd,
                                                    Value = x.UrunAd
                                                }).ToList();
            ViewBag.urun = urunler;
            return View();
        }
        [HttpPost]
        public ActionResult YeniKalem(FaturaKalem fk)
        {
            c.FaturaKalems.Add(fk);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DinamikFatura()
        {
            Dinamik dinamik = new Dinamik();
            dinamik.faturalar = c.Faturalars.ToList();
            dinamik.faturaKalem = c.FaturaKalems.ToList();
            return View(dinamik);
        }
        public ActionResult FaturaKaydet(string FaturaSeriNo, string FaturaSıraNo, DateTime Tarih, string VergiDairesi, 
            string Saat, string TeslimEden, string TeslimAlan, string ToplamTutar, FaturaKalem[] kalemler)
        {
            Faturalar faturalar = new Faturalar();
            faturalar.FaturaSeriNo = FaturaSeriNo;
            faturalar.FaturaSıraNo = FaturaSıraNo;
            faturalar.Tarih = Tarih;
            faturalar.VergiDairesi = VergiDairesi;
            faturalar.Saat = Saat;
            faturalar.TeslimEden = TeslimEden;
            faturalar.TeslimAlan = TeslimAlan;
            faturalar.ToplamTutar = decimal.Parse(ToplamTutar);
            foreach(var x in kalemler)
            {
                FaturaKalem faturaKalem = new FaturaKalem();
                faturaKalem.Aciklama = x.Aciklama;
                faturaKalem.Miktar = x.Miktar;
                faturaKalem.BirimFiyat = x.BirimFiyat;
                faturaKalem.Tutar = x.Tutar;
                faturaKalem.FaturaId = x.FaturaId;
                c.FaturaKalems.Add(faturaKalem );
            }
            c.Faturalars.Add(faturalar);
            c.SaveChanges();
            return Json("İşlem Başarılı", JsonRequestBehavior.AllowGet);

        }
    }
}