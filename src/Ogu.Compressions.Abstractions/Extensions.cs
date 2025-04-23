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
            new Lazy<Dictionary<CompressionType, string>>(() => new Dictionary<CompressionType, string>
            {
                { CompressionType.Brotli, EncodingNames.Brotli },
                { CompressionType.Deflate, EncodingNames.Deflate },
                { CompressionType.Snappy, EncodingNames.Snappy },
                { CompressionType.Zstd, EncodingNames.Zstd },
                { CompressionType.Gzip, EncodingNames.Gzip },
                { CompressionType.None, EncodingNames.None }
            });

        /// <summary>
        /// Converts a <see cref="CompressionType" /> to its corresponding encoding name.
        /// </summary>
        /// <param name="compressionType">The compression type to convert.</param>
        /// <returns>The encoding name as a string.</returns>
        /// <remarks>
        /// <list type="bullet">
        ///   <item>
        ///     <description><see cref="CompressionType.Brotli" /> → <c>br</c></description>
        ///   </item>
        ///   <item>
        ///     <description><see cref="CompressionType.Deflate" /> → <c>deflate</c></description>
        ///   </item>
        ///   <item>
        ///     <description><see cref="CompressionType.Snappy" /> → <c>snappy</c></description>
        ///   </item>
        ///   <item>
        ///     <description><see cref="CompressionType.Zstd"/> → <c>zstd</c></description>
        ///   </item>
        ///   <item>
        ///     <description><see cref="CompressionType.Gzip"/> → <c>gzip</c></description>
        ///   </item>
        ///   <item>
        ///     <description><see cref="CompressionType.None"/> → <c>none</c> (used internally to indicate no compression; does not correspond to a real encoding header)</description>
        ///   </item>
        /// </list>
        /// </remarks>
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

        /// <summary>
        /// Converts a <see cref="CompressionLevel" /> to the closest matching <see cref="CompressionType.Zstd" /> compression level.
        /// </summary>
        /// <param name="level">The .NET <see cref="CompressionLevel" /> to map from.</param>
        /// <returns>The corresponding <see cref="CompressionType.Zstd" /> compression level.</returns>
        /// <remarks>
        /// <list type="bullet">
        ///   <item>
        ///     <description><see cref="CompressionLevel.Optimal"/> → 3 (balanced compression and speed)</description>
        ///   </item>
        ///   <item>
        ///     <description><see cref="CompressionLevel.SmallestSize"/> → 22 (maximum compression, slowest)</description>
        ///   </item>
        ///   <item>
        ///     <description><see cref="CompressionLevel.Fastest"/> → -131072 (fastest compression, minimal compression ratio)</description>
        ///   </item>
        ///   <item>
        ///     <description><see cref="CompressionLevel.NoCompression"/> → -131072 (effectively disables compression)</description>
        ///   </item>
        /// </list>
        /// Custom values can be provided by casting an integer to <see cref="CompressionLevel"/>, e.g.,
        /// <c>(CompressionLevel)5</c> will return 5, clamped between -131072 and 22.
        /// </remarks>
        public static int ToZstdLevel(this CompressionLevel level)
        {
            switch (level)
            {
                case CompressionLevel.Optimal:
                    return 3;
#if NET6_0_OR_GREATER
                case CompressionLevel.SmallestSize:
                    return 22;
#endif
                case CompressionLevel.Fastest:
                case CompressionLevel.NoCompression:
                    return -131072;
                default:
                    return Math.Max(22, Math.Min(-131072, (int)level));
            }
        }

        /// <summary>
        /// Adds the specified <see cref="CompressionType"/> to the `Accept-Encoding` header of the request.
        /// If the compression type is <see cref="CompressionType.None"/>, no header is added.
        /// </summary>
        /// <param name="headers">The HTTP request headers to modify.</param>
        /// <param name="compressionType">The compression type to add.</param>
        public static void AddCompressionType(this HttpRequestHeaders headers, CompressionType compressionType)
        {
            compressionType.AddToRequestHeaders(headers);
        }

        /// <summary>
        /// Adds the specified <see cref="CompressionType"/> to the <c>Accept-Encoding</c> header of the request,
        /// using the given quality value. If the compression type is <see cref="CompressionType.None"/>, no header is added.
        /// </summary>
        /// <param name="requestHeaders">The HTTP request headers to modify.</param>
        /// <param name="compressionType">The compression type to add.</param>
        /// <param name="quality">The quality value (between 0.0 and 1.0) indicating the relative preference of the encoding.</param>
        public static void AddCompressionType(this HttpRequestHeaders requestHeaders, CompressionType compressionType, double quality)
        {
            compressionType.AddToRequestHeaders(requestHeaders, quality);
        }

        /// <summary>
        /// Adds each specified <see cref="CompressionType"/> to the <c>Accept-Encoding</c> header of the request,
        /// with its corresponding quality value. Compression types with <see cref="CompressionType.None"/> are ignored.
        /// </summary>
        /// <param name="requestHeaders">The HTTP request headers to modify.</param>
        /// <param name="compressionTypeToQuality">
        /// A collection of key-value pairs where the key is a <see cref="CompressionType"/>,
        /// and the value is the quality factor (between 0.0 and 1.0).
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="compressionTypeToQuality"/> is <c>null</c>.</exception>
        public static void AddCompressionTypes(this HttpRequestHeaders requestHeaders, IEnumerable<KeyValuePair<CompressionType, double>> compressionTypeToQuality)
        {
            compressionTypeToQuality.AddToRequestHeaders(requestHeaders);
        }

        /// <summary>
        /// Adds the specified <see cref="CompressionType"/> to the `Accept-Encoding` header of the request.
        /// If the compression type is <see cref="CompressionType.None"/>, no header is added.
        /// </summary>
        /// <param name="compressionType">The compression type to add.</param>
        /// <param name="requestHeaders">The HTTP request headers.</param>
        public static void AddToRequestHeaders(this CompressionType compressionType, HttpRequestHeaders requestHeaders)
        {
            if (compressionType == CompressionType.None)
            {
                return;
            }

            requestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue(compressionType.ToEncodingName()));
        }

        /// <summary>
        /// Adds the specified <see cref="CompressionType"/> to the `Accept-Encoding` header of the request.
        /// If the compression type is <see cref="CompressionType.None"/>, no header is added.
        /// </summary>
        /// <param name="compressionType">The compression type to add.</param>
        /// <param name="requestHeaders">The HTTP request headers.</param>
        /// <param name="quality">The quality value (between 0.0 and 1.0) indicating the relative preference of the encoding.</param>
        public static void AddToRequestHeaders(this CompressionType compressionType, HttpRequestHeaders requestHeaders, double quality)
        {
            if (compressionType == CompressionType.None)
            {
                return;
            }

            requestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue(compressionType.ToEncodingName(), quality));
        }

        /// <summary>
        /// Adds the specified collection of <see cref="CompressionType"/> values to the <c>Accept-Encoding</c>
        /// header of the HTTP request. Compression types with a value of <see cref="CompressionType.None"/> are ignored.
        /// </summary>
        /// <param name="compressionTypes">A collection of compression types to add.</param>
        /// <param name="requestHeaders">The HTTP request headers to modify.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="compressionTypes"/> is <c>null</c>.</exception>
        public static void AddToRequestHeaders(this IEnumerable<CompressionType> compressionTypes, HttpRequestHeaders requestHeaders)
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
                requestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue(encodings));
            }
        }

        /// <summary>
        /// Adds each specified <see cref="CompressionType"/> to the <c>Accept-Encoding</c> header of the request,
        /// with its corresponding quality value. Compression types with <see cref="CompressionType.None"/> are ignored.
        /// </summary>
        /// <param name="compressionTypeToQuality">
        /// A collection of key-value pairs where the key is a <see cref="CompressionType"/>,
        /// and the value is the quality factor (between 0.0 and 1.0).
        /// </param>
        /// <param name="requestHeaders">The HTTP request headers to modify.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="compressionTypeToQuality"/> is <c>null</c>.</exception>
        public static void AddToRequestHeaders(this IEnumerable<KeyValuePair<CompressionType, double>> compressionTypeToQuality, HttpRequestHeaders requestHeaders)
        {
            if (compressionTypeToQuality == null)
            {
                throw new ArgumentNullException(nameof(compressionTypeToQuality));
            }

            foreach (var kvp in compressionTypeToQuality)
            {
                if (kvp.Key == CompressionType.None)
                {
                    continue;
                }

                requestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue(kvp.Key.ToEncodingName(), kvp.Value));
            }
        }
    }
}