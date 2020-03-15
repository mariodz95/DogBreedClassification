using DogBreed.DAL.Entities;
using DogBreed.Service;
using DogBreed.Service.Common;
using DogBreed.Shared;
using DogBreed.ViewModel;
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
        private readonly IResponseService _responseService;

        public PredictController(IResponseService responseService)
        {
            _responseService = responseService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Classify([FromForm]IFormFile formData)
        {

            var predictionResult = await _responseService.Classify(formData);
            return Ok(predictionResult);
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetResults()
        {
            var listOfResults = await _responseService.GetAllResultsAsync();
            return Ok(listOfResults);
        
        }
    }
}