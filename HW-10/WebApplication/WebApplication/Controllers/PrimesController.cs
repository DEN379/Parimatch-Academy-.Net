using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    [Route("primes")]
    [ApiController]
    public class PrimesController : ControllerBase
    {
        private readonly IPrimeFindService primeFindService;
        private readonly ILogger<PrimesController> logger;

        public PrimesController(IPrimeFindService primeFindService ,ILogger<PrimesController> logger)
        {
            this.primeFindService = primeFindService;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult GetPrimes([FromQuery] int? from, [FromQuery] int? to)
        {
            logger.LogInformation("Get \"/primes\", getting parametres");

            if (from != null || to != null)
            {
                logger.LogInformation("Got paramtres form \"/primes\" => from=" + from + ", to=" + to);

                var primeNumbers = primeFindService.FindPrimeNumber(from.Value, to.Value);
                var primeNumbersString = string.Join(" ", primeNumbers);

                logger.LogInformation("Primes: ");
                logger.LogInformation(primeNumbersString);

                return Ok(primeNumbersString);
            }
            else
            {
                logger.LogInformation($"{(int)HttpStatusCode.BadRequest} code. Status: {HttpStatusCode.BadRequest}");

                return BadRequest();
            }
        }

        [HttpGet("{number:int}")]
        public IActionResult CheckNumberIsPrime(int number)
        {
            logger.LogInformation("Get parameter from \"/primes/" + number);

            if (primeFindService.CheckPrimeNumber(number))
            {
                string goodResponse = $"{(int)HttpStatusCode.OK} code. " +
                    $"Status: {HttpStatusCode.OK}, a number => {number} is prime";
                logger.LogInformation(goodResponse);

                return Ok(goodResponse);
            }
            else
            {
                string notFoudResponse = $"{(int)HttpStatusCode.NotFound} code. " +
                    $"Status: {HttpStatusCode.NotFound}, a number => {number} isn't prime";
                logger.LogInformation(notFoudResponse);

                return NotFound();
            }
        }
    }
}
