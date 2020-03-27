using DogBreed.Service.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DogBreed.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PredictController : ControllerBase
    {
        private readonly IResultService _resultService;

        public PredictController(IResultService responseService)
        {
            _resultService = responseService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Classify(Guid userId, [FromForm]IFormFile formData)
        {
            var predictionResult = await _resultService.Classify(formData, userId);
            return Ok(predictionResult);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetResults(int row, Guid userId)
        {         
            var listOfResults = await _resultService.GetAllResultsAsync(row, userId);
            return Ok(listOfResults);      
        }
    }
}