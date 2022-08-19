using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EticaretProjesi.CustomExtension;
using EticaretProjesi.Entities;
using EticaretProjesi.Interfaces;
using Microsoft.AspNetCore.Http;

namespace EticaretProjesi.Repositories
{
    public class SepetRepository:ISepetRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        //HttpContext erişiyoruz buradan 
        public SepetRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void SepeteEkle(Urun urun)
        {
            var gelenliste = _httpContextAccessor.HttpContext.Session
                .GetObject<List<Urun>>("sepet");
            //gelen listeyi kontrol edicez verileri alıp

           

            if (gelenliste==null)
            {
                gelenliste = new List<Urun>();
                gelenliste.Add(urun);
                //gelen listeye ilgili ürünü ekleriz
                
            }
            else
            {
                gelenliste.Add(urun);
               
            }
            _httpContextAccessor.HttpContext.Session.SetObject("sepet",gelenliste);

           

        }

        public void SepettenCikar(Urun urun)
        {
            var gelenliste = _httpContextAccessor.HttpContext.Session
                .GetObject<List<Urun>>("sepet");
            //gelen listeyi kontrol edicez verileri alp
            gelenliste.Remove(urun); _httpContextAccessor.HttpContext.Session.SetObject("sepet",gelenliste);
        }

        public List<Urun> GetirSepettekiUrunler()
        {
            return _httpContextAccessor.HttpContext.Session.GetObject<List<Urun>>("sepet");
        }



        public void SepetiBosalt()
        {
            _httpContextAccessor.HttpContext.Session.Remove("sepet");

        }
    }
}
