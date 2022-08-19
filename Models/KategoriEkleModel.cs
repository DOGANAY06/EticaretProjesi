using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EticaretProjesi.Models
{
    public class KategoriEkleModel
    {
        [Required(ErrorMessage = "Ad alanı boş bırakılamaz")]
        public string Ad { get; set; }
    }
}
