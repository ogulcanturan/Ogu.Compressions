using Ogu.Compressions.Abstractions;
using Ogu.Compressions.Brotli.Native;
using System.IO.Compression;

namespace Ogu.Compressions.Tests.Brotli.Native
{
    public class NativeBrotliCompressionOptionsTests
    {
        [Fact]
        public void Constructor_WhenCalled_InitializesCorrectly()
        {
            // Act
            var options = new NativeBrotliCompressionOptions();

            // Assert
            Assert.Equal(CompressionLevel.Fastest, options.Level);
            Assert.Equal(CompressionDefaults.BufferSize, options.BufferSize);
            Assert.Equal(CompressionDefaults.EncodingNames.Brotli, options.EncodingName);
            Assert.Equal(CompressionType.Brotli, options.Type);
            Assert.Equal(CompressionDefaults.NativeBrotli.WindowSize, options.WindowSize);
        }

        [Fact]
        public void Constructor_WhenCalled_WithLevel_InitializesCorrectly()
        {
            // Arrange
            const CompressionLevel level = CompressionLevel.NoCompression;

            // Act
            var options = new NativeBrotliCompressionOptions(level);

            // Assert
            Assert.Equal(level, options.Level);
            Assert.Equal(CompressionDefaults.BufferSize, options.BufferSize);
            Assert.Equal(CompressionDefaults.EncodingNames.Brotli, options.EncodingName);
            Assert.Equal(CompressionType.Brotli, options.Type);
            Assert.Equal(CompressionDefaults.NativeBrotli.WindowSize, options.WindowSize);
        }

        [Fact]
        public void Constructor_WhenCalled_WithLevelAndBufferSize_InitializesCorrectly()
        {
            // Arrange
            const CompressionLevel level = CompressionLevel.Optimal;
            const int bufferSize = 4096;

            // Act
            var options = new NativeBrotliCompressionOptions(level, bufferSize);

            // Assert
            Assert.Equal(level, options.Level);
            Assert.Equal(bufferSize, options.BufferSize);
            Assert.Equal(CompressionDefaults.EncodingNames.Brotli, options.EncodingName);
            Assert.Equal(CompressionType.Brotli, options.Type);
            Assert.Equal(CompressionDefaults.NativeBrotli.WindowSize, options.WindowSize);
        }

        [Fact]
        public void Constructor_WhenCalled_WithLevelBufferSizeAndWindowSize_InitializesCorrectly()
        {
            // Arrange
            const CompressionLevel level = CompressionLevel.SmallestSize;
            const int bufferSize = 8192;
            const uint windowSize = 20;

            // Act
            var options = new NativeBrotliCompressionOptions(level, bufferSize, windowSize);

            // Assert
            Assert.Equal(level, options.Level);
            Assert.Equal(bufferSize, options.BufferSize);
            Assert.Equal(CompressionDefaults.EncodingNames.Brotli, options.EncodingName);
            Assert.Equal(CompressionType.Brotli, options.Type);
            Assert.Equal(windowSize, options.WindowSize);
        }
    }
}