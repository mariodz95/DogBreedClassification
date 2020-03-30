using DogBreed.DAL.Entities;
using System.Collections.Generic;
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
using System.Reflection;

namespace DogBreed.Service
{
    public class ResultService : IResultService
    {
        private readonly IResultRepository _resultRepository;
        public ResultService(IResultRepository resultRepository)
        {
            _resultRepository = resultRepository;
        }

        public async Task<List<IDogImage>> Classify(IFormFile formData, Guid userId)
        {
            //var filePath = $"G:\\Ruap project\\DogBreed.Backend\\DogBreed\\{formData.FileName}";
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

            MLContext mlContext = new MLContext();

            string modelPath = "MLModels/MLModel.zip";
            ITransformer mlModel = mlContext.Model.Load(modelPath, out var modelInputSchema);
            var predEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);
            var prediction = predEngine.Predict(input);
            var scores = Util.GetSlotNames(predEngine.OutputSchema, "Score", prediction.Score);

            List<IDogImage> dogList = new List<IDogImage>();

            IDogImage image = await _resultRepository.AddImageAsync(formData.FileName, file, userId);
            int counter = 1;
            foreach (KeyValuePair<string, float> item in scores.Take(5))
            {
                IDogImage dog = new DogImage();
                dog.Name = util.FirstLetterToUpper(item.Key.Remove(0, 10).Replace("_", " ").Replace("-", " "));
                dog.Score = item.Value;
                dogList.Add(dog);
                if (counter == 1)
                {
                    await _resultRepository.AddResultAsync(image.Id, dog.Name, dog.Score);
                }
                counter++;
            }

            System.IO.File.Delete(filePath);

            return dogList;
        }

        public async Task<IDogImage> GetAllResultsAsync(int row, Guid userId)
        {
            var test = await _resultRepository.GetAllResultsAsync(row, userId);
            return test;
        }
    }
}
