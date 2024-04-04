using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class GrafikController : Controller
    {
        // GET: Grafik
        Context c = new Context();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult KategoriUrunSayisi()
        {
            ArrayList xvalue = new ArrayList();
            ArrayList yvalue = new ArrayList();

            var kategoriUrun = c.Kategoris.Select(x => new
            {
                kategoriAdi = x.KategoriAd,
                urunSayisi = x.Uruns.Count()
            }).ToList();

            foreach(var item in kategoriUrun)
            {
                xvalue.Add(item.kategoriAdi);
                yvalue.Add(item.urunSayisi);
            }

            var grafik = new Chart(width: 1300, height: 600)
                .AddTitle("Kategori - Ürün")
                .AddSeries(chartType:"Column", name:"Ürün Sayısı", xValue:xvalue, yValues:yvalue);

            return File(grafik.ToWebImage().GetBytes(), "image/jpeg");
        }
        public ActionResult UrunStok()
        {
            return View();
        }
        public ActionResult VisualizeUrunResult()
        {
            return Json(UrunListesi(), JsonRequestBehavior.AllowGet);
        }
        public List<Grafikler1> UrunListesi()
        {
            List<Grafikler1> grafikler1 = new List<Grafikler1>();
            
            using(Context c = new Context())
            {
                grafikler1 = c.Uruns.Select(x => new Grafikler1()
                {
                    urunAd = x.UrunAd,
                    stok = x.Stok
                }).ToList();
                return grafikler1;
            }
        }
    }
}