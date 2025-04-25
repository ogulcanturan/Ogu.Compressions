using Ogu.Compressions.Abstractions;

namespace Ogu.Compressions.Tests.Abstractions
{
    public class CompressionTypeResolverTests
    {
        [Fact]
        public void Constructor_WhenCalled_InitializesCorrectly()
        {
            // Act
            var compressionTypeResolver = new CompressionTypeResolver();

            // Assert
            Assert.True(compressionTypeResolver.TryResolve("brotli", out _));
            Assert.True(compressionTypeResolver.TryResolve(CompressionDefaults.EncodingNames.Brotli, out _));
            Assert.True(compressionTypeResolver.TryResolve(CompressionDefaults.EncodingNames.Deflate, out _));
            Assert.True(compressionTypeResolver.TryResolve(CompressionDefaults.EncodingNames.Snappy, out _));
            Assert.True(compressionTypeResolver.TryResolve("zstandard", out _));
            Assert.True(compressionTypeResolver.TryResolve(CompressionDefaults.EncodingNames.Zstd, out _));
            Assert.True(compressionTypeResolver.TryResolve(CompressionDefaults.EncodingNames.Gzip, out _));
            Assert.True(compressionTypeResolver.TryResolve(CompressionDefaults.EncodingNames.None, out _));

            Assert.True(compressionTypeResolver.TryResolve("BROTLI", out _));
            Assert.True(compressionTypeResolver.TryResolve("bR", out _));
            Assert.True(compressionTypeResolver.TryResolve("DEFLATE", out _));
            Assert.True(compressionTypeResolver.TryResolve("SnaPpY", out _));
            Assert.True(compressionTypeResolver.TryResolve("ZStandaRd", out _));
            Assert.True(compressionTypeResolver.TryResolve("ZStD", out _));
            Assert.True(compressionTypeResolver.TryResolve("GZIP", out _));
            Assert.True(compressionTypeResolver.TryResolve("NoNe", out _));
            Assert.False(compressionTypeResolver.TryResolve("some", out _));
        }

        [Fact]
        public void Constructor_WhenCalled_WithEncodingNameToCompressionTypeParam_InitializesCorrectly()
        {
            // Arrange
            const string customBrotliEncodingName = "custom-brotli";
            const string customSnappyEncodingName = "custom-snappy";

            // Act
            var compressionTypeResolver = new CompressionTypeResolver([
                new KeyValuePair<string, CompressionType>(customBrotliEncodingName, CompressionType.Brotli)
            ]);

            // Assert
            Assert.True(compressionTypeResolver.TryResolve(customBrotliEncodingName, out _));
            Assert.True(compressionTypeResolver.TryResolve("CusTom-BROTLI", out _));
            Assert.False(compressionTypeResolver.TryResolve(customSnappyEncodingName, out _));
            Assert.False(compressionTypeResolver.TryResolve(CompressionDefaults.EncodingNames.Brotli, out _));
        }
    }
}