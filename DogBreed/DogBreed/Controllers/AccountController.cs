using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogBreed.DAL.Entities;
using DogBreed.Service.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DogBreed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IResponseService _responseService;
        public  UserManager<IdentityUser> _userManager;
        public SignInManager<IdentityUser> _signInManager;

        public AccountController(IResponseService responseService /*UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManage*/)
        {
            _responseService = responseService;
            //_userManager = userManager;
            //_signInManager = signInManager;
        }




        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> Registration(string email, string password)
        {
            UserEntity result = await _responseService.RegisterAsync(email, password);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(string email, string password)
        {
            UserEntity result = await _responseService.LoginAsync(email, password);
            return Ok(result);
        }
    }
}