using Ogu.Compressions.Abstractions;
using System.IO.Compression;

namespace Ogu.Compressions.Tests.None
{
    public class NoneCompressionOptionsTests
    {
        [Fact]
        public void Constructor_WhenCalled_InitializesCorrectly()
        {
            // Act
            var options = new NoneCompressionOptions();

            // Assert
            Assert.Equal(CompressionLevel.Fastest, options.Level);
            Assert.Equal(CompressionDefaults.BufferSize, options.BufferSize);
            Assert.Equal(CompressionDefaults.EncodingNames.None, options.EncodingName);
            Assert.Equal(CompressionType.None, options.Type);
        }

        [Fact]
        public void Constructor_WhenCalled_WithLevel_InitializesCorrectly()
        {
            // Arrange
            const CompressionLevel level = CompressionLevel.NoCompression;

            // Act
            var options = new NoneCompressionOptions(level);

            // Assert
            Assert.Equal(level, options.Level);
            Assert.Equal(CompressionDefaults.BufferSize, options.BufferSize);
            Assert.Equal(CompressionDefaults.EncodingNames.None, options.EncodingName);
            Assert.Equal(CompressionType.None, options.Type);
        }

        [Fact]
        public void Constructor_WhenCalled_WithLevelAndBufferSize_InitializesCorrectly()
        {
            // Arrange
            const CompressionLevel level = CompressionLevel.Optimal;
            const int bufferSize = 4096;

            // Act
            var options = new NoneCompressionOptions(level, bufferSize);

            // Assert
            Assert.Equal(level, options.Level);
            Assert.Equal(bufferSize, options.BufferSize);
            Assert.Equal(CompressionDefaults.EncodingNames.None, options.EncodingName);
            Assert.Equal(CompressionType.None, options.Type);
        }
    }
}