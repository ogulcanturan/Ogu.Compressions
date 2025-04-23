using Ogu.Compressions.Abstractions;
using System.IO.Compression;

namespace Ogu.Compressions.Tests
{
    public class CompressionFactoryTests
    {
        private readonly CompressionFactory _compressionFactory;
        public CompressionFactoryTests()
        {
            _compressionFactory = new CompressionFactory();
        }

        [Fact]
        public void Constructor_WhenCalled_InitializesCorrectly()
        {
            // Act
            var brotliCompression = _compressionFactory.Get(CompressionType.Brotli);
            var deflateCompression = _compressionFactory.Get(CompressionType.Deflate);
            var snappyCompression = _compressionFactory.Get(CompressionType.Snappy);
            var zstdCompression = _compressionFactory.Get(CompressionType.Zstd);
            var gzipCompression = _compressionFactory.Get(CompressionType.Gzip);
            var noneCompression = _compressionFactory.Get(CompressionType.None);

            // Assert
            Assert.NotNull(brotliCompression);
            Assert.NotNull(deflateCompression);
            Assert.NotNull(snappyCompression);
            Assert.NotNull(zstdCompression);
            Assert.NotNull(gzipCompression);
            Assert.NotNull(noneCompression);
            Assert.NotNull(_compressionFactory.Get(""));

            Assert.Equal(EncodingNames.Brotli, brotliCompression.EncodingName);
            Assert.Equal(CompressionLevel.Fastest, brotliCompression.Level);
            Assert.Equal(CompressionDefaults.BufferSize, brotliCompression.BufferSize);

            Assert.Equal(EncodingNames.Deflate, deflateCompression.EncodingName);
            Assert.Equal(CompressionLevel.Fastest, deflateCompression.Level);
            Assert.Equal(CompressionDefaults.BufferSize, deflateCompression.BufferSize);

            Assert.Equal(EncodingNames.Snappy, snappyCompression.EncodingName);
            Assert.Equal(CompressionLevel.Fastest, snappyCompression.Level);
            Assert.Equal(CompressionDefaults.BufferSize, snappyCompression.BufferSize);

            Assert.Equal(EncodingNames.Zstd, zstdCompression.EncodingName);
            Assert.Equal(CompressionLevel.Fastest, zstdCompression.Level);
            Assert.Equal(CompressionDefaults.BufferSize, zstdCompression.BufferSize);

            Assert.Equal(EncodingNames.Gzip, gzipCompression.EncodingName);
            Assert.Equal(CompressionLevel.Fastest, gzipCompression.Level);
            Assert.Equal(CompressionDefaults.BufferSize, gzipCompression.BufferSize);

            Assert.Equal(EncodingNames.None, noneCompression.EncodingName);
            Assert.Equal(CompressionLevel.Fastest, noneCompression.Level);
            Assert.Equal(0, noneCompression.BufferSize);
        }

        [Fact]
        public void Constructor_WhenCalled_WithCompressionOptions_InitializesCorrectly()
        {
            // Arrange
            var compressionOptions = new CompressionFactoryOptions
            {
                BufferSize = 4096,
                Level = CompressionLevel.Optimal
            };

            // Act
            var compressionFactory = new CompressionFactory(compressionOptions);

            var brotliCompression = compressionFactory.Get(CompressionType.Brotli);
            var deflateCompression = compressionFactory.Get(CompressionType.Deflate);
            var snappyCompression = compressionFactory.Get(CompressionType.Snappy);
            var zstdCompression = compressionFactory.Get(CompressionType.Zstd);
            var gzipCompression = compressionFactory.Get(CompressionType.Gzip);
            var noneCompression = compressionFactory.Get(CompressionType.None);

            // Assert
            Assert.NotNull(brotliCompression);
            Assert.NotNull(deflateCompression);
            Assert.NotNull(snappyCompression);
            Assert.NotNull(zstdCompression);
            Assert.NotNull(gzipCompression);
            Assert.NotNull(noneCompression);
            Assert.NotNull(_compressionFactory.Get(string.Empty));

            Assert.Equal(EncodingNames.Brotli, brotliCompression.EncodingName);
            Assert.Equal(compressionOptions.Level, brotliCompression.Level);
            Assert.Equal(compressionOptions.BufferSize, brotliCompression.BufferSize);

            Assert.Equal(EncodingNames.Deflate, deflateCompression.EncodingName);
            Assert.Equal(compressionOptions.Level, deflateCompression.Level);
            Assert.Equal(compressionOptions.BufferSize, deflateCompression.BufferSize);

            Assert.Equal(EncodingNames.Snappy, snappyCompression.EncodingName);
            Assert.Equal(compressionOptions.Level, snappyCompression.Level);
            Assert.Equal(compressionOptions.BufferSize, snappyCompression.BufferSize);

            Assert.Equal(EncodingNames.Zstd, zstdCompression.EncodingName);
            Assert.Equal(compressionOptions.Level, zstdCompression.Level);
            Assert.Equal(compressionOptions.BufferSize, zstdCompression.BufferSize);

            Assert.Equal(EncodingNames.Gzip, gzipCompression.EncodingName);
            Assert.Equal(compressionOptions.Level, gzipCompression.Level);
            Assert.Equal(compressionOptions.BufferSize, gzipCompression.BufferSize);

            Assert.Equal(EncodingNames.None, noneCompression.EncodingName);
            Assert.Equal(CompressionLevel.Fastest, noneCompression.Level);
            Assert.Equal(0, noneCompression.BufferSize);
        }

        [Fact]
        public void Get_CompressionTypeInput_ReturnsCorrectCompression()
        {
            // Act
            var brotliCompression = _compressionFactory.Get(CompressionType.Brotli);
            var deflateCompression = _compressionFactory.Get(CompressionType.Deflate);
            var snappyCompression = _compressionFactory.Get(CompressionType.Snappy);
            var zstdCompression = _compressionFactory.Get(CompressionType.Zstd);
            var gzipCompression = _compressionFactory.Get(CompressionType.Gzip);
            var noneCompression = _compressionFactory.Get(CompressionType.None);

            // Assert
            Assert.IsType<Compressions.BrotliCompression>(brotliCompression);
            Assert.IsType<Compressions.DeflateCompression>(deflateCompression);
            Assert.IsType<Compressions.SnappyCompression>(snappyCompression);
            Assert.IsType<Compressions.ZstdCompression>(zstdCompression);
            Assert.IsType<Compressions.GzipCompression>(gzipCompression);
            Assert.IsType<Compressions.NoneCompression>(noneCompression);
        }

        [Fact]
        public void Get_EncodingNameInput_ReturnsCorrectCompression()
        {
            var brotliCompression = _compressionFactory.Get(EncodingNames.Brotli);
            var deflateCompression = _compressionFactory.Get(EncodingNames.Deflate);
            var snappyCompression = _compressionFactory.Get(EncodingNames.Snappy);
            var zstdCompression = _compressionFactory.Get(EncodingNames.Zstd);
            var gzipCompression = _compressionFactory.Get(EncodingNames.Gzip);
            var noneCompression = _compressionFactory.Get(EncodingNames.None);

            // Assert
            Assert.IsType<Compressions.BrotliCompression>(brotliCompression);
            Assert.IsType<Compressions.DeflateCompression>(deflateCompression);
            Assert.IsType<Compressions.SnappyCompression>(snappyCompression);
            Assert.IsType<Compressions.ZstdCompression>(zstdCompression);
            Assert.IsType<Compressions.GzipCompression>(gzipCompression);
            Assert.IsType<Compressions.NoneCompression>(noneCompression);
        }
    }
}
