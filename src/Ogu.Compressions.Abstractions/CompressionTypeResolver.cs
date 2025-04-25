using System;
using System.Collections.Generic;
using System.Linq;

namespace Ogu.Compressions.Abstractions
{
    /// <summary>
    /// Implementation of <see cref="ICompressionTypeResolver"/> that resolves compression types based on encoding names.
    /// </summary>
    public sealed class CompressionTypeResolver : ICompressionTypeResolver
    {
        private readonly Dictionary<string, CompressionType> _encodingNameToCompressionType;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompressionTypeResolver"/> class.
        /// This constructor populates the internal dictionary with predefined encoding names and their associated <see cref="CompressionType"/> values.
        /// </summary>
        /// <remarks>
        /// The dictionary is initialized with these encoding names as br, deflate, snappy, zstd, gzip, and none.
        /// The encoding names are case-insensitive and include both the official names and aliases such as brotli, zstandard.
        /// </remarks>
        public CompressionTypeResolver()
        {
            _encodingNameToCompressionType = new Dictionary<string, CompressionType>(StringComparer.OrdinalIgnoreCase)
            {
                { "brotli", CompressionType.Brotli },
                { CompressionDefaults.EncodingNames.Brotli, CompressionType.Brotli },
                { CompressionDefaults.EncodingNames.Deflate, CompressionType.Deflate },
                { CompressionDefaults.EncodingNames.Snappy, CompressionType.Snappy },
                { "zstandard", CompressionType.Zstd },
                { CompressionDefaults.EncodingNames.Zstd, CompressionType.Zstd },
                { CompressionDefaults.EncodingNames.Gzip, CompressionType.Gzip },
                { CompressionDefaults.EncodingNames.None, CompressionType.None }
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompressionTypeResolver"/> class.
        /// </summary>
        /// <param name="encodingNameToCompressionType">
        /// An enumerable collection of key-value pairs where each key is an encoding name (e.g., "br", "gzip", etc.),
        /// and the corresponding value is the associated <see cref="CompressionType"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="encodingNameToCompressionType"/> is null.</exception>
        public CompressionTypeResolver(IEnumerable<KeyValuePair<string, CompressionType>> encodingNameToCompressionType)
        {
            if (encodingNameToCompressionType == null)
            {
                throw new ArgumentNullException(nameof(encodingNameToCompressionType));
            }

            _encodingNameToCompressionType = encodingNameToCompressionType
                .GroupBy(kv => kv.Key, StringComparer.OrdinalIgnoreCase)
                .ToDictionary(g => g.Key, g => g.First().Value, StringComparer.OrdinalIgnoreCase);
        }

        public bool TryResolve(string encodingName, out CompressionType type)
        {
            return _encodingNameToCompressionType.TryGetValue(encodingName, out type);
        }
    }
}