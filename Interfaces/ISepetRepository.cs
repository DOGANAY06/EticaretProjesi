using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EticaretProjesi.Entities;

namespace EticaretProjesi.Interfaces
{
    public interface ISepetRepository
    {
        void SepeteEkle(Urun urun);
        void SepettenCikar(Urun urun);
        List<Urun> GetirSepettekiUrunler();
        void SepetiBosalt();
    }
}
