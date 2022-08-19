using EticaretProjesi.Interfaces;
using EticaretProjesi.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EticaretProjesi.Context;
using EticaretProjesi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace EticaretProjesi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddDbContext<MyContext>();
            services.AddIdentity<AppUser,IdentityRole>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequiredLength = 1;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                
                //þifre kurallarý belirleme
            }).AddEntityFrameworkStores<MyContext>();

            //userManager => aspnetUser =kullanýcý silme ekleme güncelleme rol atama gibi iþlemler yapar 
            //roleManager => aspnetrole  = rol ekleme rol silme 
            // signInManager => Kullanýcýnýn giriþ çýkýþ yaptýðý kontrolü

            //Identity eklicem ve entityframework ile çalýþcam ýdentity giriþ iþlemleri
            services.AddScoped<IKategoryRepository, KategoriRepository>();
            services.AddScoped<IUrunRepository, UrunRepository>();
            services.AddScoped<IUrunKategoryRepository, UrunKategoriRepository>();
            services.AddSession();
            services.AddAuthentication();
            services.AddScoped<ISepetRepository, SepetRepository>();
            //AddSingleton= Her isteðe tek bir nesne örneði döner 
            //AddTransient =Her isteðe farklý bir nesne örneði döner
            //AddScoped= Ýlgili isteðe yapan kiþiye tek bir istek döner
            services.AddControllersWithViews();

            services.ConfigureApplicationCookie(opt =>
            {//giriþ iþlemleri cookie
                opt.LoginPath = new PathString("/Home/GirisYap"); //admin paneline gitmeye çalýþýrsak
                opt.Cookie.Name = "AspNetCoreYoutube";
                opt.Cookie.HttpOnly = true;
                //javascript ile ilgili cookie çekilemesin
                opt.Cookie.SameSite = SameSiteMode.Strict;
                //hiçbir domain altdomain kullanamaz
                opt.ExpireTimeSpan=TimeSpan.FromMinutes(30); //30 dakika tutulsun 
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env
        ,UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStatusCodePagesWithReExecute("/Home/NotFound", "?code={0}");

            IdentityInitializer.OlusturAdmin(userManager, roleManager); //bu bizim yerimize admini oluþturucak
            app.UseRouting();
            app.UseAuthentication();
            //KULLANICI GÝRÝÞ YAPMIÞ MI KONTROLÜ 

            app.UseAuthorization();
             //ÞARTLAR KARÞILANIYOR MU KARÞILANMIYOR MU GÝRÝÞ ÝÇÝN ONU KONTROL EDER
            app.UseExceptionHandler("/Error");
            //herhangi bir hatayla karþý karþýya geldiðinde hata kodunu vericek
            app.UseSession();
            //ornek.com.tr/deneme
            //ornek.com.tr/Home/Index
            app.UseEndpoints(endpoints =>
            {
                
                endpoints.MapControllerRoute(name: "areas", pattern: "{area}/{controller=Home}/" +
                                                                     "{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                
            });
        }
    }
}
