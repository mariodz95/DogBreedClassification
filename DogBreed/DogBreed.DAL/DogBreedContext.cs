using DogBreed.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DogBreed.DAL
{
    public class DogBreedContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
                   => options.UseSqlServer("Server=localhost;Database=DogBreed;Trusted_Connection=True;");
                
        public DbSet<DogBreedEntity> DogBreeds { get; set; }
        public DbSet<PredictionResultEntity> PredictionResults { get; set; }
        public DbSet<DogImageEntity> DogImages { get; set; }
    }
}
