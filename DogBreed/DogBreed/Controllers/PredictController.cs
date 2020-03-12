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

            string modelPath = "MLModels/MLModel.zip";
            ITransformer mlModel = mlContext.Model.Load(modelPath, out var modelInputSchema);
            var predEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);
            var scores = GetSlotNames(predEngine.OutputSchema, "Score", prediction.Score);

            List<DogBreedViewModel> dogList = new List<DogBreedViewModel>();
            foreach (KeyValuePair<string, float> item in scores.Take(5))
            {
                DogBreedViewModel dog = new DogBreedViewModel();
                dog.Name = FirstLetterToUpper(item.Key.Remove(0, 10).Replace("_", " ").Replace("-"," "));
                dog.Score = item.Value;
                dogList.Add(dog);
            }

            System.IO.File.Delete(filePath);

            return Ok(dogList);
        }


        public string FirstLetterToUpper(string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
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