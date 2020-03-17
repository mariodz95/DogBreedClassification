
using DogBreed.Service.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetResults(int row)
        {
            var listOfResults = await _responseService.GetAllResultsAsync(row);
            var headerlength = listOfResults.ToString().Length;
            return Ok(listOfResults);      
        }
    }
}