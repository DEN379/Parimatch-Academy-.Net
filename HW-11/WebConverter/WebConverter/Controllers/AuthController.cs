using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebConverter.Filters;
using WebConverter.Models;

namespace WebConverter.Controllers
{
    /// <summary>
    /// Authentication controller with route /register
    /// </summary>
    [Route("/register")]
    public class AuthController : Controller
    {
        /// <summary>
        /// User registration.
        /// </summary>
        /// <param name="register">Regiser model.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        [AuthExceptionFilter]
        public IActionResult Register([FromBody] Register register)
        {
            throw new NotImplementedException();
        }
    }
}
