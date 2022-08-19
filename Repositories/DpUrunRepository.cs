using Dapper.Contrib.Extensions;
using EticaretProjesi.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EticaretProjesi.Repositories
{
    public class DpUrunRepository
    {
        public List<Urun> GetirHepsi()
        {//en hızlı micro orm aracı dapper 
            using var connection = new SqlConnection("server=(localdb)\\MSSQLLocalDB;database=Eticaret; integrated security =true ");

            return connection.GetAll<Urun>().ToList();

        }
    }
}
