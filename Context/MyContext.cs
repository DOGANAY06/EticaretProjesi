using EticaretProjesi.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EticaretProjesi.Context
{
    public class MyContext :IdentityDbContext<AppUser> 
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\MSSQLLocalDB;Database=Eticaret;Integrated Security=true;");
            //DATABASE BAĞLANMAK İÇİN
            base.OnConfiguring(optionsBuilder);
        }
        //MODEL BELİRTİRİZ

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Urun>().HasMany(I => I.UrunKategoriler).WithOne
                (I => I.Urun).HasForeignKey(I => I.UrunId);
            modelBuilder.Entity<Kategori>().HasMany(I => I.UrunKategoriler).WithOne
                (I => I.Kategori).HasForeignKey(I => I.KategoriId);
            modelBuilder.Entity<UrunKategori>().HasIndex(I => new
            {
                I.KategoriId,
                I.UrunId
            }).IsUnique(); //TEKRARLI data girilmesinin önüne geçiyoruz böyle yaparak
            base.OnModelCreating(modelBuilder);
        } //ilişki belirtiyoruz

        public DbSet<UrunKategori> UrunKategoriler  { get; set; }
        public DbSet<Urun> Urunler { get; set; }
        public DbSet<Kategori> Kategoriler { get; set; }


    }
}
