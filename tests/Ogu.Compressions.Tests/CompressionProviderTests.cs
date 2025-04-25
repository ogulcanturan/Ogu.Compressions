using Microsoft.Extensions.DependencyInjection;
using Moq;
using Ogu.Compressions.Abstractions;
using System.IO.Compression;

namespace Ogu.Compressions.Tests
{
    public class CompressionProviderTests
    {
        [Fact]
        public void Constructor_WhenCalled_InitializesCorrectly()
        {
            // Act
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddCompressions();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var compressionProvider = serviceProvider.GetRequiredService<ICompressionProvider>();

            var brotliCompression = compressionProvider.GetCompression(CompressionType.Brotli);
            var deflateCompression = compressionProvider.GetCompression(CompressionType.Deflate);
            var snappyCompression = compressionProvider.GetCompression(CompressionType.Snappy);
            var zstdCompression = compressionProvider.GetCompression(CompressionType.Zstd);
            var gzipCompression = compressionProvider.GetCompression(CompressionType.Gzip);
            var noneCompression = compressionProvider.GetCompression(CompressionType.None);

            // Assert
            Assert.NotNull(brotliCompression);
            Assert.NotNull(deflateCompression);
            Assert.NotNull(snappyCompression);
            Assert.NotNull(zstdCompression);
            Assert.NotNull(gzipCompression);
            Assert.NotNull(noneCompression);
            Assert.Null(compressionProvider.GetCompression(string.Empty));

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
            var serviceCollection = new ServiceCollection();
            const int bufferSize = 4096;
            const CompressionLevel level = CompressionLevel.Optimal;

            Action<CompressionOptions> compressionOptions = opts =>
            {
                opts.BufferSize = bufferSize;
                opts.Level = level;
            };

            serviceCollection.AddCompressions(compressionOptions);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            // Act
            var compressionProvider = serviceProvider.GetRequiredService<ICompressionProvider>();

            var brotliCompression = compressionProvider.GetCompression(CompressionType.Brotli);
            var deflateCompression = compressionProvider.GetCompression(CompressionType.Deflate);
            var snappyCompression = compressionProvider.GetCompression(CompressionType.Snappy);
            var zstdCompression = compressionProvider.GetCompression(CompressionType.Zstd);
            var gzipCompression = compressionProvider.GetCompression(CompressionType.Gzip);
            var noneCompression = compressionProvider.GetCompression(CompressionType.None);
            var emptyCompression = compressionProvider.GetCompression(string.Empty);

            // Assert
            Assert.NotNull(brotliCompression);
            Assert.NotNull(deflateCompression);
            Assert.NotNull(snappyCompression);
            Assert.NotNull(zstdCompression);
            Assert.NotNull(gzipCompression);
            Assert.NotNull(noneCompression);
            Assert.Null(emptyCompression);
            Assert.NotNull(compressionProvider.Compressions);
            Assert.Equal(6, compressionProvider.Compressions.Count());

            Assert.Equal(EncodingNames.Brotli, brotliCompression.EncodingName);
            Assert.Equal(level, brotliCompression.Level);
            Assert.Equal(bufferSize, brotliCompression.BufferSize);

            Assert.Equal(EncodingNames.Deflate, deflateCompression.EncodingName);
            Assert.Equal(level, deflateCompression.Level);
            Assert.Equal(bufferSize, deflateCompression.BufferSize);

            Assert.Equal(EncodingNames.Snappy, snappyCompression.EncodingName);
            Assert.Equal(level, snappyCompression.Level);
            Assert.Equal(bufferSize, snappyCompression.BufferSize);

            Assert.Equal(EncodingNames.Zstd, zstdCompression.EncodingName);
            Assert.Equal(level, zstdCompression.Level);
            Assert.Equal(bufferSize, zstdCompression.BufferSize);

            Assert.Equal(EncodingNames.Gzip, gzipCompression.EncodingName);
            Assert.Equal(level, gzipCompression.Level);
            Assert.Equal(bufferSize, gzipCompression.BufferSize);

            Assert.Equal(EncodingNames.None, noneCompression.EncodingName);
            Assert.Equal(CompressionLevel.Fastest, noneCompression.Level);
            Assert.Equal(0, noneCompression.BufferSize);
        }

        [Fact]
        public void Constructor_WhenCalled_WithEmptyCompressions_InitializedCorrectly()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddCompressionProvider();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // Act
            var compressionProvider = serviceProvider.GetRequiredService<ICompressionProvider>();

            // Assert
            Assert.Empty(compressionProvider.Compressions);
        }

        [Fact]
        public void GetCompression_CompressionTypeInput_ReturnsCorrectCompression()
        {
            // Act
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddCompressions();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var compressionProvider = serviceProvider.GetRequiredService<ICompressionProvider>();


            var brotliCompression = compressionProvider.GetCompression(CompressionType.Brotli);
            var deflateCompression = compressionProvider.GetCompression(CompressionType.Deflate);
            var snappyCompression = compressionProvider.GetCompression(CompressionType.Snappy);
            var zstdCompression = compressionProvider.GetCompression(CompressionType.Zstd);
            var gzipCompression = compressionProvider.GetCompression(CompressionType.Gzip);
            var noneCompression = compressionProvider.GetCompression(CompressionType.None);
            var emptyCompression = compressionProvider.GetCompression(string.Empty);

            // Assert
            Assert.IsType<Compressions.BrotliCompression>(brotliCompression);
            Assert.IsType<Compressions.DeflateCompression>(deflateCompression);
            Assert.IsType<Compressions.SnappyCompression>(snappyCompression);
            Assert.IsType<Compressions.ZstdCompression>(zstdCompression);
            Assert.IsType<Compressions.GzipCompression>(gzipCompression);
            Assert.IsType<Compressions.NoneCompression>(noneCompression);
            Assert.Null(emptyCompression);
        }

        [Fact]
        public void GetCompression_EncodingNameInput_ReturnsCorrectCompression()
        {
            // Act
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddCompressions();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var compressionProvider = serviceProvider.GetRequiredService<ICompressionProvider>();

            var brotliCompression = compressionProvider.GetCompression(EncodingNames.Brotli);
            var deflateCompression = compressionProvider.GetCompression(EncodingNames.Deflate);
            var snappyCompression = compressionProvider.GetCompression(EncodingNames.Snappy);
            var zstdCompression = compressionProvider.GetCompression(EncodingNames.Zstd);
            var gzipCompression = compressionProvider.GetCompression(EncodingNames.Gzip);
            var noneCompression = compressionProvider.GetCompression(EncodingNames.None);
            var emptyCompression = compressionProvider.GetCompression(string.Empty);

            // Assert
            Assert.IsType<Compressions.BrotliCompression>(brotliCompression);
            Assert.IsType<Compressions.DeflateCompression>(deflateCompression);
            Assert.IsType<Compressions.SnappyCompression>(snappyCompression);
            Assert.IsType<Compressions.ZstdCompression>(zstdCompression);
            Assert.IsType<Compressions.GzipCompression>(gzipCompression);
            Assert.IsType<Compressions.NoneCompression>(noneCompression);
            Assert.Null(emptyCompression);
        }

        [Fact]
        public void GetRequiredCompression_CompressionTypeInput_ReturnsCorrectCompression()
        {
            // Act
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddCompressions();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var compressionProvider = serviceProvider.GetRequiredService<ICompressionProvider>();

            var brotliCompression = compressionProvider.GetRequiredCompression(CompressionType.Brotli);
            var deflateCompression = compressionProvider.GetRequiredCompression(CompressionType.Deflate);
            var snappyCompression = compressionProvider.GetRequiredCompression(CompressionType.Snappy);
            var zstdCompression = compressionProvider.GetRequiredCompression(CompressionType.Zstd);
            var gzipCompression = compressionProvider.GetRequiredCompression(CompressionType.Gzip);
            var noneCompression = compressionProvider.GetRequiredCompression(CompressionType.None);
            var emptyCompression = () => compressionProvider.GetRequiredCompression(string.Empty);

            Assert.IsType<Compressions.BrotliCompression>(brotliCompression);
            Assert.IsType<Compressions.DeflateCompression>(deflateCompression);
            Assert.IsType<Compressions.SnappyCompression>(snappyCompression);
            Assert.IsType<Compressions.ZstdCompression>(zstdCompression);
            Assert.IsType<Compressions.GzipCompression>(gzipCompression);
            Assert.IsType<Compressions.NoneCompression>(noneCompression);
            Assert.Throws<CompressionNotAvailableException>(emptyCompression);
        }

        [Fact]
        public void GetRequiredCompression_EncodingNameInput_ReturnsCorrectCompression()
        {
            // Act
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddCompressions();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var compressionProvider = serviceProvider.GetRequiredService<ICompressionProvider>();

            var brotliCompression = compressionProvider.GetRequiredCompression(EncodingNames.Brotli);
            var deflateCompression = compressionProvider.GetRequiredCompression(EncodingNames.Deflate);
            var snappyCompression = compressionProvider.GetRequiredCompression(EncodingNames.Snappy);
            var zstdCompression = compressionProvider.GetRequiredCompression(EncodingNames.Zstd);
            var gzipCompression = compressionProvider.GetRequiredCompression(EncodingNames.Gzip);
            var noneCompression = compressionProvider.GetRequiredCompression(EncodingNames.None);
            var emptyCompression = () => compressionProvider.GetRequiredCompression(string.Empty);

            // Assert
            Assert.IsType<Compressions.BrotliCompression>(brotliCompression);
            Assert.IsType<Compressions.DeflateCompression>(deflateCompression);
            Assert.IsType<Compressions.SnappyCompression>(snappyCompression);
            Assert.IsType<Compressions.ZstdCompression>(zstdCompression);
            Assert.IsType<Compressions.GzipCompression>(gzipCompression);
            Assert.IsType<Compressions.NoneCompression>(noneCompression);
            Assert.Throws<CompressionNotAvailableException>(emptyCompression);
        }

        [Fact]
        public async Task CompressAsync_StringInput_ShouldCallAppropriateMethod()
        {
            // Arrange
            const string input = "Hello, World!";
            const string encodingName = EncodingNames.Brotli;
            const CompressionType type = CompressionType.Brotli;
            var cancellationToken = CancellationToken.None;

            var compressionMock = new Mock<ICompression>();

            compressionMock.SetupGet(x => x.EncodingName).Returns(encodingName);
            compressionMock.SetupGet(x => x.Type).Returns(CompressionType.Brotli);

            compressionMock.Setup(c => c.CompressAsync(input, cancellationToken)).ReturnsAsync([]);

            var compressionProvider = new CompressionProvider([compressionMock.Object]);

            // Act
            await compressionProvider.CompressAsync(type, input, cancellationToken);
            await compressionProvider.CompressAsync(encodingName, input, cancellationToken);

            // Assert
            compressionMock.Verify(c => c.CompressAsync(input, cancellationToken), Times.Exactly(2));
        }

        [Fact]
        public async Task CompressAsync_BytesInput_ShouldCallAppropriateMethod()
        {
            // Arrange
            byte[] input = [1];
            const string encodingName = EncodingNames.Brotli;
            const CompressionType type = CompressionType.Brotli;
            var cancellationToken = CancellationToken.None;

            var compressionMock = new Mock<ICompression>();

            compressionMock.SetupGet(x => x.EncodingName).Returns(encodingName);
            compressionMock.SetupGet(x => x.Type).Returns(CompressionType.Brotli);

            compressionMock.Setup(c => c.CompressAsync(input, cancellationToken)).ReturnsAsync([]);

            var compressionProvider = new CompressionProvider([compressionMock.Object]);

            // Act
            await compressionProvider.CompressAsync(type, input, cancellationToken);
            await compressionProvider.CompressAsync(encodingName, input, cancellationToken);

            // Assert
            compressionMock.Verify(c => c.CompressAsync(input, cancellationToken), Times.Exactly(2));
        }

        [Fact]
        public async Task CompressAsync_StreamInput_ShouldCallAppropriateMethod()
        {
            // Arrange
            var input = new MemoryStream([1]);
            const string encodingName = EncodingNames.Brotli;
            const CompressionType type = CompressionType.Brotli;
            var cancellationToken = CancellationToken.None;

            var compressionMock = new Mock<ICompression>();

            compressionMock.SetupGet(x => x.EncodingName).Returns(encodingName);
            compressionMock.SetupGet(x => x.Type).Returns(CompressionType.Brotli);

            compressionMock.Setup(c => c.CompressAsync(input, cancellationToken)).ReturnsAsync([]);

            var compressionProvider = new CompressionProvider([compressionMock.Object]);

            // Act
            await compressionProvider.CompressAsync(type, input, cancellationToken);
            await compressionProvider.CompressAsync(encodingName, input, cancellationToken);

            // Assert
            compressionMock.Verify(c => c.CompressAsync(input, cancellationToken), Times.Exactly(2));

            await input.DisposeAsync();
        }

        [Fact]
        public async Task CompressAsync_StreamInput_WithLeaveOpenTrue_ShouldCallAppropriateMethod()
        {
            // Arrange
            var input = new MemoryStream([1]);
            const string encodingName = EncodingNames.Brotli;
            const CompressionType type = CompressionType.Brotli;
            const bool leaveOpen = true;
            var cancellationToken = CancellationToken.None;

            var compressionMock = new Mock<ICompression>();

            compressionMock.SetupGet(x => x.EncodingName).Returns(encodingName);
            compressionMock.SetupGet(x => x.Type).Returns(CompressionType.Brotli);

            compressionMock.Setup(c => c.CompressAsync(input, leaveOpen, cancellationToken)).ReturnsAsync([]);

            var compressionProvider = new CompressionProvider([compressionMock.Object]);

            // Act
            await compressionProvider.CompressAsync(type, input, leaveOpen, cancellationToken);
            await compressionProvider.CompressAsync(encodingName, input, leaveOpen, cancellationToken);

            // Assert
            compressionMock.Verify(c => c.CompressAsync(input, leaveOpen, cancellationToken), Times.Exactly(2));

            await input.DisposeAsync();
        }

        [Fact]
        public async Task CompressAsync_StringInput_WithLevel_ShouldCallAppropriateMethod()
        {
            // Arrange
            const string input = "Hello, World!";
            const string encodingName = EncodingNames.Brotli;
            const CompressionType type = CompressionType.Brotli;
            const CompressionLevel level = CompressionLevel.Optimal;
            var cancellationToken = CancellationToken.None;

            var compressionMock = new Mock<ICompression>();

            compressionMock.SetupGet(x => x.EncodingName).Returns(encodingName);
            compressionMock.SetupGet(x => x.Type).Returns(CompressionType.Brotli);

            compressionMock.Setup(c => c.CompressAsync(input, level, cancellationToken)).ReturnsAsync([]);

            var compressionProvider = new CompressionProvider([compressionMock.Object]);

            // Act
            await compressionProvider.CompressAsync(type, input, level, cancellationToken);
            await compressionProvider.CompressAsync(encodingName, input, level, cancellationToken);

            // Assert
            compressionMock.Verify(c => c.CompressAsync(input, level, cancellationToken), Times.Exactly(2));
        }

        [Fact]
        public async Task CompressAsync_BytesInput_WithLevel_ShouldCallAppropriateMethod()
        {
            // Arrange
            byte[] input = [1];
            const string encodingName = EncodingNames.Brotli;
            const CompressionType type = CompressionType.Brotli;
            const CompressionLevel level = CompressionLevel.Optimal;
            var cancellationToken = CancellationToken.None;

            var compressionMock = new Mock<ICompression>();

            compressionMock.SetupGet(x => x.EncodingName).Returns(encodingName);
            compressionMock.SetupGet(x => x.Type).Returns(CompressionType.Brotli);

            compressionMock.Setup(c => c.CompressAsync(input, level, cancellationToken)).ReturnsAsync([]);

            var compressionProvider = new CompressionProvider([compressionMock.Object]);

            // Act
            await compressionProvider.CompressAsync(type, input, level, cancellationToken);
            await compressionProvider.CompressAsync(encodingName, input, level, cancellationToken);

            // Assert
            compressionMock.Verify(c => c.CompressAsync(input, level, cancellationToken), Times.Exactly(2));
        }

        [Fact]
        public async Task CompressAsync_StreamInput_WithLevel_ShouldCallAppropriateMethod()
        {
            // Arrange
            var input = new MemoryStream([1]);
            const string encodingName = EncodingNames.Brotli;
            const CompressionType type = CompressionType.Brotli;
            const CompressionLevel level = CompressionLevel.Optimal;
            var cancellationToken = CancellationToken.None;

            var compressionMock = new Mock<ICompression>();

            compressionMock.SetupGet(x => x.EncodingName).Returns(encodingName);
            compressionMock.SetupGet(x => x.Type).Returns(CompressionType.Brotli);

            compressionMock.Setup(c => c.CompressAsync(input, level, cancellationToken)).ReturnsAsync([]);

            var compressionProvider = new CompressionProvider([compressionMock.Object]);

            // Act
            await compressionProvider.CompressAsync(type, input, level, cancellationToken);
            await compressionProvider.CompressAsync(encodingName, input, level, cancellationToken);

            // Assert
            compressionMock.Verify(c => c.CompressAsync(input, level, cancellationToken), Times.Exactly(2));

            await input.DisposeAsync();
        }

        [Fact]
        public async Task CompressAsync_StreamInput_WithLevelAndLeaveOpenTrue_ShouldCallAppropriateMethod()
        {
            // Arrange
            var input = new MemoryStream([1]);
            const string encodingName = EncodingNames.Brotli;
            const CompressionType type = CompressionType.Brotli;
            const CompressionLevel level = CompressionLevel.Optimal;
            const bool leaveOpen = true;
            var cancellationToken = CancellationToken.None;

            var compressionMock = new Mock<ICompression>();

            compressionMock.SetupGet(x => x.EncodingName).Returns(encodingName);
            compressionMock.SetupGet(x => x.Type).Returns(CompressionType.Brotli);

            compressionMock.Setup(c => c.CompressAsync(input, level, leaveOpen, cancellationToken)).ReturnsAsync([]);

            var compressionProvider = new CompressionProvider([compressionMock.Object]);

            // Act
            await compressionProvider.CompressAsync(type, input, level, leaveOpen, cancellationToken);
            await compressionProvider.CompressAsync(encodingName, input, level, leaveOpen, cancellationToken);

            // Assert
            compressionMock.Verify(c => c.CompressAsync(input, level, leaveOpen, cancellationToken), Times.Exactly(2));

            await input.DisposeAsync();
        }

        [Fact]
        public async Task CompressToStreamAsync_StringInput_ShouldCallAppropriateMethod()
        {
            // Arrange
            const string input = "Hello, World!";
            const string encodingName = EncodingNames.Brotli;
            const CompressionType type = CompressionType.Brotli;
            var cancellationToken = CancellationToken.None;
            var stream = new MemoryStream();

            var compressionMock = new Mock<ICompression>();

            compressionMock.SetupGet(x => x.EncodingName).Returns(encodingName);
            compressionMock.SetupGet(x => x.Type).Returns(CompressionType.Brotli);

            compressionMock.Setup(c => c.CompressToStreamAsync(input, cancellationToken)).ReturnsAsync(stream);

            var compressionProvider = new CompressionProvider([compressionMock.Object]);

            // Act
            await compressionProvider.CompressToStreamAsync(type, input, cancellationToken);
            await compressionProvider.CompressToStreamAsync(encodingName, input, cancellationToken);

            // Assert
            compressionMock.Verify(c => c.CompressToStreamAsync(input, cancellationToken), Times.Exactly(2));

            await stream.DisposeAsync();
        }

        [Fact]
        public async Task CompressToStreamAsync_BytesInput_ShouldCallAppropriateMethod()
        {
            // Arrange
            byte[] input = [1];
            const string encodingName = EncodingNames.Brotli;
            const CompressionType type = CompressionType.Brotli;
            var cancellationToken = CancellationToken.None;
            var stream = new MemoryStream();

            var compressionMock = new Mock<ICompression>();

            compressionMock.SetupGet(x => x.EncodingName).Returns(encodingName);
            compressionMock.SetupGet(x => x.Type).Returns(CompressionType.Brotli);

            compressionMock.Setup(c => c.CompressToStreamAsync(input, cancellationToken)).ReturnsAsync(stream);

            var compressionProvider = new CompressionProvider([compressionMock.Object]);

            // Act
            await compressionProvider.CompressToStreamAsync(type, input, cancellationToken);
            await compressionProvider.CompressToStreamAsync(encodingName, input, cancellationToken);

            // Assert
            compressionMock.Verify(c => c.CompressToStreamAsync(input, cancellationToken), Times.Exactly(2));

            await stream.DisposeAsync();
        }

        [Fact]
        public async Task CompressToStreamAsync_StreamInput_ShouldCallAppropriateMethod()
        {
            // Arrange
            var input = new MemoryStream([1]);
            const string encodingName = EncodingNames.Brotli;
            const CompressionType type = CompressionType.Brotli;
            var cancellationToken = CancellationToken.None;
            var stream = new MemoryStream();

            var compressionMock = new Mock<ICompression>();

            compressionMock.SetupGet(x => x.EncodingName).Returns(encodingName);
            compressionMock.SetupGet(x => x.Type).Returns(CompressionType.Brotli);

            compressionMock.Setup(c => c.CompressToStreamAsync(input, cancellationToken)).ReturnsAsync(stream);

            var compressionProvider = new CompressionProvider([compressionMock.Object]);

            // Act
            await compressionProvider.CompressToStreamAsync(type, input, cancellationToken);
            await compressionProvider.CompressToStreamAsync(encodingName, input, cancellationToken);

            // Assert
            compressionMock.Verify(c => c.CompressToStreamAsync(input, cancellationToken), Times.Exactly(2));

            await Task.WhenAll(input.DisposeAsync().AsTask(), stream.DisposeAsync().AsTask());
        }

        [Fact]
        public async Task CompressToStreamAsync_StreamInput_WithLeaveOpenTrue_ShouldCallAppropriateMethod()
        {
            // Arrange
            var input = new MemoryStream([1]);
            const string encodingName = EncodingNames.Brotli;
            const CompressionType type = CompressionType.Brotli;
            const bool leaveOpen = true;
            var cancellationToken = CancellationToken.None;
            var stream = new MemoryStream();

            var compressionMock = new Mock<ICompression>();

            compressionMock.SetupGet(x => x.EncodingName).Returns(encodingName);
            compressionMock.SetupGet(x => x.Type).Returns(CompressionType.Brotli);

            compressionMock.Setup(c => c.CompressToStreamAsync(input, leaveOpen, cancellationToken)).ReturnsAsync(stream);

            var compressionProvider = new CompressionProvider([compressionMock.Object]);

            // Act
            await compressionProvider.CompressToStreamAsync(type, input, leaveOpen, cancellationToken);
            await compressionProvider.CompressToStreamAsync(encodingName, input, leaveOpen, cancellationToken);

            // Assert
            compressionMock.Verify(c => c.CompressToStreamAsync(input, leaveOpen, cancellationToken), Times.Exactly(2));

            await Task.WhenAll(input.DisposeAsync().AsTask(), stream.DisposeAsync().AsTask());
        }

        [Fact]
        public async Task CompressToStreamAsync_StringInput_WithLevel_ShouldCallAppropriateMethod()
        {
            // Arrange
            const string input = "Hello, World!";
            const string encodingName = EncodingNames.Brotli;
            const CompressionType type = CompressionType.Brotli;
            const CompressionLevel level = CompressionLevel.Optimal;

            var cancellationToken = CancellationToken.None;
            var stream = new MemoryStream();

            var compressionMock = new Mock<ICompression>();

            compressionMock.SetupGet(x => x.EncodingName).Returns(encodingName);
            compressionMock.SetupGet(x => x.Type).Returns(CompressionType.Brotli);

            compressionMock.Setup(c => c.CompressToStreamAsync(input, level, cancellationToken)).ReturnsAsync(stream);

            var compressionProvider = new CompressionProvider([compressionMock.Object]);

            // Act
            await compressionProvider.CompressToStreamAsync(type, input, level, cancellationToken);
            await compressionProvider.CompressToStreamAsync(encodingName, input, level, cancellationToken);

            // Assert
            compressionMock.Verify(c => c.CompressToStreamAsync(input, level,cancellationToken), Times.Exactly(2));

            await stream.DisposeAsync();
        }

        [Fact]
        public async Task CompressToStreamAsync_BytesInput_WithLevel_ShouldCallAppropriateMethod()
        {
            // Arrange
            byte[] input = [1];
            const string encodingName = EncodingNames.Brotli;
            const CompressionType type = CompressionType.Brotli;
            const CompressionLevel level = CompressionLevel.Optimal;

            var cancellationToken = CancellationToken.None;
            var stream = new MemoryStream();

            var compressionMock = new Mock<ICompression>();

            compressionMock.SetupGet(x => x.EncodingName).Returns(encodingName);
            compressionMock.SetupGet(x => x.Type).Returns(CompressionType.Brotli);

            compressionMock.Setup(c => c.CompressToStreamAsync(input, level, cancellationToken)).ReturnsAsync(stream);

            var compressionProvider = new CompressionProvider([compressionMock.Object]);

            // Act
            await compressionProvider.CompressToStreamAsync(type, input, level, cancellationToken);
            await compressionProvider.CompressToStreamAsync(encodingName, input, level, cancellationToken);

            // Assert
            compressionMock.Verify(c => c.CompressToStreamAsync(input, level, cancellationToken), Times.Exactly(2));

            await stream.DisposeAsync();
        }

        [Fact]
        public async Task CompressToStreamAsync_StreamInput_WithLevel_ShouldCallAppropriateMethod()
        {
            // Arrange
            var input = new MemoryStream([1]);
            const string encodingName = EncodingNames.Brotli;
            const CompressionType type = CompressionType.Brotli;
            const CompressionLevel level = CompressionLevel.Optimal;

            var cancellationToken = CancellationToken.None;
            var stream = new MemoryStream();

            var compressionMock = new Mock<ICompression>();

            compressionMock.SetupGet(x => x.EncodingName).Returns(encodingName);
            compressionMock.SetupGet(x => x.Type).Returns(CompressionType.Brotli);

            compressionMock.Setup(c => c.CompressToStreamAsync(input, level, cancellationToken)).ReturnsAsync(stream);

            var compressionProvider = new CompressionProvider([compressionMock.Object]);

            // Act
            await compressionProvider.CompressToStreamAsync(type, input, level, cancellationToken);
            await compressionProvider.CompressToStreamAsync(encodingName, input, level, cancellationToken);

            // Assert
            compressionMock.Verify(c => c.CompressToStreamAsync(input, level, cancellationToken), Times.Exactly(2));

            await Task.WhenAll(input.DisposeAsync().AsTask(), stream.DisposeAsync().AsTask());
        }

        [Fact]
        public async Task CompressToStreamAsync_StreamInput_WithLevelAndLeaveOpenTrue_ShouldCallAppropriateMethod()
        {
            // Arrange
            var input = new MemoryStream([1]);
            const string encodingName = EncodingNames.Brotli;
            const CompressionType type = CompressionType.Brotli;
            const CompressionLevel level = CompressionLevel.Optimal;
            const bool leaveOpen = true;

            var cancellationToken = CancellationToken.None;
            var stream = new MemoryStream();

            var compressionMock = new Mock<ICompression>();

            compressionMock.SetupGet(x => x.EncodingName).Returns(encodingName);
            compressionMock.SetupGet(x => x.Type).Returns(CompressionType.Brotli);

            compressionMock.Setup(c => c.CompressToStreamAsync(input, level, leaveOpen, cancellationToken)).ReturnsAsync(stream);

            var compressionProvider = new CompressionProvider([compressionMock.Object]);

            // Act
            await compressionProvider.CompressToStreamAsync(type, input, level, leaveOpen, cancellationToken);
            await compressionProvider.CompressToStreamAsync(encodingName, input, level, leaveOpen, cancellationToken);

            // Assert
            compressionMock.Verify(c => c.CompressToStreamAsync(input, level, leaveOpen, cancellationToken), Times.Exactly(2));

            await Task.WhenAll(input.DisposeAsync().AsTask(), stream.DisposeAsync().AsTask());
        }

        [Fact]
        public void Compress_StringInput_ShouldCallAppropriateMethod()
        {
            // Arrange
            const string input = "Hello, World!";
            const string encodingName = EncodingNames.Brotli;
            const CompressionType type = CompressionType.Brotli;

            var compressionMock = new Mock<ICompression>();

            compressionMock.SetupGet(x => x.EncodingName).Returns(encodingName);
            compressionMock.SetupGet(x => x.Type).Returns(CompressionType.Brotli);

            compressionMock.Setup(c => c.Compress(input)).Returns([]);

            var compressionProvider = new CompressionProvider([compressionMock.Object]);

            // Act
            compressionProvider.Compress(type, input);
            compressionProvider.Compress(encodingName, input);

            // Assert
            compressionMock.Verify(c => c.Compress(input), Times.Exactly(2));
        }

        [Fact]
        public void Compress_BytesInput_ShouldCallAppropriateMethod()
        {
            // Arrange
            byte[] input = [1];
            const string encodingName = EncodingNames.Brotli;
            const CompressionType type = CompressionType.Brotli;

            var compressionMock = new Mock<ICompression>();

            compressionMock.SetupGet(x => x.EncodingName).Returns(encodingName);
            compressionMock.SetupGet(x => x.Type).Returns(CompressionType.Brotli);

            compressionMock.Setup(c => c.Compress(input)).Returns([]);

            var compressionProvider = new CompressionProvider([compressionMock.Object]);

            // Act
            compressionProvider.Compress(type, input);
            compressionProvider.Compress(encodingName, input);

            // Assert
            compressionMock.Verify(c => c.Compress(input), Times.Exactly(2));
        }

        [Fact]
        public void Compress_StreamInput_ShouldCallAppropriateMethod()
        {
            // Arrange
            var input = new MemoryStream([1]);
            const string encodingName = EncodingNames.Brotli;
            const CompressionType type = CompressionType.Brotli;

            var compressionMock = new Mock<ICompression>();

            compressionMock.SetupGet(x => x.EncodingName).Returns(encodingName);
            compressionMock.SetupGet(x => x.Type).Returns(CompressionType.Brotli);

            compressionMock.Setup(c => c.Compress(input)).Returns([]);

            var compressionProvider = new CompressionProvider([compressionMock.Object]);

            // Act
            compressionProvider.Compress(type, input);
            compressionProvider.Compress(encodingName, input);

            // Assert
            compressionMock.Verify(c => c.Compress(input), Times.Exactly(2));

            input.Dispose();
        }

        [Fact]
        public void Compress_StreamInput_WithLeaveOpenTrue_ShouldCallAppropriateMethod()
        {
            // Arrange
            var input = new MemoryStream([1]);
            const string encodingName = EncodingNames.Brotli;
            const CompressionType type = CompressionType.Brotli;
            const bool leaveOpen = true;
            var cancellationToken = CancellationToken.None;

            var compressionMock = new Mock<ICompression>();

            compressionMock.SetupGet(x => x.EncodingName).Returns(encodingName);
            compressionMock.SetupGet(x => x.Type).Returns(CompressionType.Brotli);

            compressionMock.Setup(c => c.Compress(input, leaveOpen)).Returns([]);

            var compressionProvider = new CompressionProvider([compressionMock.Object]);

            // Act
            compressionProvider.Compress(type, input, leaveOpen);
            compressionProvider.Compress(encodingName, input, leaveOpen);

            // Assert
            compressionMock.Verify(c => c.Compress(input, leaveOpen), Times.Exactly(2));

            input.Dispose();
        }

        [Fact]
        public void Compress_StringInput_WithLevel_ShouldCallAppropriateMethod()
        {
            // Arrange
            const string input = "Hello, World!";
            const string encodingName = EncodingNames.Brotli;
            const CompressionType type = CompressionType.Brotli;
            const CompressionLevel level = CompressionLevel.Optimal;

            var compressionMock = new Mock<ICompression>();

            compressionMock.SetupGet(x => x.EncodingName).Returns(encodingName);
            compressionMock.SetupGet(x => x.Type).Returns(CompressionType.Brotli);

            compressionMock.Setup(c => c.Compress(input, level)).Returns([]);

            var compressionProvider = new CompressionProvider([compressionMock.Object]);

            // Act
            compressionProvider.Compress(type, input, level);
            compressionProvider.Compress(encodingName, input, level);

            // Assert
            compressionMock.Verify(c => c.Compress(input, level), Times.Exactly(2));
        }

        [Fact]
        public void Compress_BytesInput_WithLevel_ShouldCallAppropriateMethod()
        {
            // Arrange
            byte[] input = [1];
            const string encodingName = EncodingNames.Brotli;
            const CompressionType type = CompressionType.Brotli;
            const CompressionLevel level = CompressionLevel.Optimal;

            var compressionMock = new Mock<ICompression>();

            compressionMock.SetupGet(x => x.EncodingName).Returns(encodingName);
            compressionMock.SetupGet(x => x.Type).Returns(CompressionType.Brotli);

            compressionMock.Setup(c => c.Compress(input, level)).Returns([]);

            var compressionProvider = new CompressionProvider([compressionMock.Object]);

            // Act
            compressionProvider.Compress(type, input, level);
            compressionProvider.Compress(encodingName, input, level);

            // Assert
            compressionMock.Verify(c => c.Compress(input, level), Times.Exactly(2));
        }

        [Fact]
        public void Compress_StreamInput_WithLevel_ShouldCallAppropriateMethod()
        {
            // Arrange
            var input = new MemoryStream([1]);
            const string encodingName = EncodingNames.Brotli;
            const CompressionType type = CompressionType.Brotli;
            const CompressionLevel level = CompressionLevel.Optimal;

            var compressionMock = new Mock<ICompression>();

            compressionMock.SetupGet(x => x.EncodingName).Returns(encodingName);
            compressionMock.SetupGet(x => x.Type).Returns(CompressionType.Brotli);

            compressionMock.Setup(c => c.Compress(input, level)).Returns([]);

            var compressionProvider = new CompressionProvider([compressionMock.Object]);

            // Act
            compressionProvider.Compress(type, input, level);
            compressionProvider.Compress(encodingName, input, level);

            // Assert
            compressionMock.Verify(c => c.Compress(input, level), Times.Exactly(2));

            input.Dispose();
        }

        [Fact]
        public void Compress_StreamInput_WithLevelAndLeaveOpenTrue_ShouldCallAppropriateMethod()
        {
            // Arrange
            var input = new MemoryStream([1]);
            const string encodingName = EncodingNames.Brotli;
            const CompressionType type = CompressionType.Brotli;
            const CompressionLevel level = CompressionLevel.Optimal;
            const bool leaveOpen = true;

            var compressionMock = new Mock<ICompression>();

            compressionMock.SetupGet(x => x.EncodingName).Returns(encodingName);
            compressionMock.SetupGet(x => x.Type).Returns(CompressionType.Brotli);

            compressionMock.Setup(c => c.Compress(input, level, leaveOpen)).Returns([]);

            var compressionProvider = new CompressionProvider([compressionMock.Object]);

            // Act
            compressionProvider.Compress(type, input, level, leaveOpen);
            compressionProvider.Compress(encodingName, input, level, leaveOpen);

            // Assert
            compressionMock.Verify(c => c.Compress(input, level, leaveOpen), Times.Exactly(2));

            input.Dispose();
        }
    }
}