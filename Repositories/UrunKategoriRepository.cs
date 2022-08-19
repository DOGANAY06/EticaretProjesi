using EticaretProjesi.Context;
using EticaretProjesi.Entities;
using EticaretProjesi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EticaretProjesi.Repositories
{
    public class UrunKategoriRepository : GenericRepository<UrunKategori>, IUrunKategoryRepository
    {
        public UrunKategori GetirFiltreile(Expression<Func<UrunKategori, bool>> filter)
        {
            using var context = new MyContext();
            return context.UrunKategoriler.FirstOrDefault(filter);

        }
    }
}
