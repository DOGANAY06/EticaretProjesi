using EticaretProjesi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EticaretProjesi.CustomExtension;
using EticaretProjesi.Entities;
using Microsoft.AspNetCore.Http;
using EticaretProjesi.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using NLog;
using LogLevel = NLog.LogLevel;

namespace EticaretProjesi.Controllers
{
    //[Route("dogan/[action]")]  //controllerde yazdığımız endpoints middleware ezer burada ki 
    
    public class HomeController : Controller
    { 
        private readonly SignInManager<AppUser> _signInManager;
        IUrunRepository _urunRepository;
        private readonly ISepetRepository _sepetRepository;
        public HomeController(IUrunRepository urunRepository, SignInManager<AppUser> signInManager, ISepetRepository sepetRepository)
        {//construactor
            _signInManager = signInManager;
            _sepetRepository = sepetRepository;
            _urunRepository = urunRepository;
        }

       
      
        public IActionResult Index(int? kategoriId)
        {
            ViewBag.KategoriId = kategoriId;
            TempData["urunSayisi"] =0;
            return View();
            //databasede ki bütün ürünü döndürecek
        }
        public IActionResult GirisYap()
        {
            return View(new KullanıcıGirisModel());
        }
        [HttpPost]
        public IActionResult GirisYap(KullanıcıGirisModel model)
        {
            if (ModelState.IsValid)
            {
                var signInResult = _signInManager.PasswordSignInAsync(model.KullaniciAd, model.Sifre, model.BeniHatirla, false).Result;
                //şartlar sağlanıyor mu sağlanmıyor mu required olan kullanıcıgirismodelde
                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                ModelState.AddModelError("","Kullanıcı adı veya şifre hatalı");
            }


            return View(model);
        }
        //tam olarak burada sunu yapmaya çalışıyoruz
        //ysk.com.tr/Home/UrunDetay/3
        public IActionResult UrunDetay(int id)
        {
           
            return View(_urunRepository.GetirIdile(id));
        }

        public IActionResult EkleSepet(int id)
        {

            var urun = _urunRepository.GetirIdile(id);
            _sepetRepository.SepeteEkle(urun);
            TempData["Bildirim"] = "Ürün sepete eklendi";


            return RedirectToAction("Index");


        }

        public IActionResult Sepet()
        { _sepetRepository.GetirSepettekiUrunler();
            return View(_sepetRepository.GetirSepettekiUrunler()
                );
        }
        //
        public IActionResult SepetiBosalt(decimal fiyat)
        {
            _sepetRepository.SepetiBosalt();
            return RedirectToAction("Tesekkur",new {fiyat=fiyat});
            //Tesekkur tasıdık ilgili fiyatı

        }

        public IActionResult Tesekkur(decimal fiyat)
        {
            ViewBag.Fiyat = fiyat;
            return View();
        }

        public IActionResult SepettenCikar(int id)
        {
           var cikarilacakUrun = _urunRepository.GetirIdile(id);
           _sepetRepository.SepettenCikar(cikarilacakUrun);
           return RedirectToAction("Sepet");

        }

        public IActionResult NotFound(int code)
        {
            ViewBag.Code = code;
            return View();
        }

        [Route("/Error")]
        public IActionResult Error()
        {
            var errorInfo = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var logger = LogManager.GetLogger("FileLogger");
            //logumuza ulaşabiliriz
            logger.Log(LogLevel.Error, $"\nHatanın gerçekleştiği yer:{errorInfo.Error} \nHata Mesajı:{errorInfo.Error.Message}\nStack Trace:{errorInfo.Error.StackTrace}");
            return View();
        }

        
       


    }
}
