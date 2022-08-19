using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EticaretProjesi.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace EticaretProjesi.TagHelpers
{
    [HtmlTargetElement("getirKategoriAd")]  //cshtml tarafında hangi etiket ile yakalamamız gerektiğini belirtir.
    public class KategoriAd: TagHelper
    {
        private readonly IUrunRepository _urunRepository;

        public KategoriAd(IUrunRepository urunRepository)
        {
            _urunRepository = urunRepository;
        }    


        //biizm kategori adı getirmemiz için ürünId İHTİYACÇ VAR 
        public int UrunId { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {//taghelper process i ovveride ederek yeni bir taghelper oluşturabiliriz

            
            string data = "";
            var gelenKategoriler = _urunRepository.GetirKategoriler(UrunId).Select(I => I.Ad); //KATEGORİLERİN adlarını getirsin urunıd göre   
            foreach (var item in gelenKategoriler)
            {
                data += item+" "; //data stringine item yazıcaz gelen kategori ismini 
            }

            output.Content.SetContent(data);
        }
    }
}
