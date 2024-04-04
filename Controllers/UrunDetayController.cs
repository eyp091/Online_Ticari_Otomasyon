using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class UrunDetayController : Controller
    {
        // GET: UrunDetay
        Context c = new Context();
        public ActionResult Index()
        {
            UrunlerDetay ud = new UrunlerDetay();
            ud.UrunBilgi = c.Uruns.Where(x => x.UrunId == 1).ToList();
            ud.DetayBilgi = c.Detays.Where(y => y.DetayId == 1).ToList();
            return View(ud);
        }
    }
}