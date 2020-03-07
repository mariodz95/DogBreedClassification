using DogBreedML.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;
using Microsoft.ML;
using Microsoft.ML.Data;
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
            var filePath = $"G:\\Ruap project\\DogBreed\\DogBreed\\{formData.FileName}";

            if (formData.Length > 0)
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await formData.CopyToAsync(stream);
                }
            }
          
            ModelInput input = new ModelInput();
            input.ImageSource = formData.FileName;
           
            ModelOutput prediction = _predictionEnginePool.Predict(modelName: "ModelInput", example: input);

            MLContext mlContext = new MLContext();

            //var test = ConsumeModel.Predict(input);

            
            //Define DataViewSchema for data preparation pipeline and trained model
            DataViewSchema modelSchema;
            // Load trained model
            // Load model & create prediction engine
            string modelPath = "MLModels/MLModel.zip";
            ITransformer mlModel = mlContext.Model.Load(modelPath, out var modelInputSchema);
            var predEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);
            var scores = GetSlotNames(predEngine.OutputSchema, "Score", prediction.Score);

            List<DogBreedViewModel> dogList = new List<DogBreedViewModel>();
            foreach (KeyValuePair<string, float> item in scores.Take(5))
            {
                DogBreedViewModel dog = new DogBreedViewModel();
                dog.Name = item.Key.Remove(0, 10).Replace("_", " ");
                dog.Score = item.Value;
                dogList.Add(dog);
            }

            System.IO.File.Delete(filePath);

            //string dogBreed = prediction.Prediction;
            //var maxValue = prediction.Score.OrderByDescending(x => x).Take(5);
            
            //dogBreed = dogBreed.Remove(0, 10).Replace("_", " ");
            //DogBreedViewModel dog = new DogBreedViewModel();
            //dog.Name = dogBreed;
            //dog.Score = maxValue;

            return Ok(dogList);
        }

        private static Dictionary<string, float> GetSlotNames(DataViewSchema schema, string name, float[] scores)
        {
            Dictionary<string, float> result = new Dictionary<string, float>();


            var column = schema.GetColumnOrNull(name);

            var slotNames = new VBuffer<ReadOnlyMemory<char>>();
            column.Value.GetSlotNames(ref slotNames);
            var names = new string[slotNames.Length];
            var num = 0;
            foreach (var denseValue in slotNames.DenseValues())
            {
                result.Add(denseValue.ToString(), scores[num++]);
            }

            return result.OrderByDescending(c => c.Value).ToDictionary(i => i.Key, i => i.Value);
        }
    }
}