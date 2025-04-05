using Microsoft.Extensions.Options;
using Ogu.Compressions.Abstractions;
using System.IO.Compression;

namespace Ogu.Compressions.Tests.GzipCompression
{
    public partial class GzipCompressionTests
    {
        private readonly Compressions.GzipCompression _gzipCompression;

        public GzipCompressionTests()
        {
            _gzipCompression =
                new Compressions.GzipCompression(
                    Options.Create<GzipCompressionOptions>(new GzipCompressionOptions()));
        }

        [Fact]
        public void Constructor_WhenCalled_InitializesCorrectly()
        {
            // Assert
            Assert.NotNull(_gzipCompression);
            Assert.Equal(EncodingNames.Gzip, _gzipCompression.EncodingName);
            Assert.Equal(CompressionLevel.Fastest, _gzipCompression.Level);
            Assert.Equal(81920, _gzipCompression.BufferSize);
        }

        [Fact]
        public async Task CompressAsync_StringInput_ReturnsCompressedBytes()
        {
            // Arrange
            const string input = "Hello, World!";

            // Act
            var actual = await _gzipCompression.CompressAsync(input);

            // Assert
            Assert.NotEmpty(actual);
        }

        [Fact]
        public async Task CompressAsync_BytesInput_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();

            // Act
            var actual = await _gzipCompression.CompressAsync(input);

            // Assert
            Assert.NotEmpty(actual);
        }

        [Fact]
        public async Task CompressAsync_StreamInput_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);

            // Act
            var actual = await _gzipCompression.CompressAsync(stream);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task CompressAsync_StreamInput_WithLeaveOpenTrue_LeavesStreamOpen()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);

            // Act
            var actual = await _gzipCompression.CompressAsync(stream, leaveOpen: true);

            // Assert
            Assert.NotEmpty(actual);

            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task CompressAsync_StringInput_WithLevel_ReturnsCompressedBytes()
        {
            // Arrange
            const string input = "Hello, World!";

            // Act
            var actual = await _gzipCompression.CompressAsync(input, CompressionLevel.Fastest);

            // Assert
            Assert.NotEmpty(actual);
        }

        [Fact]
        public async Task CompressAsync_BytesInput_WithLevel_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();

            // Act
            var actual = await _gzipCompression.CompressAsync(input, CompressionLevel.Fastest);

            // Assert
            Assert.NotEmpty(actual);
        }

        [Fact]
        public async Task CompressAsync_StreamInput_WithLevel_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);

            // Act
            var actual = await _gzipCompression.CompressAsync(stream, CompressionLevel.Fastest);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task CompressAsync_StreamInput_WithLevelAndLeaveOpenTrue_LeavesStreamOpen()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);

            // Act
            var actual = await _gzipCompression.CompressAsync(stream, leaveOpen: true, CompressionLevel.Fastest);

            // Assert
            Assert.NotEmpty(actual);

            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task CompressToStreamAsync_StringInput_ReturnsCompressedStream()
        {
            // Arrange
            const string input = "Hello, World!";

            // Act
            var stream = await _gzipCompression.CompressToStreamAsync(input);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);

            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task CompressToStreamAsync_BytesInput_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();

            // Act
            var stream = await _gzipCompression.CompressToStreamAsync(input);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);

            var actual = ((MemoryStream)stream).ToArray();

            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task CompressToStreamAsync_StreamInput_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var rawStream = new MemoryStream(input);

            // Act
            var stream = await _gzipCompression.CompressToStreamAsync(rawStream);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);
            Assert.Throws<ObjectDisposedException>(() => rawStream.Length);

            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task CompressToStreamAsync_StreamInput_WithLeaveOpenTrue_LeavesStreamOpen()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var rawStream = new MemoryStream(input);

            // Act
            var stream = await _gzipCompression.CompressToStreamAsync(rawStream, leaveOpen: true);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);

            await rawStream.DisposeAsync();
            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => rawStream.Length);
            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task CompressToStreamAsync_StringInput_WithLevel_ReturnsCompressedStream()
        {
            // Arrange
            const string input = "Hello, World!";

            // Act
            var stream = await _gzipCompression.CompressToStreamAsync(input, CompressionLevel.Fastest);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);

            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task CompressToStreamAsync_BytesInput_WithLevel_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();

            // Act
            var stream = await _gzipCompression.CompressToStreamAsync(input, CompressionLevel.Fastest);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);

            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task CompressToStreamAsync_StreamInput_WithLevel_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var rawStream = new MemoryStream(input);

            // Act
            var stream = await _gzipCompression.CompressToStreamAsync(rawStream, CompressionLevel.Fastest);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);
            Assert.Throws<ObjectDisposedException>(() => rawStream.Length);

            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task CompressToStreamAsync_StreamInput_WithLevelAndLeaveOpenTrue_LeavesStreamOpen()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var rawStream = new MemoryStream(input);

            // Act
            var stream = await _gzipCompression.CompressToStreamAsync(rawStream, leaveOpen: true, CompressionLevel.Fastest);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);

            await rawStream.DisposeAsync();
            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => rawStream.Length);
            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public void Compress_StringInput_ReturnsCompressedBytes()
        {
            // Arrange
            const string input = "Hello, World!";

            // Act
            var actual = _gzipCompression.Compress(input);

            // Assert
            Assert.NotEmpty(actual);
        }

        [Fact]
        public void Compress_BytesInput_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();

            // Act
            var actual = _gzipCompression.Compress(input);

            // Assert
            Assert.NotEmpty(actual);
        }

        [Fact]
        public void Compress_StreamInput_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);

            // Act
            var actual = _gzipCompression.Compress(stream);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public void Compress_StreamInput_WithLeaveOpenTrue_LeavesStreamOpen()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);

            // Act
            var actual = _gzipCompression.Compress(stream, leaveOpen: true);

            // Assert
            Assert.NotEmpty(actual);

            stream.Dispose();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public void Compress_StringInput_WithLevel_ReturnsCompressedBytes()
        {
            // Arrange
            const string input = "Hello, World!";

            // Act
            var actual = _gzipCompression.Compress(input, CompressionLevel.Fastest);

            // Assert
            Assert.NotEmpty(actual);
        }

        [Fact]
        public void Compress_BytesInput_WithLevel_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();

            // Act
            var actual = _gzipCompression.Compress(input, CompressionLevel.Fastest);

            // Assert
            Assert.NotEmpty(actual);
        }

        [Fact]
        public void Compress_StreamInput_WithLevel_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);

            // Act
            var actual = _gzipCompression.Compress(stream, CompressionLevel.Fastest);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public void Compress_StreamInput_WithLevelAndLeaveOpenTrue_LeavesStreamOpen()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);

            // Act
            var actual = _gzipCompression.Compress(stream, leaveOpen: true, CompressionLevel.Fastest);

            // Assert
            Assert.NotEmpty(actual);

            stream.Dispose();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public void CompressToStream_StringInput_ReturnsCompressedStream()
        {
            // Arrange
            const string input = "Hello, World!";

            // Act
            var stream = _gzipCompression.CompressToStream(input);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);

            stream.Dispose();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public void CompressToStream_BytesInput_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();

            // Act
            var stream = _gzipCompression.CompressToStream(input);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);

            stream.Dispose();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public void CompressToStream_StreamInput_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var rawStream = new MemoryStream(input);

            // Act
            var stream = _gzipCompression.CompressToStream(rawStream);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);
            Assert.Throws<ObjectDisposedException>(() => rawStream.Length);

            stream.Dispose();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public void CompressToStream_StreamInput_WithLeaveOpenTrue_LeavesStreamOpen()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var rawStream = new MemoryStream(input);

            // Act
            var stream = _gzipCompression.CompressToStream(rawStream, leaveOpen: true);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);

            rawStream.Dispose();
            stream.Dispose();

            Assert.Throws<ObjectDisposedException>(() => rawStream.Length);
            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public void CompressToStream_StringInput_WithLevel_ReturnsCompressedStream()
        {
            // Arrange
            const string input = "Hello, World!";

            // Act
            var stream = _gzipCompression.CompressToStream(input, CompressionLevel.Fastest);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);

            stream.Dispose();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public void CompressToStream_BytesInput_WithLevel_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();

            // Act
            var stream = _gzipCompression.CompressToStream(input, CompressionLevel.Fastest);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);

            stream.Dispose();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public void CompressToStream_StreamInput_WithLevel_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var rawStream = new MemoryStream(input);

            // Act
            var stream = _gzipCompression.CompressToStream(rawStream, CompressionLevel.Fastest);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);
            Assert.Throws<ObjectDisposedException>(() => rawStream.Length);

            stream.Dispose();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public void CompressToStream_StreamInput_WithLevelAndLeaveOpenTrue_LeavesStreamOpen()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var rawStream = new MemoryStream(input);

            // Act
            var stream = _gzipCompression.CompressToStream(rawStream, leaveOpen: true, CompressionLevel.Fastest);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);

            rawStream.Dispose();
            stream.Dispose();

            Assert.Throws<ObjectDisposedException>(() => rawStream.Length);
            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }
    }
}