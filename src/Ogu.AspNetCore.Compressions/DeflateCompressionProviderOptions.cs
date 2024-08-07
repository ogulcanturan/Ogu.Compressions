using Microsoft.Extensions.Options;
using System.IO.Compression;

namespace Ogu.AspNetCore.Compressions
{
    public class DeflateCompressionProviderOptions : IOptions<DeflateCompressionProviderOptions>
    {
        /// <summary>
        /// What level of compression to use for the stream. The default is Fastest.
        /// </summary>
        public CompressionLevel Level { get; set; } = CompressionLevel.Fastest;

        /// <inheritdoc />
        DeflateCompressionProviderOptions IOptions<DeflateCompressionProviderOptions>.Value => this;
    }
}