using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace EticaretProjesi.Entities
{
    [Dapper.Contrib.Extensions.Table("Urunler")]
    //ürünü gördüpün zaman Urunler ara Türkçe kod yazdık normalde Uruns arar o 
    public class Urun:IEquatable<Urun>
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Ad { get; set; }
        [MaxLength(250)]
        public string Resim { get; set; }

        public decimal Fiyat { get; set; }

        public List<UrunKategori> UrunKategoriler { get; set; }

        public bool Equals([AllowNull] Urun other)
        //Geçerli nesnenin aynı türdeki başka bir nesneye eşit olup olmadığını gösterir.
        {//equals methodu ile nesne karşılaştırmalarını yaptık
            return Id == other.Id && Ad == other.Ad && Resim == other.Resim && Fiyat == other.Fiyat;

        }
    }
}
