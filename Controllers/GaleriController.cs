using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class GaleriController : Controller
    {
        // GET: Galeri
        Context c = new Context();
        public ActionResult Index()
        {
            var urunler = c.Uruns.ToList();
            return View(urunler);
        }
    }
}