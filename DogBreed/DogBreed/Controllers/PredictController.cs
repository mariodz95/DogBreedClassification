using DogBreedML.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DogBreed.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PredictController : ControllerBase
    {
        private readonly PredictionEnginePool<ModelInput, ModelOutput> _predictionEnginePool;

        public PredictController(PredictionEnginePool<ModelInput, ModelOutput> predictionEnginePool)
        {
            _predictionEnginePool = predictionEnginePool;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Classify([FromForm]IFormFile formData)
        {
            // TODO: put file path to appsettings.json
            var filePath = $"G:\\Ruap web api\\DogBreed\\DogBreed\\{formData.FileName}";

            if (formData.Length > 0)
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await formData.CopyToAsync(stream);
                }
            }
          
            ModelInput input = new ModelInput();
            input.ImageSource = formData.FileName;
            input.Label = formData.Name;
           
            ModelOutput prediction = _predictionEnginePool.Predict(modelName: "ModelInput", example: input);

            System.IO.File.Delete(filePath);

            string dogBreed = prediction.Prediction;
            var maxValue = prediction.Score.OrderByDescending(x => x).Take(5);

            dogBreed = dogBreed.Remove(0, 10).Replace("_", " ");
            DogBreedViewModel dog = new DogBreedViewModel();
            dog.Name = dogBreed;
            dog.Score = maxValue;

            return Ok(dog);
        }
    }
}