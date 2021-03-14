using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace DepsWebApp
{
    /// <summary>
    /// Main Program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Entry point webapp.
        /// </summary>
        /// <param name="args">Arguments.</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Host builder.
        /// </summary>
        /// <param name="args">Arguments.</param>
        /// <returns>IHostBuilder.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
