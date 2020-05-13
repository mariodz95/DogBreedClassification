using DogBreed.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DogBreed.DAL
{
    public class DogBreedContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
                   => options.UseSqlServer("Server=tcp:dogbreed20200506121351dbserver.database.windows.net,1433;Initial Catalog=DogBreed20200506121351_db;Persist Security Info=False;User ID=Admin123;Password=123Admin;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
                
        public DbSet<PredictionResultEntity> PredictionResults { get; set; }
        public DbSet<DogImageEntity> DogImages { get; set; }
        public DbSet<UserEntity> Users { get; set; }
    }
}
