using EticaretProjesi.Context;
using EticaretProjesi.Entities;
using EticaretProjesi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EticaretProjesi.Repositories
{
    public class UrunRepository : GenericRepository<Urun>,IUrunRepository
    {//genericrepository de hepsi var simdi ürüne göre kategori listesi döndürücez bir ürün birden fazla kategoriye sahip olabilir

        private readonly IUrunKategoryRepository _urunKategoriRepository;
        //bağımlılıklardan gelicek kategori ve ürünü birleştiricez
        public UrunRepository(IUrunKategoryRepository urunKategoriRepository)
        {
            _urunKategoriRepository = urunKategoriRepository;
        }

        public List<Kategori> GetirKategoriler(int urunId) //inteface imzasını belirtmeliyim bunun 
        {
            using var context = new MyContext();
            return  context.Urunler.Join(context.UrunKategoriler, urun => urun.Id, //dönen query
                UrunKategori => UrunKategori.UrunId,
                (u, uc) => new
                {
                    //yukarıda urunler tablosunu urun kategoriler tablosu ile birleştirme islemi yaptım 
                    urun = u,
                    urunKategori = uc
                }).Join(context.Kategoriler, iki => iki.urunKategori.KategoriId, kategori => kategori.Id,
                (uc, k) => new
                {
                    urun = uc.urun,
                    kategori = k,
                    urunKategori = uc.urunKategori //3 tablo birleştirdik
                }).Where(I => I.urun.Id == urunId).Select(I => new Kategori
            {
                Ad = I.kategori.Ad,
                Id = I.kategori.Id,

            }).ToList();
        }

        public void EkleKategori(UrunKategori urunKategori)
        {
            var kontrolKayit = _urunKategoriRepository.GetirFiltreile
            (I => I.KategoriId == urunKategori.KategoriId &&
                  I.UrunId == urunKategori.UrunId);
            //kategori Id eşitse bizim gönderdiğimiz kategori ve ürünkategoriler içerisinde urunıd eşitse bizim gönderdiğimiz urunıd getir bunu
            //varsa getirecek 
            if (kontrolKayit == null)
            {
                _urunKategoriRepository.Ekle(urunKategori);
            }
        }

        public void SilKategori(UrunKategori urunKategori)
        {
            var kontrolKayit = _urunKategoriRepository.GetirFiltreile
            (I => I.KategoriId == urunKategori.KategoriId &&
                  I.UrunId == urunKategori.UrunId);
            //kategori Id eşitse bizim gönderdiğimiz kategori ve ürünkategoriler içerisinde urunıd eşitse bizim gönderdiğimiz urunıd getir bunu
            //varsa getirecek 
            if (kontrolKayit!=null)
            {
                _urunKategoriRepository.Sil(kontrolKayit);
            }
        }

        public List<Urun> GetirKategoriIdile(int KategoriId)
        {
            using var context = new MyContext(); 
            //join sorgusu atıcaz kategoriye göre getirmek için
            return context.Urunler.Join(context.UrunKategoriler, u => u.Id, uc => uc.UrunId,
                (urun, urunkategori) => new
                {
                    Urun = urun,
                    UrunKategori = urunkategori
                }).Where(I => I.UrunKategori.KategoriId ==KategoriId).Select(I => new Urun
            {
                Id = I.Urun.Id,
                Ad = I.Urun.Ad,
                Fiyat = I.Urun.Fiyat,
                Resim = I.Urun.Resim
            }).ToList();
        }
    }
}
