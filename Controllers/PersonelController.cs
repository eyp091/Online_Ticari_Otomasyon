using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class PersonelController : Controller
    {
        // GET: Personel
        Context c = new Context();
        public ActionResult Index()
        {
            var personel = c.Personels.ToList();    
            return View(personel);
        }
        [HttpGet]
        public ActionResult PersonelEkle()
        {
            List<SelectListItem> departmanlar = (from x in c.Departmans.ToList()
                                                select new SelectListItem
                                                {
                                                    Text = x.DepartmanAd,
                                                    Value = x.DepartmanId.ToString()
                                                }).ToList();
            //Controllerdan view'a veri taşımak için kullanılan dinamik nesnedir.
            ViewBag.dprt = departmanlar;
            return View();
        }
        [HttpPost]
        public ActionResult PersonelEkle(Personel p)
        {
            c.Personels.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult PersonelGetir(int id)
        {
            List<SelectListItem> departmanlar = (from x in c.Departmans.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = x.DepartmanAd,
                                                     Value = x.DepartmanId.ToString()
                                                 }).ToList();
            //Controllerdan view'a veri taşımak için kullanılan dinamik nesnedir.
            ViewBag.dprt = departmanlar;
            var personel = c.Personels.Find(id);
            return View("PersonelGetir", personel);
        }
        public ActionResult PersonelGuncelle(Personel p)
        {
            var personel = c.Personels.Find(p.PersonelId);
            personel.PersonelAd = p.PersonelAd;
            personel.PersonelSoyad = p.PersonelSoyad;
            personel.PersonelGorsel = p.PersonelGorsel;
            personel.DepartmanId = p.DepartmanId;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult PersonleListe()
        {
            var personel = c.Personels.ToList();
            return View(personel);
        }
    }
}