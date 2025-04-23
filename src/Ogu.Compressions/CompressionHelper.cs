using Ogu.Compressions.Abstractions;
using System;
using System.Collections.Generic;

namespace Ogu.Compressions
{
    internal static class CompressionHelper
    {
        private static readonly Lazy<Dictionary<string, CompressionType>> LazyEncodingNameToCompressionType =
            new Lazy<Dictionary<string, CompressionType>>(() => new Dictionary<string, CompressionType>(StringComparer.OrdinalIgnoreCase)
            {
                { "brotli", CompressionType.Brotli },
                { EncodingNames.Brotli, CompressionType.Brotli },
                { EncodingNames.Deflate, CompressionType.Deflate },
                { EncodingNames.Snappy, CompressionType.Snappy },
                { "zstandard", CompressionType.Zstd },
                { EncodingNames.Zstd, CompressionType.Zstd },
                { EncodingNames.Gzip, CompressionType.Gzip },
                { EncodingNames.None, CompressionType.None }
            });

        /// <summary>
        /// Attempts to convert a given encoding name to its corresponding <see cref="CompressionType"/>.
        /// </summary>
        /// <param name="encodingName">
        /// The name of the encoding (e.g. <c>"br"</c>, <c>"deflate"</c>, <c>"snappy"</c>, <c>"zstd"</c>, <c>"gzip"</c>, or <c>"none"</c>).
        /// The value <c>"none"</c> is used internally to indicate no compression and does not correspond to a real encoding header.
        /// </param>
        /// <param name="compressionType">
        /// Contains the <see cref="CompressionType"/> associated with the encoding name,
        /// if the conversion succeeded; otherwise, contains the default value which is <see cref="CompressionType.None" />.</param>
        /// <returns><c>true</c> if the encoding name was successfully converted; otherwise, <c>false</c>.</returns>
        public static bool TryConvertEncodingNameToCompressionType(string encodingName, out CompressionType compressionType)
        {
            return LazyEncodingNameToCompressionType.Value.TryGetValue(encodingName, out compressionType);
        }
    }
}