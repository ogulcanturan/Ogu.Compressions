using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace Ogu.Compressions.Abstractions
{
    public static class MappingExtensions
    {
        private static readonly Lazy<Dictionary<CompressionType, string>> LazyCompressionTypeToEncodingName =
            new Lazy<Dictionary<CompressionType, string>>(() => new Dictionary<CompressionType, string>()
            {
                { CompressionType.Brotli, EncodingNames.Brotli },
                { CompressionType.Deflate, EncodingNames.Deflate },
                { CompressionType.Snappy, EncodingNames.Snappy },
                { CompressionType.Zstd, EncodingNames.Zstd },
                { CompressionType.Gzip, EncodingNames.Gzip },
                { CompressionType.None, string.Empty }
            });

        public static string ToEncodingName(this CompressionType compressionType)
        {
#if NETSTANDARD2_0
            return LazyCompressionTypeToEncodingName.Value.TryGetValue(compressionType, out var encodingName) 
                ? encodingName 
                : string.Empty;
#else
            return LazyCompressionTypeToEncodingName.Value.GetValueOrDefault(compressionType, string.Empty);
#endif
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