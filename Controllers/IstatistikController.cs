using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class IstatistikController : Controller
    {
        // GET: Istatistik
        Context c = new Context();
        public ActionResult Index()
        {
            var toplamCari = c.Caris.Count().ToString();
            ViewBag.d1 = toplamCari;
            var toplamUrun = c.Uruns.Count().ToString();
            ViewBag.d2 = toplamUrun;
            var toplamPersonel = c.Personels.Count().ToString();
            ViewBag.d3 = toplamPersonel;
            var toplamKategori = c.Kategoris.Count().ToString();
            ViewBag.d4 = toplamKategori;
            var toplamStok = c.Uruns.Sum(x => x.Stok).ToString();
            ViewBag.d5 = toplamStok;
            var toplamMarka = c.Uruns.Select(x => x.Marka).Distinct().Count().ToString();
            ViewBag.d6 = toplamMarka;
            var kritikSeviye = c.Uruns.Count(x => x.Stok <= 10).ToString();
            ViewBag.d7 = kritikSeviye;
            var maxFiyatliUrun = (from x in c.Uruns orderby x.SatisFiyat descending select x.UrunAd).FirstOrDefault();
            ViewBag.d8 = maxFiyatliUrun;
            var minFiyatliUrun = (from x in c.Uruns orderby x.SatisFiyat select x.UrunAd).FirstOrDefault();
            ViewBag.d9 = minFiyatliUrun;
            var maxMarka = c.Uruns.GroupBy(x => x.Marka).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault();
            ViewBag.d12 = maxMarka;
            var enCokSatan = c.Uruns.Where(x => x.UrunId == (c.SatisHarekets.GroupBy(y => y.UrunId).OrderByDescending(z => z.Count()).Select(k => k.Key).FirstOrDefault())).Select(a => a.UrunAd).FirstOrDefault();
            ViewBag.d13 = enCokSatan;
            var kasadakiToplam = c.SatisHarekets.Sum(x => x.Tutar).ToString();
            ViewBag.d14 = kasadakiToplam;
            DateTime buGun = DateTime.Today;
            var gunlukSatis = c.SatisHarekets.Count(x => x.Tarih == buGun).ToString();
            ViewBag.d15 = gunlukSatis;
            var gunlukKasa = c.SatisHarekets
                    .Where(x => x.Tarih == buGun && x.Tutar != null) // Null değerleri filtrele
                    .Sum(y => (decimal?)y.Tutar) // Sum işleminden önce null olmayan bir dizi üzerinde çalışmasını sağla
                    ?.ToString() ?? "0"; // Sum işlemi sonucu null ise 0 olarak kabul et
            ViewBag.d16 = gunlukKasa;
            return View();

        }
        public ActionResult BasitTablolar()
        {
            var sorgu = from x in c.Caris
                        group x by x.CariSehir into g
                        select new SinifGrup
                        {
                            Sahir = g.Key,
                            Sayi = g.Count()
                        };
            return View(sorgu.ToList());
        }
        public PartialViewResult Partial1()
        {
            var sorgu2 = from x in c.Personels
                         group x by x.Departman.DepartmanAd into g
                         select new SinifGrup2
                         {
                             Departman = g.Key,
                             Sayi = g.Count()
                         };
            return PartialView(sorgu2.ToList());
        }
        public PartialViewResult Partial2()
        {
            var sorgu3 = c.Caris.ToList();
            return PartialView(sorgu3);
        }
        public PartialViewResult Partial3() { 
            var sorgu4 = c.Uruns.ToList();
            return PartialView(sorgu4);
        }
        public PartialViewResult Partial4()
        {
            var sorgu5 = from x in c.Uruns
                         group x by x.Marka into g
                         select new SinifGrup3
                         {
                             Marka = g.Key,
                             Sayi = g.Count()
                         };
            return PartialView(sorgu5.ToList());
        }
        public PartialViewResult Partial5()
        {
            var sorgu6 = c.Kategoris.ToList();
            return PartialView(sorgu6);
        }
    }
}