using System.IO.Compression;
using ZstdSharp;

namespace Ogu.Compressions.Tests
{
    public class MappingExtensionsTests
    {
        [Fact]
        public void ToZstd_ReturnsCorrectCompressionLevel()
        {
            // Arrange & Act
            var optimalResult = CompressionLevel.Optimal.ToZstd();
#if NET6_0_OR_GREATER
            var smallestSizeResult = CompressionLevel.SmallestSize.ToZstd();
#endif
            var fastestResult = CompressionLevel.Fastest.ToZstd();
            var noCompressionResult = CompressionLevel.NoCompression.ToZstd();

            // Assert
            Assert.Equal(Compressor.MaxCompressionLevel, optimalResult);
#if NET6_0_OR_GREATER
            Assert.Equal(Compressor.MaxCompressionLevel, smallestSizeResult);
#endif
            Assert.Equal(Compressor.MinCompressionLevel, fastestResult);
            Assert.Equal(Compressor.DefaultCompressionLevel, noCompressionResult);
        }
    }
}