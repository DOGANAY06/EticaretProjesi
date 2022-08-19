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
                
                //�ifre kurallar� belirleme
            }).AddEntityFrameworkStores<MyContext>();

            //userManager => aspnetUser =kullan�c� silme ekleme g�ncelleme rol atama gibi i�lemler yapar 
            //roleManager => aspnetrole  = rol ekleme rol silme 
            // signInManager => Kullan�c�n�n giri� ��k�� yapt��� kontrol�

            //Identity eklicem ve entityframework ile �al��cam �dentity giri� i�lemleri
            services.AddScoped<IKategoryRepository, KategoriRepository>();
            services.AddScoped<IUrunRepository, UrunRepository>();
            services.AddScoped<IUrunKategoryRepository, UrunKategoriRepository>();
            services.AddSession();
            services.AddAuthentication();
            services.AddScoped<ISepetRepository, SepetRepository>();
            //AddSingleton= Her iste�e tek bir nesne �rne�i d�ner 
            //AddTransient =Her iste�e farkl� bir nesne �rne�i d�ner
            //AddScoped= �lgili iste�e yapan ki�iye tek bir istek d�ner
            services.AddControllersWithViews();

            services.ConfigureApplicationCookie(opt =>
            {//giri� i�lemleri cookie
                opt.LoginPath = new PathString("/Home/GirisYap"); //admin paneline gitmeye �al���rsak
                opt.Cookie.Name = "AspNetCoreYoutube";
                opt.Cookie.HttpOnly = true;
                //javascript ile ilgili cookie �ekilemesin
                opt.Cookie.SameSite = SameSiteMode.Strict;
                //hi�bir domain altdomain kullanamaz
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

            IdentityInitializer.OlusturAdmin(userManager, roleManager); //bu bizim yerimize admini olu�turucak
            app.UseRouting();
            app.UseAuthentication();
            //KULLANICI G�R�� YAPMI� MI KONTROL� 

            app.UseAuthorization();
             //�ARTLAR KAR�ILANIYOR MU KAR�ILANMIYOR MU G�R�� ���N ONU KONTROL EDER
            app.UseExceptionHandler("/Error");
            //herhangi bir hatayla kar�� kar��ya geldi�inde hata kodunu vericek
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
