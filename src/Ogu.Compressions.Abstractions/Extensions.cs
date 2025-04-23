using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Net.Http.Headers;

namespace Ogu.Compressions.Abstractions
{
    public static class Extensions
    {
        private static readonly Lazy<Dictionary<CompressionType, string>> LazyCompressionTypeToEncodingName =
            new Lazy<Dictionary<CompressionType, string>>(() => new Dictionary<CompressionType, string>()
            {
                { CompressionType.Brotli, EncodingNames.Brotli },
                { CompressionType.Deflate, EncodingNames.Deflate },
                { CompressionType.Snappy, EncodingNames.Snappy },
                { CompressionType.Zstd, EncodingNames.Zstd },
                { CompressionType.Gzip, EncodingNames.Gzip },
                { CompressionType.None, EncodingNames.None }
            });

        public static string ToEncodingName(this CompressionType compressionType)
        {
#if NETSTANDARD2_0
            return LazyCompressionTypeToEncodingName.Value.TryGetValue(compressionType, out var encodingName) 
                ? encodingName 
                : EncodingNames.None;
#else
            return LazyCompressionTypeToEncodingName.Value.GetValueOrDefault(compressionType, EncodingNames.None);
#endif
        }

        public static int ToZstdLevel(this CompressionLevel level)
        {
            switch (level)
            {
                case CompressionLevel.Optimal:
#if NET6_0_OR_GREATER
                case CompressionLevel.SmallestSize:
#endif
                    return 22;
                case CompressionLevel.Fastest:
                    return -131072;
                case CompressionLevel.NoCompression:
                default:
                    return 3;
            }
        }

        public static void AddToRequestHeaders(this CompressionType compressionType, HttpRequestHeaders headers)
        {
            if (compressionType == CompressionType.None)
            {
                return;
            }

            headers.AcceptEncoding.Add(new StringWithQualityHeaderValue(compressionType.ToEncodingName()));
        }

        public static void AddToRequestHeaders(this CompressionType compressionType, HttpRequestHeaders headers, double quality)
        {
            if (compressionType == CompressionType.None)
            {
                return;
            }

            headers.AcceptEncoding.Add(new StringWithQualityHeaderValue(compressionType.ToEncodingName(), quality));
        }

        public static void AddToRequestHeaders(this ICollection<CompressionType> compressionTypes, HttpRequestHeaders headers)
        {
            if (compressionTypes == null)
            {
                throw new ArgumentNullException(nameof(compressionTypes));
            }

            var encodings =
#if NETSTANDARD2_0
                string.Join(",", compressionTypes.Where(c => c != CompressionType.None).Select(c => c.ToEncodingName()));
#else
                string.Join(',', compressionTypes.Where(c => c != CompressionType.None).Select(c => c.ToEncodingName()));
#endif

            if (encodings != string.Empty)
            {
                headers.AcceptEncoding.Add(new StringWithQualityHeaderValue(encodings));
            }
        }

        public static void AddToRequestHeaders(this ICollection<CompressionType> compressionTypes, HttpRequestHeaders headers, double quality)
        {
            if (compressionTypes == null)
            {
                throw new ArgumentNullException(nameof(compressionTypes));
            }

            var encodings =
#if NETSTANDARD2_0
                string.Join(",", compressionTypes.Where(c => c != CompressionType.None).Select(c => c.ToEncodingName()));
#else
                string.Join(',', compressionTypes.Where(c => c != CompressionType.None).Select(c => c.ToEncodingName()));
#endif

            if (encodings != string.Empty)
            {
                headers.AcceptEncoding.Add(new StringWithQualityHeaderValue(encodings, quality));
            }
        }
    }
}