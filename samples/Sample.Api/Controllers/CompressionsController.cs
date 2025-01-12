using Microsoft.AspNetCore.Mvc;
using Ogu.Compressions.Abstractions;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Api.Controllers
{
    [Route("api/compressions")]
    public class CompressionsController : ControllerBase
    {
        private readonly ICompressionFactory _compression;

        public CompressionsController(ICompressionFactory compression)
        {
            _compression = compression;
        }

        [HttpPost("compress")]
        public async Task<IActionResult> Compress([FromBody] string rawInput, [FromQuery] CompressionType compressionType, CancellationToken cancellationToken = default)
        {
            var result = await _compression.CompressAsync(compressionType, rawInput, cancellationToken);

            return Ok(Convert.ToBase64String(result));
        }

        [HttpPost("decompress")]
        public async Task<IActionResult> Decompress([FromBody] string base64CompressedInput, [FromQuery] CompressionType compressionType, CancellationToken cancellationToken = default)
        {
            var bytes = Convert.FromBase64String(base64CompressedInput);

            var result = await _compression.DecompressAsync(compressionType, bytes, cancellationToken);

            return Ok(Encoding.UTF8.GetString(result));
        }
    }
}