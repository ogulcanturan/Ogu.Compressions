using Ogu.Compressions.Abstractions;
using System;
using System.Collections.Generic;

namespace Ogu.Compressions
{
    internal static class CompressionHelpers
    {
        private static readonly Lazy<Dictionary<string, CompressionType>> LazyEncodingNameToCompressionType =
            new Lazy<Dictionary<string, CompressionType>>(() => new Dictionary<string, CompressionType>(StringComparer.OrdinalIgnoreCase)
            {
                { "brotli", CompressionType.Brotli },
                { EncodingNames.Brotli, CompressionType.Brotli },
                { EncodingNames.Deflate, CompressionType.Deflate },
                { EncodingNames.Snappy, CompressionType.Snappy },
                { EncodingNames.Zstd, CompressionType.Zstd },
                { EncodingNames.Gzip, CompressionType.Gzip }
            });

        public static bool TryConvertEncodingNameToCompressionType(string encodingName, out CompressionType compressionType)
        {
            return LazyEncodingNameToCompressionType.Value.TryGetValue(encodingName, out compressionType);
        }
    }
}