using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Options;
using Ogu.Compressions.Abstractions;
using System.IO;

namespace Ogu.AspNetCore.Compressions
{
    public class ZstdCompressionProvider : ICompressionProvider
    {
        private readonly int _compressionLevel;
        private readonly int _bufferSize;

        public ZstdCompressionProvider(IOptions<ZstdCompressionProviderOptions> options)
        {
            var optionsValue = options.Value;

            _compressionLevel = optionsValue.Level.ToZstdLevel();
            _bufferSize = optionsValue.BufferSize;
        }

        public string EncodingName => EncodingNames.Zstd;

        public bool SupportsFlush => true;

        public Stream CreateStream(Stream outputStream)
        {
            return new ZstdSharp.CompressionStream(outputStream, _compressionLevel, _bufferSize, leaveOpen: true);
        }
    }
}