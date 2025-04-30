using Ogu.Compressions.Abstractions;
using System;

namespace Ogu.Compressions
{
    /// <summary>
    /// Configuration options for registering compressions.
    /// Allows selection between native and managed Brotli implementations,
    /// and provides a delegate to configure shared compression options.
    /// </summary>
    public class CompressionRegistrationOptions
    {
        /// <summary>
        /// Indicates whether to use the native brotli implementation.
        /// If false, the managed brotli (System.IO.Compression.Brotli) compression will be used.
        /// </summary>
        /// <remarks>
        /// Default value is <c>false</c>.
        /// </remarks>
        public bool UseNativeBrotli { get; set; }

        /// <summary>
        /// A delegate to configure shared <see cref="CompressionOptions"/>
        /// applied to all registered compression implementations.
        /// </summary>
        public Action<CompressionOptions> CompressionOptions { get; set; }
    }
}