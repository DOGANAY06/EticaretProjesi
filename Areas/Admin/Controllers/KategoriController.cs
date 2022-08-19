using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EticaretProjesi.Entities;
using EticaretProjesi.Interfaces;
using EticaretProjesi.Models;

namespace EticaretProjesi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class KategoriController : Controller
    {
        
        private readonly IKategoryRepository _kategoryRepository;

        public KategoriController(IKategoryRepository kategoryRepository)
        {
            _kategoryRepository = kategoryRepository;
        }

        public IActionResult Index()
        {
            return View(_kategoryRepository.GetirHepsi());
        }

        public IActionResult Ekle()
        {
            return View(new KategoriEkleModel());
        }

        [HttpPost]
        public IActionResult Ekle(KategoriEkleModel model)
        {
            if (ModelState.IsValid)
            {
                _kategoryRepository.Ekle(new Kategori
                {
                    Ad = model.Ad
                });
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Guncelle(int id)
        {
            var guncellenecekKategori = _kategoryRepository.GetirIdile(id);
            KategoriGuncelleModel model = new KategoriGuncelleModel
            {
                Id=guncellenecekKategori.Id,
                Ad = guncellenecekKategori.Ad
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Guncelle(KategoriGuncelleModel model)
        {
            if (ModelState.IsValid)
            {
                var guncellenecekKategori = _kategoryRepository.GetirIdile(model.Id); //id alıcak 
                guncellenecekKategori.Ad = model.Ad;
                //modelden gelen ad
                _kategoryRepository.Guncelle(guncellenecekKategori);
                return RedirectToAction("Index");
            }
              
            return View(model);
        }

        public IActionResult Sil(int id)
        {
            _kategoryRepository.Sil(new Kategori{Id = id});
            return RedirectToAction("Index");
        }
    }
}
