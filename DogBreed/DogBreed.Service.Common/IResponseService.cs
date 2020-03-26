﻿using Microsoft.AspNetCore.Http;
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
        Task<DogImageEntity> GetAllResultsAsync(int row);
        Task<UserEntity> RegisterAsync(string email, string password);
        Task<bool> CheckIfUserExist(string email);
        Task<UserEntity> LoginAsync(string email, string password); 
    }
}
