using Ogu.Compressions.Abstractions;
using System.IO.Compression;
using System.Net.Http.Headers;
using ZstdSharp;

namespace Ogu.Compressions.Tests.Abstractions
{
    public class CompressionExtensionsTests
    {
        [Fact]
        public void ToEncodingName_ReturnsCorrectEncodingName()
        {
            // Arrange & Act
            var brotliEncodingName = CompressionType.Brotli.ToEncodingName();
            var deflateEncodingName = CompressionType.Deflate.ToEncodingName();
            var snappyEncodingName = CompressionType.Snappy.ToEncodingName();
            var zstdEncodingName = CompressionType.Zstd.ToEncodingName();
            var gzipEncodingName = CompressionType.Gzip.ToEncodingName();
            var noneEncodingName = CompressionType.None.ToEncodingName();
            var someEncodingName = ((CompressionType)555).ToEncodingName();

            // Assert
            Assert.Equal(CompressionDefaults.EncodingNames.Brotli, brotliEncodingName);
            Assert.Equal(CompressionDefaults.EncodingNames.Deflate, deflateEncodingName);
            Assert.Equal(CompressionDefaults.EncodingNames.Snappy, snappyEncodingName);
            Assert.Equal(CompressionDefaults.EncodingNames.Zstd, zstdEncodingName);
            Assert.Equal(CompressionDefaults.EncodingNames.Gzip, gzipEncodingName);
            Assert.Equal(CompressionDefaults.EncodingNames.None, noneEncodingName);
            Assert.Null(someEncodingName);
        }

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
            var someResult = ((CompressionLevel)555).ToZstdLevel();
            var someResult2 = ((CompressionLevel)18).ToZstdLevel();

            // Assert
            Assert.Equal(Compressor.DefaultCompressionLevel, optimalResult);
#if NET6_0_OR_GREATER
            Assert.Equal(Compressor.MaxCompressionLevel, smallestSizeResult);
#endif
            Assert.Equal(Compressor.MinCompressionLevel, fastestResult);
            Assert.Equal(Compressor.MinCompressionLevel, noCompressionResult);
            Assert.Equal(Compressor.MaxCompressionLevel, someResult);
            Assert.Equal(18, someResult2);
        }

        [Fact]
        public void AddCompressionType_ShouldAddCompressionTypeToHeaders()
        {
            // Arrange
            var httpRequestMessage = new HttpRequestMessage();

            // Act
            httpRequestMessage.Headers.AddCompressionType(CompressionType.Brotli);
            httpRequestMessage.Headers.AddCompressionType(CompressionType.None);

            // Assert
            Assert.Contains(new StringWithQualityHeaderValue(CompressionDefaults.EncodingNames.Brotli), httpRequestMessage.Headers.AcceptEncoding);
            Assert.DoesNotContain(new StringWithQualityHeaderValue(CompressionDefaults.EncodingNames.None), httpRequestMessage.Headers.AcceptEncoding);
        }

        [Fact]
        public void AddCompressionType_WithQuality_ShouldAddCompressionTypeToHeaders()
        {
            // Arrange
            const double quality = 0.5;
            var httpRequestMessage = new HttpRequestMessage();

            // Act
            httpRequestMessage.Headers.AddCompressionType(CompressionType.Brotli, quality);
            httpRequestMessage.Headers.AddCompressionType(CompressionType.None);

            // Assert
            Assert.Contains(new StringWithQualityHeaderValue(CompressionDefaults.EncodingNames.Brotli, quality), httpRequestMessage.Headers.AcceptEncoding);
            Assert.DoesNotContain(new StringWithQualityHeaderValue(CompressionDefaults.EncodingNames.Brotli), httpRequestMessage.Headers.AcceptEncoding);
            Assert.DoesNotContain(new StringWithQualityHeaderValue(CompressionDefaults.EncodingNames.None), httpRequestMessage.Headers.AcceptEncoding);
        }

        [Fact]
        public void AddCompressionTypes_ShouldAddCompressionTypesToHeaders()
        {
            // Arrange
            CompressionType[] compressionTypes = [CompressionType.Brotli, CompressionType.Gzip, CompressionType.None];

            var httpRequestMessage = new HttpRequestMessage();

            // Act
            httpRequestMessage.Headers.AddCompressionTypes(compressionTypes);

            // Assert
            Assert.Contains(new StringWithQualityHeaderValue(CompressionDefaults.EncodingNames.Brotli), httpRequestMessage.Headers.AcceptEncoding);
            Assert.Contains(new StringWithQualityHeaderValue(CompressionDefaults.EncodingNames.Gzip), httpRequestMessage.Headers.AcceptEncoding);
            Assert.DoesNotContain(new StringWithQualityHeaderValue(CompressionDefaults.EncodingNames.None), httpRequestMessage.Headers.AcceptEncoding);
            Assert.DoesNotContain(new StringWithQualityHeaderValue(CompressionDefaults.EncodingNames.Deflate), httpRequestMessage.Headers.AcceptEncoding);
        }

        [Fact]
        public void AddCompressionTypes_WithCompressionTypeToQualityParam_ShouldAddCompressionTypesToHeaders()
        {
            // Arrange
            const double brotliQuality = 0.5;
            const double gzipQuality = 0.2;
            const double noneQuality = 0;

            var compressionTypeToQuality = new Dictionary<CompressionType, double>
            {
                { CompressionType.Brotli, brotliQuality },
                { CompressionType.Gzip, gzipQuality },
                { CompressionType.None, noneQuality }
            };

            var httpRequestMessage = new HttpRequestMessage();

            // Act
            httpRequestMessage.Headers.AddCompressionTypes(compressionTypeToQuality);

            // Assert
            Assert.Contains(new StringWithQualityHeaderValue(CompressionDefaults.EncodingNames.Brotli, brotliQuality), httpRequestMessage.Headers.AcceptEncoding);
            Assert.Contains(new StringWithQualityHeaderValue(CompressionDefaults.EncodingNames.Gzip, gzipQuality), httpRequestMessage.Headers.AcceptEncoding);
            Assert.DoesNotContain(new StringWithQualityHeaderValue(CompressionDefaults.EncodingNames.Brotli), httpRequestMessage.Headers.AcceptEncoding);
            Assert.DoesNotContain(new StringWithQualityHeaderValue(CompressionDefaults.EncodingNames.None), httpRequestMessage.Headers.AcceptEncoding);
            Assert.DoesNotContain(new StringWithQualityHeaderValue(CompressionDefaults.EncodingNames.Deflate), httpRequestMessage.Headers.AcceptEncoding);
        }

        [Fact]
        public void AddToRequestHeaders_ShouldAddCompressionTypeToHeaders()
        {
            // Arrange
            var httpRequestMessage = new HttpRequestMessage();

            // Act
            CompressionType.Brotli.AddToRequestHeaders(httpRequestMessage.Headers);
            CompressionType.None.AddToRequestHeaders(httpRequestMessage.Headers);

            // Assert
            Assert.Contains(new StringWithQualityHeaderValue(CompressionDefaults.EncodingNames.Brotli), httpRequestMessage.Headers.AcceptEncoding);
            Assert.DoesNotContain(new StringWithQualityHeaderValue(CompressionDefaults.EncodingNames.None), httpRequestMessage.Headers.AcceptEncoding);
        }

        [Fact]
        public void AddToRequestHeaders_WithQuality_ShouldAddCompressionTypeToHeaders()
        {
            // Arrange
            const double quality = 0.5;
            var httpRequestMessage = new HttpRequestMessage();

            // Act
            CompressionType.Brotli.AddToRequestHeaders(httpRequestMessage.Headers, quality);
            CompressionType.None.AddToRequestHeaders(httpRequestMessage.Headers);


            // Assert
            Assert.Contains(new StringWithQualityHeaderValue(CompressionDefaults.EncodingNames.Brotli, quality), httpRequestMessage.Headers.AcceptEncoding);
            Assert.DoesNotContain(new StringWithQualityHeaderValue(CompressionDefaults.EncodingNames.Brotli), httpRequestMessage.Headers.AcceptEncoding);
            Assert.DoesNotContain(new StringWithQualityHeaderValue(CompressionDefaults.EncodingNames.None), httpRequestMessage.Headers.AcceptEncoding);
        }

        [Fact]
        public void AddToRequestHeaders_ShouldAddCompressionTypesToHeaders()
        {
            // Arrange
            CompressionType[] compressionTypes = [CompressionType.Brotli, CompressionType.Gzip, CompressionType.None];

            var httpRequestMessage = new HttpRequestMessage();

            // Act
            compressionTypes.AddToRequestHeaders(httpRequestMessage.Headers);

            // Assert
            Assert.Contains(new StringWithQualityHeaderValue(CompressionDefaults.EncodingNames.Brotli), httpRequestMessage.Headers.AcceptEncoding);
            Assert.Contains(new StringWithQualityHeaderValue(CompressionDefaults.EncodingNames.Gzip), httpRequestMessage.Headers.AcceptEncoding);
            Assert.DoesNotContain(new StringWithQualityHeaderValue(CompressionDefaults.EncodingNames.None), httpRequestMessage.Headers.AcceptEncoding);
            Assert.DoesNotContain(new StringWithQualityHeaderValue(CompressionDefaults.EncodingNames.Deflate, 0.9), httpRequestMessage.Headers.AcceptEncoding);
        }

        [Fact]
        public void AddToRequestHeaders_WithCompressionTypeToQualityParam_ShouldAddCompressionTypesToHeaders()
        {
            // Arrange
            const double brotliQuality = 0.5;
            const double gzipQuality = 0.2;
            const double noneQuality = 0;

            var compressionTypeToQuality = new Dictionary<CompressionType, double>
            {
                { CompressionType.Brotli, brotliQuality },
                { CompressionType.Gzip, gzipQuality },
                { CompressionType.None, noneQuality }
            };

            var httpRequestMessage = new HttpRequestMessage();

            // Act
            compressionTypeToQuality.AddToRequestHeaders(httpRequestMessage.Headers);

            // Assert
            Assert.Contains(new StringWithQualityHeaderValue(CompressionDefaults.EncodingNames.Brotli, brotliQuality), httpRequestMessage.Headers.AcceptEncoding);
            Assert.Contains(new StringWithQualityHeaderValue(CompressionDefaults.EncodingNames.Gzip, gzipQuality), httpRequestMessage.Headers.AcceptEncoding);
            Assert.DoesNotContain(new StringWithQualityHeaderValue(CompressionDefaults.EncodingNames.Brotli), httpRequestMessage.Headers.AcceptEncoding);
            Assert.DoesNotContain(new StringWithQualityHeaderValue(CompressionDefaults.EncodingNames.None), httpRequestMessage.Headers.AcceptEncoding);
            Assert.DoesNotContain(new StringWithQualityHeaderValue(CompressionDefaults.EncodingNames.Deflate), httpRequestMessage.Headers.AcceptEncoding);
        }
    }
}