using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace EticaretProjesi.CustomExtension
{
    public static class CustomSessionExtension
    { //extension methodlarda class static olmalı
        public static void SetObject<T>(this ISession session, string key, T value)
        where  T:class, new()
        {
             var stringData =  JsonConvert.SerializeObject(value);
             //serileştirme işlemi bir nesnedeki verinin bir yerde depolaması veya ağ ortamında bir yerden bir yere gönderilmesi gerektiği durumlarda uygun formata dönüştürülmesine denir.

            session.SetString(key,stringData);
            
        }

        public static T GetObject<T>(this ISession session, string key) where T: class, new ()
        {
            var jsonData= session.GetString(key);
            if (!string.IsNullOrWhiteSpace(jsonData))
            {
               return JsonConvert.DeserializeObject<T>(jsonData);

            }

            return null;
            //gelen data boşsa null dön
        }
    }
}
