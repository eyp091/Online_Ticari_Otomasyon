using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class KargoController : Controller
    {
        // GET: Kargo
        Context c = new Context();
        public ActionResult Index(string p)
        {
            var kargo = from x in c.KargoDetays select x;
            if (!string.IsNullOrEmpty(p))
            {
                kargo = kargo.Where(y => y.TakipKodu.Contains(p));
            }
            return View(kargo.ToList());
        }
        [HttpGet]
        public ActionResult YeniKargo()
        {
            Guid guid = Guid.NewGuid();
            string guidString = guid.ToString("N");
            string takipKodu = guidString.Substring(0, 10);
            ViewBag.takipkod = takipKodu;
            return View();
        }
        [HttpPost]
        public ActionResult YeniKargo(KargoDetay k)
        {
            c.KargoDetays.Add(k);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KargoTakip(string id) //-> RouteConfig dosyasında sayfalar arası veri aktarımı id ile tanımlandığı için değişkenin adı id olmak zorunda
        {
            var degerler = c.KargoTakips.Where(x => x.TakipKodu == id).ToList();
            return View(degerler);
        }
    }
}