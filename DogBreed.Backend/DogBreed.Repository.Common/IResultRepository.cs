using DogBreed.DAL.Entities;
using DogBreed.Model.Common;
using System;
using System.Threading.Tasks;

namespace DogBreed.Repository.Common
{
    public interface IResultRepository
    {
        Task<IPredictionResult> AddResultAsync(Guid imageId, string name, float score);
        Task<IDogImage> AddImageAsync(string name, byte[] file, Guid userId);
        Task<DogImageEntity> GetAllResultsAsync(int row, Guid userId);
    }
}
