using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EticaretProjesi.Models
{
    public class UrunEkleModel
    {//ürün eklemek için model 
        [Required(ErrorMessage = "Ad Alanı gereklidir")]
        public string Ad { get; set; }
        [Range(0,double.MaxValue,ErrorMessage = "Fiyat 0 dan büyük olmalıdır")] //GEREKLİLİK KONTROLÜ VALİDATİON
        public decimal Fiyat { get; set; }
        public IFormFile Resim { get; set; }

    }
}
