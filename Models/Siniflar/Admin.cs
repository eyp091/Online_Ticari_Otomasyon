using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string AdminAd { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string AdminSoyad { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string AdminMail { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(10)]
        public string KullaniciAdi { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string Sifre { get; set; }

        [Column(TypeName = "char")]
        [StringLength(1)]
        public string Yetki { get; set; }
    }
}