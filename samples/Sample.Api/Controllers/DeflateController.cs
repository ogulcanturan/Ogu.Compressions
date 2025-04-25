using Microsoft.AspNetCore.Mvc;
using Ogu.Compressions.Abstractions;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Api.Controllers
{
    [Route("api/compressions/[controller]")]
    public class DeflateController : ControllerBase
    {
        private readonly IDeflateCompression _compression;

        public DeflateController(IDeflateCompression compression)
        {
            _compression = compression;
        }

        [HttpPost("compress")]
        public async Task<IActionResult> Compress([FromBody] string rawInput, CancellationToken cancellationToken = default)
        {
            var result = await _compression.CompressAsync(rawInput, cancellationToken);

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