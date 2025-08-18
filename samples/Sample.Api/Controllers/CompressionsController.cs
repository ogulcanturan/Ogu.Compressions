using Microsoft.AspNetCore.Mvc;
using Ogu.Compressions.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO.Compression;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace Sample.Api.Controllers
{
    [Route("api/[controller]")]
    public class CompressionsController : ControllerBase
    {
        private readonly ICompressionProvider _compressionProvider;
        private readonly IMemoryCache _memoryCache;

        public CompressionsController(ICompressionProvider compressionProvider, IMemoryCache memoryCache)
        {
            _compressionProvider = compressionProvider;
            _memoryCache = memoryCache;
        }

        [HttpGet("compress")]
        public IActionResult GetCompressedData([Required][FromQuery] Guid requestId)
        {
            if (!_memoryCache.TryGetValue($"C_{requestId}", out var content))
            {
                return NotFound();
            }

            return new ContentResult
            {
                Content = $"{content}",
                ContentType = "text/plain",
                StatusCode = 200
            };
        }

        [HttpGet("decompress")]
        public IActionResult GetDecompressedData([Required] [FromQuery] Guid requestId)
        {
            if (!_memoryCache.TryGetValue($"D_{requestId}", out var content))
            {
                return NotFound();
            }

            return new ContentResult
            {
                Content = $"{content}",
                ContentType = "application/json",
                StatusCode = 200
            };
        }

        [HttpPost("compress")]
        public async Task<IActionResult> Compress([FromBody] string rawInput, [FromQuery] CompressionType type, [FromQuery] CompressionLevel? level, [FromQuery] bool returnContent = true, CancellationToken cancellationToken = default)
        {
            var result = level.HasValue
                ? await _compressionProvider.CompressAsync(type, rawInput, level.Value, cancellationToken) 
                : await _compressionProvider.CompressAsync(type, rawInput, cancellationToken);

            var content = Convert.ToBase64String(result);

            var correlationId = Guid.NewGuid().ToString();

            HttpContext.Response.Headers.TryAdd("X-Request-Id", correlationId);
            HttpContext.Response.Headers.TryAdd("X-Quick-Link", $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}?requestId={correlationId}");

            _memoryCache.Set($"C_{correlationId}", content);

            return Ok(returnContent ? content : string.Empty);
        }

        [HttpPost("decompress")]
        public async Task<IActionResult> Decompress([FromBody] string base64CompressedInput, [FromQuery] CompressionType type, [FromQuery] bool returnContent = true, CancellationToken cancellationToken = default)
        {
            var bytes = Convert.FromBase64String(base64CompressedInput);

            var result = await _compressionProvider.DecompressAsync(type, bytes, cancellationToken);

            var content = Encoding.UTF8.GetString(result);

            var correlationId = Guid.NewGuid().ToString();

            HttpContext.Response.Headers.TryAdd("X-Request-Id", correlationId);
            HttpContext.Response.Headers.TryAdd("X-Quick-Link", $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}?requestId={correlationId}");

            _memoryCache.Set($"D_{correlationId}", content);

            return Ok(returnContent ? content : string.Empty);
        }
    }
}