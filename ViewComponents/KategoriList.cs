using EticaretProjesi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EticaretProjesi.ViewComponents
{
    public class KategoriList:ViewComponent
    {//asenkronik çalışır
     //
        private readonly IKategoryRepository _kategoriRepository;
        public KategoriList(IKategoryRepository kategoriRepository)
        {
            _kategoriRepository = kategoriRepository;
        }
        public IViewComponentResult Invoke()
        {//controllerden bagımsız kendi kendine çalışan bir yapı 
            return View(_kategoriRepository.GetirHepsi());
            
        }
    }
}
