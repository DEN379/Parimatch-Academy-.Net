using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using WebConverter.Models;
using WebConverter.Services;

namespace WebConverter.Auth
{
    /// <summary>
    /// Bace access authentication handler
    /// </summary>
    public class BaseAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IOptionsMonitor<AuthenticationSchemeOptions> options;
        private readonly ILoggerFactory logger;
        private readonly UrlEncoder encoder;
        private readonly ISystemClock clock;
        private readonly IRegisterService registerService;

        /// <summary>
        /// Base auth handler constructor
        /// </summary>
        /// <param name="options">IOptionsMonitor</param>
        /// <param name="logger">ILoggerFactory</param>
        /// <param name="encoder">UrlEncoder</param>
        /// <param name="clock">ISystemClock</param>
        /// <param name="registerService">IRegisterService</param>
        public BaseAuthHandler
            (IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder, 
            ISystemClock clock,
            IRegisterService registerService
            )
                : base(options, logger, encoder, clock)
        {
            this.options = options;
            this.logger = logger;
            this.encoder = encoder;
            this.clock = clock;
            this.registerService = registerService;
        }

        /// <summary>
        /// Handle Authenticate Async
        /// </summary>
        /// <returns>Task of AuthenticateResult</returns>
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Authorization Header not found");
            Register register;
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                byte[] credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                string[] credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');

                string login = credentials[0];
                string password = credentials[1];

                register = await registerService.Auth(login, password);

            }
            catch (Exception)
            {
                return AuthenticateResult.Fail("Authorization Header isn't valid");
            }

            if (register == null)
                return AuthenticateResult.Fail("Username or Password isn't valid");


            var claims = new Claim[] {
                new Claim(ClaimTypes.NameIdentifier, register.Login),
                new Claim(ClaimTypes.Name, register.Password),
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
    }
}
