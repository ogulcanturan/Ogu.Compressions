using Microsoft.AspNetCore.Mvc;
using Ogu.Compressions.Abstractions;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.IO.Compression;

namespace Sample.Api.Controllers
{
    [Route("api/compressions/[controller]")]
    public class GzipController : ControllerBase
    {
        private readonly IGzipCompression _compression;

        public GzipController(IGzipCompression compression)
        {
            _compression = compression;
        }

        [HttpPost("compress")]
        public async Task<IActionResult> Compress([FromBody] string rawInput, [FromQuery] CompressionLevel? level, CancellationToken cancellationToken = default)
        {
            var result = await _compression.CompressAsync(rawInput, level ?? _compression.Level, cancellationToken);

            return Ok(Convert.ToBase64String(result));
        }

        [HttpPost("decompress")]
        public async Task<IActionResult> Decompress([FromBody] string base64CompressedInput, CancellationToken cancellationToken = default)
        {
            var bytes = Convert.FromBase64String(base64CompressedInput);

            var result = await _compression.DecompressAsync(bytes, cancellationToken);

            return Ok(Encoding.UTF8.GetString(result));
        }
    }
}