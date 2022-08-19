using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EticaretProjesi.Entities
{
    public class UrunKategori
    {
        public int Id { get; set; }
        public int UrunId { get; set; }
        public Urun Urun { get; set; }
        //üründen 1 ürün alsın
        public int KategoriId { get; set; }
        public Kategori Kategori { get; set; }

    }
}
