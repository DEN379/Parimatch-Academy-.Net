using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebConverter.Filters;
using WebConverter.Models;
using WebConverter.Services;

namespace WebConverter.Controllers
{
    /// <summary>
    /// Authentication controller with route /register
    /// </summary>
    [Route("/register")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly IRegisterService registerService;

        /// <summary>
        /// Auth controller constructor.
        /// </summary>
        /// <param name="registerService">IRegisterService.</param>
        public AuthController(IRegisterService registerService)
        {
            this.registerService = registerService;
        }

        /// <summary>
        /// User registration.
        /// </summary>
        /// <param name="register">Regiser model.</param>
        /// <returns>Credentials as Base64 code.</returns>
        [HttpPost]
        [AuthExceptionFilter]
        public IActionResult Register(Register register)
        {
            byte[] credentialsBytes = Encoding.UTF8.GetBytes(register.Login + ":" + register.Password);
            string credentialsBase64 = Convert.ToBase64String(credentialsBytes);

            registerService.Register(register);

            return Ok("Basic " + credentialsBase64);
        }
    }
}
