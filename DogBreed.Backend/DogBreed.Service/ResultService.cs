﻿using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using DogBreedML.Model;
using Microsoft.ML;
using Microsoft.AspNetCore.Http;
using DogBreed.Shared;
using DogBreed.Service.Common;
using DogBreed.Repository.Common;
using DogBreed.Model.Common;
using DogBreed.Model;
using System;
using Microsoft.Extensions.ML;
using DogBreed.DAL.Entities;

namespace DogBreed.Service
{
    public class ResultService : IResultService
    {
        private readonly IResultRepository _resultRepository;
        private readonly PredictionEnginePool<ModelInput, ModelOutput> _predictionEnginePool;

        public ResultService(IResultRepository resultRepository, PredictionEnginePool<ModelInput, ModelOutput> predictionEnginePool)
        {
            _resultRepository = resultRepository;
            _predictionEnginePool = predictionEnginePool;
        }

        public async Task<List<IDogImage>> Classify(IFormFile formData, Guid userId)
        {
            var path = Environment.CurrentDirectory;
            var filePath = Path.GetFullPath(path + formData.FileName);
            if (formData.Length > 0)
            {
                using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
                {
                    await formData.CopyToAsync(stream);
                }
            }
            Util util = new Util();
            byte[] file = util.ConvertToBytes(formData);

            ModelInput input = new ModelInput();
            input.ImageSource = filePath;

            var prediction = _predictionEnginePool.Predict(modelName: "DogBreedModel", example: input);
         
            List<IDogImage> dogList = new List<IDogImage>();

            IDogImage image = await _resultRepository.AddImageAsync(formData.FileName, file, userId);
            IDogImage dog = new DogImage();

            dog.Name = prediction.Prediction.Remove(0, 10).Replace("_", " ").Replace("-", " ");
            dog.Score = prediction.Score.Max();
            dogList.Add(dog);     
            await _resultRepository.AddResultAsync(image.Id, dog.Name, dog.Score);
            
            System.IO.File.Delete(filePath);

            return dogList;
        }

        public async Task<DogImageEntity> GetAllResultsAsync(int row, Guid userId)
        {
            return await _resultRepository.GetAllResultsAsync(row, userId);
        }
    }
}
