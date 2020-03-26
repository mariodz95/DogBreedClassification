using System.Threading.Tasks;
using DogBreed.DAL.Entities;
using DogBreed.Service.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DogBreed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;


        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> Registration(string email, string password)
        {
            var result = await _accountService.RegisterAsync(email, password);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var result = await _accountService.LoginAsync(email, password);
            return Ok(result);
        }
    }
}