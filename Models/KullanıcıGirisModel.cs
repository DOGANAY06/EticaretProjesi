using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EticaretProjesi.Models
{
    public class KullanıcıGirisModel
    {
        [Required(ErrorMessage = "Kullanıcı adı boş geçilemez")]
        public string KullaniciAd { get; set; }
        [Required(ErrorMessage = "Şifre boş geçilemez")]
        public string Sifre { get; set; }
        public bool BeniHatirla { get; set; }


    }
}
