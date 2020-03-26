using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using DogBreed.Model.Common;

namespace DogBreed.Service.Common
{
    public interface IResultService
    {
        Task<List<IDogImage>> Classify(IFormFile formData);
        Task<IDogImage> GetAllResultsAsync(int row);
    }
}
