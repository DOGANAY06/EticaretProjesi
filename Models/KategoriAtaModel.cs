using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EticaretProjesi.Models
{
    public class KategoriAtaModel
    {//çoğa cok bir ilişki olduğu için ata yapmamız gerekiyor
        public int KategoriId { get; set; }
        public string KategoriAd { get; set; }

        public bool Varmi { get; set; }
    }
}
