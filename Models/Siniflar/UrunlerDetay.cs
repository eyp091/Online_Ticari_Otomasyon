using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class UrunlerDetay
    {
        public IEnumerable<Urun> UrunBilgi { get; set; }
        public IEnumerable<Detay> DetayBilgi { get; set; }
    }
}