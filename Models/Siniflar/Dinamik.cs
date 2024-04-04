using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class Dinamik
    {
        public IEnumerable<Faturalar> faturalar { get; set; }
        public IEnumerable<FaturaKalem> faturaKalem { get; set; }
    }
}