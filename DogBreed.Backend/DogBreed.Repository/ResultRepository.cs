using AutoMapper;
using DogBreed.DAL;
using DogBreed.DAL.Entities;
using DogBreed.Model.Common;
using DogBreed.Repository.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DogBreed.Repository
{
    public class ResultRepository : IResultRepository
    {
        private readonly IMapper _mapper;

        public ResultRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<IPredictionResult> AddResultAsync(Guid imageId, string name, float score)
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
            return _mapper.Map<IPredictionResult>(result);
        }

        public async Task<IDogImage> AddImageAsync(string name, byte[] file, Guid userId)
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
                    UserId = userId,
                };
                ctx.Add(result);
                await ctx.SaveChangesAsync();
            }
            return _mapper.Map<IDogImage>(result);
        }

        public async Task<DogImageEntity> GetAllResultsAsync(int row, Guid userId)
        {
            
            DogImageEntity result = null;
            using (var ctx = new DogBreedContext())
            {
                result = await ctx.DogImages.OrderByDescending(r => r.DateCreated).Skip(row).Take(1).FirstOrDefaultAsync(i => i.UserId == userId);
                if(result == null)
                {
                    return result;
                }
                result.PredictionResults = await ctx.PredictionResults.FirstOrDefaultAsync(p => p.DogImageId == result.Id);
            }
            return result;
        }
    }
}
