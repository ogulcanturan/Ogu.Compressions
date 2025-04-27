using Microsoft.AspNetCore.Mvc;
using Ogu.Compressions.Abstractions;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO.Compression;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Api.Controllers
{
    [Route("api/compressions/[controller]")]
    public class ZstdController : ControllerBase
    {
        private readonly IZstdCompression _compression;

        public ZstdController(IZstdCompression compression)
        {
            _compression = compression;
        }

        [HttpPost("compress")]
        public async Task<IActionResult> Compress([FromBody] string rawInput, [FromQuery][Range(-131072, 22)] int? level, CancellationToken cancellationToken = default)
        {
            var result = await _compression.CompressAsync(rawInput, (CompressionLevel?)level ?? _compression.Level, cancellationToken);

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