using Microsoft.Extensions.Options;
using Ogu.Compressions.Abstractions;
using System.IO;
using System.IO.Compression;
using ICompressionProvider = Microsoft.AspNetCore.ResponseCompression.ICompressionProvider;

namespace Ogu.AspNetCore.Compressions
{
    public class DeflateCompressionProvider : ICompressionProvider
    {
        public DeflateCompressionProvider(IOptions<DeflateCompressionProviderOptions> opts)
        {
            Level = opts.Value.Level;
        }

        public string EncodingName => EncodingNames.Deflate;

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