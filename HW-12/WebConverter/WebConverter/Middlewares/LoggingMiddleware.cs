using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace WebConverter.Middlewares
{
    /// <summary>
    /// Middleware that logs an information about request and response
    /// </summary>
    public class LoggingMiddleware
    {
        private readonly ILogger<LoggingMiddleware> logger;
        private readonly RequestDelegate next;
        private RecyclableMemoryStreamManager recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();

        /// <summary>
        /// Constructor of logging middleware.
        /// </summary>
        /// <param name="next">Request delegate.</param>
        /// <param name="logger">Logger.</param>
        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        /// <summary>
        /// Method that invoke a middleware.
        /// Loggs a request and response.
        /// </summary>
        /// <param name="context">Context.</param>
        /// <returns>Task.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            var requestBody = await ObtainRequestBody(context.Request);

            logger.LogInformation($"Request query: {context.Request.Path + context.Request.Query.FirstOrDefault()} " +
                $"with method {context.Request.Method}, body: {requestBody}.");

            var originalBodyStream = context.Response.Body;
            using var stream = recyclableMemoryStreamManager.GetStream();
            context.Response.Body = stream;

            await next.Invoke(context);

            var responseBody = await ObtainResponseBody(context);

            await stream.CopyToAsync(originalBodyStream);

            logger.LogInformation($"Response code: {context.Response.StatusCode}, body: {responseBody}");
        }

        /// <summary>
        /// Read a request body via stream reader.
        /// </summary>
        /// <param name="request">A Request.</param>
        /// <returns>Request body as string.</returns>
        private static async Task<string> ObtainRequestBody(HttpRequest request)
        { 
            if (request.Body == null) return string.Empty; 
            request.EnableBuffering(); 
            var encoding = GetEncodingFromContentType(request.ContentType); 
            string bodyStr; 
            using (var reader = new StreamReader(request.Body, encoding, true, 1024, true)) 
            { 
                bodyStr = await reader.ReadToEndAsync().ConfigureAwait(false); 
            } 
            request.Body.Seek(0, SeekOrigin.Begin); 
            return bodyStr; 
        }

        /// <summary>
        /// Read a response body via stream reader.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Response body as string.</returns>
        private static async Task<string> ObtainResponseBody(HttpContext context)
        { 
            var response = context.Response; 
            response.Body.Seek(0, SeekOrigin.Begin); 
            var encoding = GetEncodingFromContentType(response.ContentType); 
            using (var reader = new StreamReader(response.Body, encoding,
                detectEncodingFromByteOrderMarks: false, bufferSize: 4096, leaveOpen: true))
            { 
                var text = await reader.ReadToEndAsync().ConfigureAwait(false); 
                response.Body.Seek(0, SeekOrigin.Begin); 
                return text; 
            } 
        }

        /// <summary>
        /// Get encoding from content type.
        /// </summary>
        /// <param name="contentTypeStr">Content type as string.</param>
        /// <returns>Encoding.</returns>
        private static Encoding GetEncodingFromContentType(string contentTypeStr)
        {
            if (string.IsNullOrEmpty(contentTypeStr))
            {
                return Encoding.UTF8;
            }

            ContentType contentType;
            try
            {
                contentType = new ContentType(contentTypeStr);
            }
            catch (FormatException)
            {
                return Encoding.UTF8;
            }

            if (string.IsNullOrEmpty(contentType.CharSet))
            {
                return Encoding.UTF8;
            }

            return Encoding.GetEncoding(contentType.CharSet, EncoderFallback.ExceptionFallback,
                DecoderFallback.ExceptionFallback);
        }
    }
}
