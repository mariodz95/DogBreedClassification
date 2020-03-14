using DogBreed.DAL;
using DogBreed.DAL.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DogBreed.Service
{
    public class ResponseService
    {
        protected DogBreedContext _context { get; set; }

        public ResponseService()
        {
        }

        public async Task<PredictionResultEntity> AddResultAsync(Guid imageId, string name, float score) 
        {
            PredictionResultEntity result;
            using (var ctx = new DogBreedContext())
            {
                result = new PredictionResultEntity
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Abrv = name,
                    Score = score,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    DogImageId = imageId
                };

                ctx.PredictionResults.Add(result);
                await ctx.SaveChangesAsync();
            }
            return result;
        }

        public async Task<DogImageEntity> AddImageAsync(string name, byte[] file)
        {
            DogImageEntity result;
  
            using (var ctx = new DogBreedContext())
            {
                result = new DogImageEntity
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Abrv = name,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    File = file,
                };
            ctx.Add(result);       
            await ctx.SaveChangesAsync();
            }
            return result;
        }

        public async Task<List<DogImageEntity>> GetResults(Guid imageId)
        {
            List<DogImageEntity> result;
            using (var ctx = new DogBreedContext())
            {
                result = await ctx.DogImages.Where(i => i.Id == imageId).Include( r => r.PredictionResults).ToListAsync();
            }
            return result;
        }

        public async Task<List<DogImageEntity>> GetAllResultsAsync()
        {
            List<DogImageEntity> result;
            using (var ctx = new DogBreedContext())
            {
                result = await ctx.DogImages.Include(r => r.PredictionResults).Take(5).ToListAsync();
            }
            return result;
        }
    }
}
