using EticaretProjesi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EticaretProjesi.Interfaces
{
    public interface IUrunRepository : IGenericRepository<Urun>
    {
        List<Kategori> GetirKategoriler(int urunId); //bir liste olarak bunu ele alabilicez
        void EkleKategori(UrunKategori urunKategori);
        void SilKategori(UrunKategori urunKategori);
        List<Urun> GetirKategoriIdile(int KategoriId);
       
    }
}
