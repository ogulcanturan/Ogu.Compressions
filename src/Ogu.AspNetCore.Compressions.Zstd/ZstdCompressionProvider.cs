using Microsoft.Extensions.Options;
using Ogu.Compressions.Abstractions;
using System.IO;
using ICompressionProvider = Microsoft.AspNetCore.ResponseCompression.ICompressionProvider;

namespace Ogu.AspNetCore.Compressions
{
    /// <summary>
    /// Provides compression provider for the Zstandard (zstd) compression. 
    /// </summary>
    public class ZstdCompressionProvider : ICompressionProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ZstdCompressionProvider"/> class.
        /// </summary>
        /// <param name="opts">The options for configuring the <see cref="ZstdCompressionProvider"/>.</param>
        public ZstdCompressionProvider(IOptions<ZstdCompressionProviderOptions> opts)
        {
            Level = opts.Value.Level.ToZstdLevel();
            BufferSize = opts.Value.BufferSize;
            EncodingName = opts.Value.EncodingName;
        }

        public string EncodingName { get; }

        /// <summary>
        /// Gets the size, in bytes, of the buffer. The default size is 81920.
        /// </summary>
        /// <remarks>
        /// Value must be greater than zero.
        /// </remarks>
        public int BufferSize { get; }

        /// <summary>
        /// Gets the compression level to use for the underlying stream.
        /// </summary>
        public int Level { get; }

        public bool SupportsFlush => true;

        public Stream CreateStream(Stream outputStream)
        {
            return new ZstdSharp.CompressionStream(outputStream, Level, BufferSize, leaveOpen: true);
        }
    }
}