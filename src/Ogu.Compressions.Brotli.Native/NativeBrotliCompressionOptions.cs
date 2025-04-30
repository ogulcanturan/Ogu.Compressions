using Microsoft.Extensions.Options;
using Ogu.Compressions.Abstractions;
using System;
using System.IO.Compression;

namespace Ogu.Compressions.Brotli.Native
{
    /// <summary>
    /// Provides configuration options for the Brotli (br) compression, 
    /// including the compression level and buffer size.
    /// </summary>
    public sealed class NativeBrotliCompressionOptions : CompressionOptions, IOptions<NativeBrotliCompressionOptions>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NativeBrotliCompressionOptions"/> class 
        /// with default values for compression level (<see cref="CompressionLevel.Fastest"/>), buffer size (81920) and default window size (22).
        /// </summary>
        public NativeBrotliCompressionOptions()
        {
            WindowSize = CompressionDefaults.NativeBrotli.WindowSize;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NativeBrotliCompressionOptions"/> class 
        /// with compression level, default buffer size (81920) and default window size (22).
        /// </summary>
        public NativeBrotliCompressionOptions(CompressionLevel level) : base(level)
        {
            WindowSize = CompressionDefaults.NativeBrotli.WindowSize;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NativeBrotliCompressionOptions"/> class
        /// with the specified compression level, buffer size and default window size (22).
        /// </summary>
        /// <param name="level">The compression level to use.</param>
        /// <param name="bufferSize">The size, in bytes, of the buffer to use. The default buffer size is 81920 bytes and must be greater than zero.</param>
        public NativeBrotliCompressionOptions(CompressionLevel level, int bufferSize) : this(level, bufferSize, CompressionDefaults.NativeBrotli.WindowSize)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NativeBrotliCompressionOptions"/> class
        /// with the specified compression level and buffer size.
        /// </summary>
        /// <param name="level">The compression level to use.</param>
        /// <param name="bufferSize">The size, in bytes, of the buffer to use. The default buffer size is 81920 bytes and must be greater than zero.</param>
        /// <param name="windowSize">The size of the sliding window used in the native brotli compression algorithm. The default value is 22, and its valid range is from 10 to 24.</param>
        public NativeBrotliCompressionOptions(CompressionLevel level, int bufferSize, uint windowSize) : base(level, bufferSize)
        {
            WindowSize = windowSize < 10 || windowSize > 24
                ? Math.Min(24, Math.Max(10, windowSize))
                : windowSize;
        }

        /// <summary>
        /// Specifies the size of the sliding window used in the Brotli compression algorithm.
        /// This value represents the base-2 logarithm of the window size, and its valid range is from 10 to 24 (inclusive).
        /// <para>
        /// A higher window size (closer to 24) will increase compression efficiency by allowing the algorithm to reference a larger portion of the data, leading to better compression ratios.
        /// However, this comes at the cost of higher memory usage and potentially slower compression performance.
        /// </para>
        /// <para>
        /// A lower window size (closer to 10) will result in faster compression speeds and lower memory usage, but it may lead to less efficient compression and larger compressed files.
        /// </para>
        /// </summary>
        /// <remarks>
        /// The default value is typically 22 (approximately 4 MB), which offers a balance between compression efficiency and memory usage.
        /// </remarks>
        public uint WindowSize { get; set; }

        public override string EncodingName => CompressionDefaults.EncodingNames.Brotli;

        public override CompressionType Type => CompressionType.Brotli;

        NativeBrotliCompressionOptions IOptions<NativeBrotliCompressionOptions>.Value => this;
    }
}