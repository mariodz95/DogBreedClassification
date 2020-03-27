using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using DogBreed.Model.Common;
using System;

namespace DogBreed.Service.Common
{
    public interface IResultService
    {
        Task<List<IDogImage>> Classify(IFormFile formData, Guid userId);
        Task<IDogImage> GetAllResultsAsync(int row, Guid userId);
    }
}
