using EticaretProjesi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EticaretProjesi.ViewComponents
{
    public class UrunList:ViewComponent
    {
        private readonly IUrunRepository _urunRepository;
        public UrunList(IUrunRepository urunRepository)
        {
            _urunRepository = urunRepository;
        }
        public IViewComponentResult Invoke(int? KategoriId) {
            if (KategoriId.HasValue)
            {
                return View(_urunRepository.GetirKategoriIdile((int)KategoriId));
                //basılan kategori dönmesi için yapıyoruz 
            }
            return View(_urunRepository.GetirHepsi());
        }
    }
}
