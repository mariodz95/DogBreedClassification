using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using DogBreed.Model.Common;
using System;
using DogBreed.DAL.Entities;

namespace DogBreed.Service.Common
{
    public interface IResultService
    {
        Task<List<IDogImage>> Classify(IFormFile formData, Guid userId);
        Task<DogImageEntity> GetAllResultsAsync(int row, Guid userId);
    }
}
