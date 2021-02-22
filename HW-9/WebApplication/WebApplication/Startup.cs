using System;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebApplication.Services;

namespace WebApplication
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IPrimeFindService, PrimeFindService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    var logger = context.RequestServices.GetRequiredService<ILogger<Startup>>();
                    logger.LogInformation("Get \"/\", printing project info");

                    string message = "Web prime numbers, made by Denys Sakadel";
                    await context.Response.WriteAsync(message);
                    logger.LogInformation("Program description: "+ message);

                });

                endpoints.MapGet("/primes", async context =>
                {
                    var logger = context.RequestServices.GetRequiredService<ILogger<Startup>>();
                    var primeFindService = context.RequestServices.GetRequiredService<IPrimeFindService>();
                    string from = context.Request.Query["from"].FirstOrDefault();
                    string to = context.Request.Query["to"].FirstOrDefault();

                    logger.LogInformation("Get \"/primes\", getting parametres");

                    if (int.TryParse(from, out int fromParse) &&
                        int.TryParse(to, out int toParse))
                    {
                        context.Response.StatusCode = (int) HttpStatusCode.OK;
                        logger.LogInformation("Got paramtres form \"/primes\" => from="+ fromParse+", to=" + toParse);

                        var primeNumbers = await primeFindService.FindPrimeNumber(fromParse, toParse);
                        var primeNumbersString = string.Join(" ", primeNumbers);

                        await context.Response.WriteAsync("Primes: " + primeNumbersString);

                        logger.LogInformation("Primes: " + primeNumbersString);
                    }
                    else
                    {
                        context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                        logger.LogInformation($"{(int)HttpStatusCode.BadRequest} code. Status: {HttpStatusCode.BadRequest}");
                    }
                });

                endpoints.MapGet("/primes/{number:int}", async context =>
                {
                    var logger = context.RequestServices.GetRequiredService<ILogger<Startup>>();
                    var primeFindService = context.RequestServices.GetRequiredService<IPrimeFindService>();
                    int number = int.Parse((string)context.Request.RouteValues["number"]);

                    logger.LogInformation("Get parameter from \"/primes/"+ number);

                    if(await primeFindService.CheckPrimeNumber(number))
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.OK;
                        string goodResponse = $"{(int)HttpStatusCode.OK} code. " +
                            $"Status: {HttpStatusCode.OK}, a number => {number} is prime";
                        await context.Response.WriteAsync(goodResponse);
                        logger.LogInformation(goodResponse);
                    }
                    else
                    {
                        string notFoudResponse = $"{(int)HttpStatusCode.NotFound} code. " +
                            $"Status: {HttpStatusCode.NotFound}, a number => {number} isn't prime";
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        logger.LogInformation(notFoudResponse);
                    }
                });
            });


        }
    }
}
