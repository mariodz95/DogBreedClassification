using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DogBreed.DAL.Entities;

namespace DogBreed.Service.Common
{
    public interface IResponseService
    {
        Task<List<DogImageEntity>> Classify(IFormFile formData);
        Task<List<DogImageEntity>> GetAllResultsAsync();
    }
}
