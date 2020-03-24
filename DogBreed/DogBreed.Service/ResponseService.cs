using DogBreed.DAL;
using DogBreed.DAL.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using DogBreedML.Model;
using Microsoft.ML;
using Microsoft.AspNetCore.Http;
using DogBreed.Shared;
using DogBreed.Service.Common;

namespace DogBreed.Service
{
    public class ResponseService : IResponseService
    {
        protected DogBreedContext _context { get; set; }

        public ResponseService()
        {
        }

        public async Task<List<DogImageEntity>> Classify(IFormFile formData)
        {
            var filePath = $"G:\\Ruap project\\DogBreed\\DogBreed\\{formData.FileName}";

            if (formData.Length > 0)
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await formData.CopyToAsync(stream);
                }
            }
            Util util = new Util();
            byte[] file = util.ConvertToBytes(formData);

            ModelInput input = new ModelInput();
            input.ImageSource = formData.FileName;

            MLContext mlContext = new MLContext();

            string modelPath = "MLModels/MLModel.zip";
            ITransformer mlModel = mlContext.Model.Load(modelPath, out var modelInputSchema);
            var predEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);
            var prediction = predEngine.Predict(input);
            var scores = Util.GetSlotNames(predEngine.OutputSchema, "Score", prediction.Score);

            List<DogImageEntity> dogList = new List<DogImageEntity>();
            ResponseService service = new ResponseService();

            DogImageEntity image = await service.AddImageAsync(formData.FileName, file);
            int counter = 1;
            foreach (KeyValuePair<string, float> item in scores.Take(5))
            {
                DogImageEntity dog = new DogImageEntity();
                dog.Name = util.FirstLetterToUpper(item.Key.Remove(0, 10).Replace("_", " ").Replace("-", " "));
                dog.Score = item.Value;
                dogList.Add(dog);
                if (counter == 1)
                {
                    await service.AddResultAsync(image.Id, dog.Name, dog.Score);
                }
                counter++;
            }

            System.IO.File.Delete(filePath);

            return dogList;
        }

        public async Task<UserEntity> RegisterAsync(string email, string password)
        {
            bool exist = await CheckIfUserExist(email);
            UserEntity result;
            if (!exist)
            {
                using (var ctx = new DogBreedContext())
                {
                    result = new UserEntity()
                    {
                        Id = Guid.NewGuid(),
                        Email = email,
                        Password = password,
                        ConfirmPassword = password,
                        DateCreated = DateTime.Now,
                        DateUpdated = DateTime.Now
                    };
                    await ctx.AddAsync(result);
                    await ctx.SaveChangesAsync();
                }
            }
            else
            {
                throw new Exception("User already exist");
            }
            return result;
        }

        public async Task<UserEntity> LoginAsync(string email, string password) 
        {
            bool loginSuccessful = await CheckIfUserExist(email);
            UserEntity user;
            if(loginSuccessful)
            {
                using (var ctx = new DogBreedContext())
                {
                    user = await ctx.Users.FirstOrDefaultAsync(u => u.Email == email);
                    return user;
                }
            }
            else
            {
                throw new Exception("User doesn't exist");
            }
        }
        public async Task<bool> CheckIfUserExist(string email) 
        {
            List<UserEntity> users;
            bool userExist;
            using(var ctx = new DogBreedContext())
            {
                users = await ctx.Users.ToListAsync();
                userExist = users.Any(u => u.Email == email);   
            }
            return userExist;

        }

        private async Task<PredictionResultEntity> AddResultAsync(Guid imageId, string name, float score) 
        {
            PredictionResultEntity result;
            using (var ctx = new DogBreedContext())
            {
                result = new PredictionResultEntity
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Abrv = name,
                    Score = score,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    DogImageId = imageId
                };

                ctx.PredictionResults.Add(result);
                await ctx.SaveChangesAsync();
            }
            return result;
        }

        public async Task<DogImageEntity> AddImageAsync(string name, byte[] file)
        {
            DogImageEntity result;
  
            using (var ctx = new DogBreedContext())
            {
                result = new DogImageEntity
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Abrv = name,
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    File = file,
                };
            ctx.Add(result);       
            await ctx.SaveChangesAsync();
            }
            return result;
        }

        public async Task<List<DogImageEntity>> GetResults(Guid imageId)
        {
            List<DogImageEntity> result;
            using (var ctx = new DogBreedContext())
            {
                result = await ctx.DogImages.Where(i => i.Id == imageId).Include( r => r.PredictionResults).ToListAsync();
            }
            return result;
        }

        public async Task<DogImageEntity> GetAllResultsAsync(int row)
        {
            DogImageEntity result;
            using (var ctx = new DogBreedContext())
            {
                result = await ctx.DogImages.OrderByDescending(r => r.DateCreated).Skip(row).Take(1).FirstOrDefaultAsync();
                result.PredictionResults = await ctx.PredictionResults.FirstOrDefaultAsync(p => p.DogImageId == result.Id);
            }
            return result;
        }
    }
}
