using Microsoft.Extensions.Options;
using Ogu.Compressions.Abstractions;
using System.IO.Compression;

namespace Ogu.AspNetCore.Compressions
{
    /// <summary>
    /// Provides configuration options for the Deflate (deflate) compression, 
    /// including the compression level.
    /// </summary>
    public class DeflateCompressionProviderOptions : IOptions<DeflateCompressionProviderOptions>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeflateCompressionProviderOptions"/> class 
        /// with default compression level (<see cref="CompressionLevel.Fastest"/>).
        /// </summary>
        public DeflateCompressionProviderOptions() : this(CompressionLevel.Fastest)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeflateCompressionProviderOptions"/> class 
        /// with compression level.
        /// </summary>
        /// <param name="level">The compression level to use.</param>
        public DeflateCompressionProviderOptions(CompressionLevel level)
        {
            Level = level;
        }

        /// <summary>
        /// Gets the name of the encoding used for underlying compression.
        /// </summary>
        public string EncodingName => CompressionDefaults.EncodingNames.Deflate;

        /// <summary>
        /// The compression level to use for the stream.
        /// The default is <see cref="CompressionLevel.Fastest" />.
        /// </summary>
        public CompressionLevel Level { get; }

        DeflateCompressionProviderOptions IOptions<DeflateCompressionProviderOptions>.Value => this;
    }
}