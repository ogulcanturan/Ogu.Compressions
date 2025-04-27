using Microsoft.Extensions.Options;
using System.IO;
using System.IO.Compression;
using ICompressionProvider = Microsoft.AspNetCore.ResponseCompression.ICompressionProvider;

namespace Ogu.AspNetCore.Compressions
{
    /// <summary>
    /// Provides compression provider for the Deflate (deflate) compression. 
    /// </summary>
    public class DeflateCompressionProvider : ICompressionProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeflateCompressionProvider"/> class.
        /// </summary>
        /// <param name="opts">The options for configuring the <see cref="DeflateCompressionProvider"/>.</param>
        public DeflateCompressionProvider(IOptions<DeflateCompressionProviderOptions> opts)
        {
            Level = opts.Value.Level;
            EncodingName = opts.Value.EncodingName;
        }

        public string EncodingName { get; }

        /// <summary>
        /// Gets the compression level to use for the underlying stream.
        /// </summary>
        public CompressionLevel Level { get; }

        public bool SupportsFlush => true;

        public Stream CreateStream(Stream outputStream)
        {
            return new DeflateStream(outputStream, Level, leaveOpen: true);
        }
    }
}