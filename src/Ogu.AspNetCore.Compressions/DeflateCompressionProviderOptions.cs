using Microsoft.Extensions.Options;
using System.IO.Compression;

namespace Ogu.AspNetCore.Compressions
{
    public class DeflateCompressionProviderOptions : IOptions<DeflateCompressionProviderOptions>
    {
        /// <summary>
        /// The compression level to use for the stream.
        /// The default is <see cref="CompressionLevel.Fastest" />.
        /// </summary>
        public CompressionLevel Level { get; set; } = CompressionLevel.Fastest;

        DeflateCompressionProviderOptions IOptions<DeflateCompressionProviderOptions>.Value => this;
    }
}