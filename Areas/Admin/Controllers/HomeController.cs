using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EticaretProjesi.Entities;
using EticaretProjesi.Interfaces;
using EticaretProjesi.Models;
using Microsoft.AspNetCore.Authorization;

namespace EticaretProjesi.Areas.Admin.Controllers
{
    [Area("Admin")] //admin için alan
    [Authorize] //sadece giriş yapmış kişiler erişebilir
    public class HomeController : Controller
    {
        private readonly IUrunRepository _urunRepository;
        private readonly IKategoryRepository _kategoriKategoryRepositoryRepository; //tüm kategorileri getirmek için
        public HomeController(IUrunRepository urunRepository, IKategoryRepository kategoryRepository)
        {
            _urunRepository = urunRepository;
            _kategoriKategoryRepositoryRepository = kategoryRepository;
        }

        public IActionResult Index()
        {
            return View(_urunRepository.GetirHepsi());
            //bütün ürün listesi mevcut
        }

        public IActionResult Ekle()
        {
            return View(new UrunEkleModel());
        }

        [HttpPost]
        public IActionResult Ekle(UrunEkleModel model)
        {
            if (ModelState.IsValid)
            {
                Urun urun = new Urun(); //yeni bir ürün nesnesi yarattık
                if (model.Resim!=null)
                {
                    var uzanti = Path.GetExtension(model.Resim.FileName);
                    
                    //sadece jpeg türünde alsın
                        var yeniResimAd = Guid.NewGuid() + uzanti;
                        var yuklenecekYer = Path.Combine(Directory.GetCurrentDirectory()
                            , "wwwroot/img/" + yeniResimAd); 
                        //resmin koyulacağı yeri combine ettik birleştirdik yani
                        var stream = new FileStream(yuklenecekYer, FileMode.Create);
                        model.Resim.CopyTo(stream);
                        urun.Resim = yeniResimAd;
                    
                }

                
                urun.Ad = model.Ad;
                urun.Fiyat = model.Fiyat;
                
                _urunRepository.Ekle(urun); //urun ekleme işlemi için 
                return RedirectToAction("Index", "Home", new { area = "Admin" });
                //resim yüklendikten sonra Indexe gitsin
            }

            return View(model);
        }

        public IActionResult Guncelle(int id)
        {
           var gelenUrun = _urunRepository.GetirIdile(id);
           UrunGuncelleModel model = new UrunGuncelleModel
           {
               Ad = gelenUrun.Ad,
               Fiyat = gelenUrun.Fiyat,
               
               Id = gelenUrun.Id

           };
           return View(model);
        }
        [HttpPost]
        public IActionResult Guncelle(UrunGuncelleModel model)
        {
            if (ModelState.IsValid)
            {
                var guncellenecekUrun = _urunRepository.GetirIdile(model.Id);
                //guncellenecek urunu Id ile alıyoruz
                if (model.Resim != null)
                {
                    var uzanti = Path.GetExtension(model.Resim.FileName);

                    //sadece jpeg türünde alsın
                    var yeniResimAd = Guid.NewGuid() + uzanti;
                    var yuklenecekYer = Path.Combine(Directory.GetCurrentDirectory()
                        , "wwwroot/img/" + yeniResimAd);
                    //resmin koyulacağı yeri combine ettik birleştirdik yani
                    var stream = new FileStream(yuklenecekYer, FileMode.Create);
                    model.Resim.CopyTo(stream);
                    guncellenecekUrun.Resim = yeniResimAd;

                }

                
                guncellenecekUrun.Ad = model.Ad;
                guncellenecekUrun.Fiyat = model.Fiyat;

                _urunRepository.Guncelle(guncellenecekUrun); //urun ekleme işlemi için 
                return RedirectToAction("Index", "Home", new { area = "Admin" });
                //resim yüklendikten sonra Indexe gitsin
            }
            return View(model);
        }

        public IActionResult Sil(int id)
        {
            _urunRepository.Guncelle(new Urun { Id = id });
            return RedirectToAction("Index");

        }

        public IActionResult AtaKategori(int id)
        {

            var uruneAitKategoriler = _urunRepository.GetirKategoriler(id).Select
                (I=>I.Ad);
            var kategoriler = _kategoriKategoryRepositoryRepository.GetirHepsi();

            //tempdata ile actionlar arası veri taşıyalım 
            TempData["UrunId"] = id;

            List<KategoriAtaModel> list = new List<KategoriAtaModel>();
            foreach (var item in kategoriler)
            {
                KategoriAtaModel model = new KategoriAtaModel();
                model.KategoriId = item.Id;
                model.KategoriAd = item.Ad;
                model.Varmi = uruneAitKategoriler.Contains(item.Ad); //KATEGORİDE ÜRÜNÜN ADI VAR MI 

                list.Add(model);
            }
            return View(list);
        }

        [HttpPost]
        public IActionResult AtaKategori(List<KategoriAtaModel> list)
        {
            int urunId = (int)TempData["UrunId"]; //int cascade ettik çevirdik
            foreach (var item in list)
            {
                if (item.Varmi)
                {
                    _urunRepository.EkleKategori(new UrunKategori
                    {
                        KategoriId = item.KategoriId,
                        UrunId = urunId
                    });
                }
                else
                {
                    _urunRepository.SilKategori(new UrunKategori
                    {
                        KategoriId = item.KategoriId,
                        UrunId = urunId
                    });
                }
            }

            return RedirectToAction("Index");
        }
    }
}
