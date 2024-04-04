using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class CariPanelController : Controller
    {
        // GET: CariPanel
        Context c = new Context();
        [Authorize]
        public ActionResult Index()
        {
            var mail = (string)Session["CariMail"];
            var degerler = c.Mesajlars.Where(x => x.Alici == mail).ToList();
            var mailid = c.Caris.Where(x => x.CariMail == mail).Select(x => x.CariId).FirstOrDefault();
            var gecmisSiparis = c.SatisHarekets.Where(y => y.CariId == mailid).Count();
            ViewBag.gecmisSiparis = gecmisSiparis;
            var toplamTutar = c.SatisHarekets.Where(x => x.CariId == mailid).Sum(y => y.Tutar);
            ViewBag.toplamTutar = toplamTutar;
            var urunSayisi = c.SatisHarekets.Where(x => x.CariId == mailid).Sum(y => y.Adet);
            ViewBag.urunSayisi = urunSayisi;
            var adSoyad = c.Caris.Where(x => x.CariId == mailid).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.adSoyad = adSoyad;
            ViewBag.mail = mail;
            var sehir = c.Caris.Where(x => x.CariId == mailid).Select(y => y.CariSehir).FirstOrDefault();
            ViewBag.sehir = sehir;
            return View(degerler);
        }
        public ActionResult Siparislerim()
        {
            var mail = (string)Session["CariMail"];
            var id = c.Caris.Where(x => x.CariMail == mail.ToString()).Select(y => y.CariId).FirstOrDefault();
            var degerler = c.SatisHarekets.Where(x => x.CariId == id).ToList();
            if (degerler != null)
            {
                // degerler null değilse yapılacak işlemler
                foreach (var satis in degerler)
                {
                    // Siparişler üzerinde işlemler yapılabilir
                    Console.WriteLine($"Ürün Adı: {satis.Urun.UrunAd}, Fiyat: {satis.Fiyat}, Tarih: {satis.Tarih}");
                }
            }
            else
            {
                // degerler null ise yapılacak işlemler veya hata işleme
                Console.WriteLine("Siparişler bulunamadı veya bir hata oluştu.");
            }

            return View(degerler);
        }
        public ActionResult GelenMesaj()
        {
            var mail = (string)Session["CariMail"];
            var mesjlar = c.Mesajlars.Where(x => x.Alici == mail).OrderByDescending(y => y.MesajId).ToList();
            var gelensayisi = c.Mesajlars.Where(x => x.Alici == mail).Count().ToString();
            ViewBag.d1 = gelensayisi;
            var gidensayisi = c.Mesajlars.Where(x => x.Gonderen == mail).Count().ToString();
            ViewBag.d2 = gidensayisi;
            return View(mesjlar);
        }
        public ActionResult GonderilenMesaj()
        {
            var mail = (string)Session["CariMail"];
            var mesjlar = c.Mesajlars.Where(x => x.Gonderen == mail).OrderByDescending(y => y.MesajId).ToList();
            var gidensayisi = c.Mesajlars.Where(x => x.Gonderen == mail).Count().ToString();
            ViewBag.d2 = gidensayisi;
            var gelensayisi = c.Mesajlars.Where(x => x.Alici == mail).Count().ToString();
            ViewBag.d1 = gelensayisi;
            return View(mesjlar);
        }
        public ActionResult MesajDetay(int id)
        {
            var mail = (string)Session["CariMail"];
            var gidensayisi = c.Mesajlars.Where(x => x.Gonderen == mail).Count().ToString();
            ViewBag.d2 = gidensayisi;
            var gelensayisi = c.Mesajlars.Where(x => x.Alici == mail).Count().ToString();
            ViewBag.d1 = gelensayisi;
            var icerik = c.Mesajlars.Where(x => x.MesajId == id).ToList();
            return View(icerik);
        }
        [HttpGet]
        public ActionResult YeniMesaj()
        {
            var mail = (string)Session["CariMail"];
            var gidensayisi = c.Mesajlars.Where(x => x.Gonderen == mail).Count().ToString();
            ViewBag.d2 = gidensayisi;
            var gelensayisi = c.Mesajlars.Where(x => x.Alici == mail).Count().ToString();
            ViewBag.d1 = gelensayisi;
            return View();
        }
        [HttpPost]
        public ActionResult YeniMesaj(Mesajlar m)
        {
            var mail = (string)Session["CariMail"];
            m.Gonderen = mail;
            m.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            c.Mesajlars.Add(m);
            c.SaveChanges();
            return View();
        }
        public ActionResult LogOut()
        {
            // Oturumu sonlandır
            FormsAuthentication.SignOut();

            // Session'ı temizle
            Session.Clear();
            Session.Abandon();

            // Cookie'leri de temizle (opsiyonel)
            HttpCookie myCookie = new HttpCookie("UserInfo");
            myCookie.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(myCookie);

            // Yönlendirme yap
            return RedirectToAction("Index", "Login");
        }
        public PartialViewResult PartialDuzenle()
        {
            var mail = (string)Session["CariMail"];
            var id = c.Caris.Where(x => x.CariMail == mail).Select(y => y.CariId).FirstOrDefault();
            var caribul = c.Caris.Find(id);
            return PartialView("PartialDuzenle", caribul);
        }
        public PartialViewResult PartialDuyuru()
        {
            var admins = c.Admins.Select(x => x.KullaniciAdi).ToList();
            var mesajlar = c.Mesajlars.Where(x => admins.Contains(x.Gonderen)).OrderByDescending(y => y.MesajId).ToList();
            return PartialView(mesajlar);
        }
        public ActionResult CariGuncelle(Cari cr)
        {
            var cari = c.Caris.Find(cr.CariId);
            cari.CariAd = cr.CariAd;
            cari.CariSoyad = cr.CariSoyad;
            cari.CariMail = cr.CariMail;
            cari.CariSehir = cr.CariSehir;
            cari.CariSifre = cr.CariSifre;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KargoTakip(string p)
        {
            if (string.IsNullOrEmpty(p))
            {
                return View(new List<KargoDetay>());
            }

            var k = from x in c.KargoDetays select x;
            k = k.Where(x => x.TakipKodu.Contains(p));
            return View(k.ToList());
        }
        public ActionResult CariKargoTakip(string id)
        {
            var degerler = c.KargoTakips.Where(x => x.TakipKodu == id).ToList();
            return View(degerler);
        }
    }
}