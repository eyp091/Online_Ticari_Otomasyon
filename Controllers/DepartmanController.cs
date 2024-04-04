using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class DepartmanController : Controller
    {
        // GET: Departman
        Context c = new Context();
        public ActionResult Index()
        {
            var degerler = c.Departmans.Where(x => x.Durum == true).ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult DepartmanEkle()
        {
            return View();
        }
        [Authorize(Roles ="A")]
        [HttpPost]
        public ActionResult DepartmanEkle(Departman d)
        {
            c.Departmans.Add(d);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DepartmanSil(int id)
        {
            var departman = c.Departmans.Find(id);
            departman.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DepartmanGetir(int id)
        {
            var departman = c.Departmans.Find(id);
            return View("DepartmanGetir",departman);
        }
        public ActionResult DepartmanGuncelle(Departman d)
        {
            var departman = c.Departmans.Find(d.DepartmanId);
            departman.DepartmanAd = d.DepartmanAd;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DepartmanDetay(int id)
        {
            var personel = c.Personels.Where(x => x.DepartmanId == id).ToList();
            var departman = c.Departmans.Where(x => x.DepartmanId != id).Select(y => y.DepartmanAd).FirstOrDefault();
            ViewBag.dpt = departman;
            return View(personel);
        }
        public ActionResult DepartmanPersonelSatis(int id)
        {
            var satis = c.SatisHarekets.Where(x => x.PersonelId == id).ToList();
            var personel = c.Personels.Where((x) => x.PersonelId == id).Select(y => y.PersonelAd + " " + y.PersonelSoyad).FirstOrDefault();
            ViewBag.prs = personel;
            return View(satis);
        }

    }
}