using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EticaretProjesi.Entities
{
    public class Kategori
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Ad { get; set; }

        public List<UrunKategori>  UrunKategoriler { get; set; }
        //1 kategorinin 1 den fazla ürünü olabilir
    }
}
