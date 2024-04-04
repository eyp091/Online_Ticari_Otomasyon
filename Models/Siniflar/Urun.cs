using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class Urun
    {
        [Key]
        public int UrunId { get; set; }

        [Column(TypeName ="Varchar")]
        [StringLength(30)]
        public string UrunAd { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string Marka { get; set; }
        public short Stok { get; set; }
        public decimal AlisFiyat { get; set; }
        public decimal SatisFiyat { get; set; }
        public bool Durum { get; set; }
        public string UrunGorsel { get; set; }
        public int KategoriId { get; set; }

        //İlişkiler:
        public ICollection<SatisHareket> SatisHarekets { get; set; }
        //virtual, Kategori adında bir sınıfın bir örneği (instance) bu özellik aracılığıyla diğer bir sınıfa bağlanabilir.
        public virtual Kategori Kategori { get; set; }
    }
}