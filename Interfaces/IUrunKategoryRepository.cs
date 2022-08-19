using EticaretProjesi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EticaretProjesi.Interfaces
{
    public interface IUrunKategoryRepository : IGenericRepository<UrunKategori>
    {
        UrunKategori GetirFiltreile(Expression<Func<UrunKategori, bool>> filter);
        //linq sorgusu herhangi bir tipten değer alıp her hangi bir tipten değer dönen delegeler
    }

}
