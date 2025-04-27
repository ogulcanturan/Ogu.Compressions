using Ogu.Compressions.Abstractions;
using System.IO.Compression;

namespace Ogu.Compressions.Tests.Gzip
{
    public class GzipCompressionOptionsTests
    {
        [Fact]
        public void Constructor_WhenCalled_InitializesCorrectly()
        {
            // Act
            var options = new GzipCompressionOptions();

            // Assert
            Assert.Equal(CompressionLevel.Fastest, options.Level);
            Assert.Equal(CompressionDefaults.BufferSize, options.BufferSize);
            Assert.Equal(CompressionDefaults.EncodingNames.Gzip, options.EncodingName);
            Assert.Equal(CompressionType.Gzip, options.Type);
        }

        [Fact]
        public void Constructor_WhenCalled_WithLevel_InitializesCorrectly()
        {
            // Arrange
            const CompressionLevel level = CompressionLevel.NoCompression;

            // Act
            var options = new GzipCompressionOptions(level);

            // Assert
            Assert.Equal(level, options.Level);
            Assert.Equal(CompressionDefaults.BufferSize, options.BufferSize);
            Assert.Equal(CompressionDefaults.EncodingNames.Gzip, options.EncodingName);
            Assert.Equal(CompressionType.Gzip, options.Type);
        }

        [Fact]
        public void Constructor_WhenCalled_WithLevelAndBufferSize_InitializesCorrectly()
        {
            // Arrange
            const CompressionLevel level = CompressionLevel.Optimal;
            const int bufferSize = 4096;

            // Act
            var options = new GzipCompressionOptions(level, bufferSize);

            // Assert
            Assert.Equal(level, options.Level);
            Assert.Equal(bufferSize, options.BufferSize);
            Assert.Equal(CompressionDefaults.EncodingNames.Gzip, options.EncodingName);
            Assert.Equal(CompressionType.Gzip, options.Type);
        }
    }
}