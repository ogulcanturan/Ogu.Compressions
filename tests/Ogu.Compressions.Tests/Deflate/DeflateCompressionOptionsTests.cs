using Ogu.Compressions.Abstractions;
using System.IO.Compression;

namespace Ogu.Compressions.Tests.Deflate
{
    public class DeflateCompressionOptionsTests
    {
        [Fact]
        public void Constructor_WhenCalled_InitializesCorrectly()
        {
            // Act
            var options = new DeflateCompressionOptions();

            // Assert
            Assert.Equal(CompressionLevel.Fastest, options.Level);
            Assert.Equal(CompressionDefaults.BufferSize, options.BufferSize);
            Assert.Equal(CompressionDefaults.EncodingNames.Deflate, options.EncodingName);
            Assert.Equal(CompressionType.Deflate, options.Type);
        }

        [Fact]
        public void Constructor_WhenCalled_WithParams_InitializesCorrectly()
        {
            // Arrange
            const CompressionLevel level = CompressionLevel.Optimal;
            const int bufferSize = 4096;

            // Act
            var options = new DeflateCompressionOptions(level, bufferSize);

            // Assert
            Assert.Equal(level, options.Level);
            Assert.Equal(bufferSize, options.BufferSize);
            Assert.Equal(CompressionDefaults.EncodingNames.Deflate, options.EncodingName);
            Assert.Equal(CompressionType.Deflate, options.Type);
        }
    }
}