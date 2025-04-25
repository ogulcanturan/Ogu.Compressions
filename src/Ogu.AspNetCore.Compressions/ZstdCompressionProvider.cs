using Microsoft.Extensions.Options;
using Ogu.Compressions.Abstractions;
using System.IO;
using ICompressionProvider = Microsoft.AspNetCore.ResponseCompression.ICompressionProvider;

namespace Ogu.AspNetCore.Compressions
{
    public class ZstdCompressionProvider : ICompressionProvider
    {
        public ZstdCompressionProvider(IOptions<ZstdCompressionProviderOptions> options)
        {
            var optionsValue = options.Value;

            Level = optionsValue.Level.ToZstdLevel();
            BufferSize = optionsValue.BufferSize;
        }

        public string EncodingName => EncodingNames.Zstd;

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