using Microsoft.AspNetCore.Mvc;
using Ogu.Compressions.Abstractions;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Api.Controllers
{
    [Route("api/decompressionhandler")]
    public class DecompressionHandlerController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DecompressionHandlerController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("with-compressionhandler")]
        public async Task<IActionResult> WithCompressionHandler([FromQuery] CompressionType compressionType, CancellationToken cancellationToken = default)
        {
            // named HttpClient is registered in the Program.cs - builder.Services.AddHttpClient("DecompressionHandler").AddHttpMessageHandler<DecompressionHandler>();
            var client = _httpClientFactory.CreateClient("DecompressionHandler"); 

            if (compressionType != CompressionType.None)
            {
                // Requesting encoding if possible - the ResponseCompression middleware will compress the response
                client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue(compressionType.ToEncodingName()));
            }

            // DecompressionHandler will decompress automatically - response should be understandable!
            using (var httpResponseMessage = await client.GetAsync(
                       $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/api/decompressionhandler",
                       cancellationToken))
            {
                var result = await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken);

                return Ok(result);
            }
        }

        [HttpGet("without-compressionhandler")]
        public async Task<IActionResult> WithoutCompressionHandler([FromQuery] CompressionType compressionType, CancellationToken cancellationToken = default)
        {
            // HttpClient is registered in the Program.cs - builder.Services.AddHttpClient();
            var client = _httpClientFactory.CreateClient();

            if (compressionType != CompressionType.None)
            {
                // Requesting encoding if possible - then the ResponseCompression middleware will compress the response
                client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue(compressionType.ToEncodingName()));
            }

            // decompress automatically - response shouldn't be understandable if compression type selected other than none!
            using (var httpResponseMessage = await client.GetAsync(
                       $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/api/decompressionhandler",
                       cancellationToken))
            {

                var result = await httpResponseMessage.Content.ReadAsByteArrayAsync(cancellationToken);

                return Ok(new
                {
                    StringData = Encoding.UTF8.GetString(result),
                    Base64String = Convert.ToBase64String(result),
                    CompressionType = $"{compressionType} ({(int)compressionType})"
                });
            }
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Test()
        {
            return Ok("Hello world!");
        }
    }
}