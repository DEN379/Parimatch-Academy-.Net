using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApplication.Controllers
{
    [Route("/")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly ILogger<StatusController> logger;

        public StatusController(ILogger<StatusController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public ActionResult<string> Index()
        {
            logger.LogInformation("Get \"/\", printing project info");

            string message = "Web prime numbers, made by Denys Sakadel";

            logger.LogInformation("Program description: " + message);

            return message;
        }
    }
}
