using EticaretProjesi.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EticaretProjesi.Repositories
{
    public class GenericRepository<Tablo> where Tablo:class,new()
    {//bütün entitlylerin kullanacağı class
        public void Ekle(Tablo tablo)
        {
            using var context = new MyContext();

            context.Set<Tablo>().Add(tablo);
            //Bu tablo dediğimiz şey bize generic repository den ne için geldiyse bu için 
            context.SaveChanges();
            //değişimler ram tarafından yansıması için

        }
        public void Guncelle(Tablo tablo)
        {
            using var context = new MyContext();

            context.Set<Tablo>().Update(tablo);
            //kategori tablosunu ekledik
            context.SaveChanges();
            //değişimler ram tarafından yansıması için

        }
        public void Sil(Tablo tablo)
        {
            using var context = new MyContext();

            context.Set<Tablo>().Remove(tablo);
            //kategori tablosunu ekledik
            context.SaveChanges();
            //değişimler ram tarafından yansıması için

        }
        public List<Tablo> GetirHepsi()
        {
            using var context = new MyContext();

            return context.Set<Tablo>().ToList();
            //contextin içindeki parametreleri listeye çevirerek getiriyor
        }
        public Tablo GetirIdile(int id)
        {
            using var context = new MyContext();
            return
                context.Set<Tablo>().Find(id);

        }
    }
}
