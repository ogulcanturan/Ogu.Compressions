using Ogu.Compressions.Abstractions;
using System.IO.Compression;
using ZstdSharp;

namespace Ogu.Compressions.Tests
{
    public class ExtensionsTests
    {
        [Fact]
        public void ToZstdLevel_ReturnsCorrectCompressionLevel()
        {
            // Arrange & Act
            var optimalResult = CompressionLevel.Optimal.ToZstdLevel();
#if NET6_0_OR_GREATER
            var smallestSizeResult = CompressionLevel.SmallestSize.ToZstdLevel();
#endif
            var fastestResult = CompressionLevel.Fastest.ToZstdLevel();
            var noCompressionResult = CompressionLevel.NoCompression.ToZstdLevel();

            // Assert
            Assert.Equal(Compressor.DefaultCompressionLevel, optimalResult);
#if NET6_0_OR_GREATER
            Assert.Equal(Compressor.MaxCompressionLevel, smallestSizeResult);
#endif
            Assert.Equal(Compressor.MinCompressionLevel, fastestResult);
            Assert.Equal(Compressor.MinCompressionLevel, noCompressionResult);
        }
    }
}