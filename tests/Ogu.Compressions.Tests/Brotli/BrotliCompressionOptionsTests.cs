using Ogu.Compressions.Abstractions;
using System.IO.Compression;

namespace Ogu.Compressions.Tests.Brotli
{
    public class BrotliCompressionOptionsTests
    {
        [Fact]
        public void Constructor_WhenCalled_InitializesCorrectly()
        {
            // Act
            var options = new BrotliCompressionOptions();

            // Assert
            Assert.Equal(CompressionLevel.Fastest, options.Level);
            Assert.Equal(CompressionDefaults.BufferSize, options.BufferSize);
            Assert.Equal(CompressionDefaults.EncodingNames.Brotli, options.EncodingName);
            Assert.Equal(CompressionType.Brotli, options.Type);
        }

        [Fact]
        public void Constructor_WhenCalled_WithLevel_InitializesCorrectly()
        {
            // Arrange
            const CompressionLevel level = CompressionLevel.NoCompression;

            // Act
            var options = new BrotliCompressionOptions(level);

            // Assert
            Assert.Equal(level, options.Level);
            Assert.Equal(CompressionDefaults.BufferSize, options.BufferSize);
            Assert.Equal(CompressionDefaults.EncodingNames.Brotli, options.EncodingName);
            Assert.Equal(CompressionType.Brotli, options.Type);
        }

        [Fact]
        public void Constructor_WhenCalled_WithLevelAndBufferSize_InitializesCorrectly()
        {
            // Arrange
            const CompressionLevel level = CompressionLevel.Optimal;
            const int bufferSize = 4096;

            // Act
            var options = new BrotliCompressionOptions(level, bufferSize);

            // Assert
            Assert.Equal(level, options.Level);
            Assert.Equal(bufferSize, options.BufferSize);
            Assert.Equal(CompressionDefaults.EncodingNames.Brotli, options.EncodingName);
            Assert.Equal(CompressionType.Brotli, options.Type);
        }
    }
}