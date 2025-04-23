using Microsoft.Extensions.Options;
using System.IO.Compression;
using Ogu.Compressions.Abstractions;

namespace Ogu.Compressions.Tests.ZstdCompression
{
    public partial class ZstdCompressionTests
    {
        private readonly Compressions.ZstdCompression _zstdCompression;

        public ZstdCompressionTests()
        {
            _zstdCompression =
                new Compressions.ZstdCompression(
                    Options.Create<ZstdCompressionOptions>(new ZstdCompressionOptions()));
        }

        [Fact]
        public void Constructor_WhenCalled_InitializesCorrectly()
        {
            // Assert
            Assert.NotNull(_zstdCompression);
            Assert.Equal(EncodingNames.Zstd, _zstdCompression.EncodingName);
            Assert.Equal(CompressionLevel.Fastest, _zstdCompression.Level);
            Assert.Equal(CompressionDefaults.BufferSize, _zstdCompression.BufferSize);
        }

        [Fact]
        public async Task CompressAsync_StringInput_ReturnsCompressedBytes()
        {
            // Arrange
            const string input = "Hello, World!";
            var expected = new byte[] { 40, 181, 47, 253, 0, 72, 105, 0, 0, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var actual = await _zstdCompression.CompressAsync(input);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CompressAsync_BytesInput_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var expected = new byte[] { 40, 181, 47, 253, 0, 72, 105, 0, 0, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var actual = await _zstdCompression.CompressAsync(input);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CompressAsync_StreamInput_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);
            var expected = new byte[] { 40, 181, 47, 253, 0, 72, 105, 0, 0, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var actual = await _zstdCompression.CompressAsync(stream);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task CompressAsync_StreamInput_WithLeaveOpenTrue_LeavesStreamOpen()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);
            var expected = new byte[] { 40, 181, 47, 253, 0, 72, 105, 0, 0, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var actual = await _zstdCompression.CompressAsync(stream, leaveOpen: true);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
            Assert.Equal(13, stream.Length);

            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task CompressAsync_StringInput_WithLevel_ReturnsCompressedBytes()
        {
            // Arrange
            const string input = "Hello, World!";
            var expected = new byte[] { 40, 181, 47, 253, 0, 72, 105, 0, 0, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var actual = await _zstdCompression.CompressAsync(input, CompressionLevel.Fastest);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CompressAsync_BytesInput_WithLevel_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var expected = new byte[] { 40, 181, 47, 253, 0, 72, 105, 0, 0, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var actual = await _zstdCompression.CompressAsync(input, CompressionLevel.Fastest);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CompressAsync_StreamInput_WithLevel_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);
            var expected = new byte[] { 40, 181, 47, 253, 0, 72, 105, 0, 0, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var actual = await _zstdCompression.CompressAsync(stream, CompressionLevel.Fastest);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task CompressAsync_StreamInput_WithLevelAndLeaveOpenTrue_LeavesStreamOpen()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);
            var expected = new byte[] { 40, 181, 47, 253, 0, 72, 105, 0, 0, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var actual = await _zstdCompression.CompressAsync(stream, leaveOpen: true, CompressionLevel.Fastest);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
            Assert.Equal(13, stream.Length);

            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task CompressToStreamAsync_StringInput_ReturnsCompressedStream()
        {
            // Arrange
            const string input = "Hello, World!";
            var expected = new byte[] { 40, 181, 47, 253, 0, 72, 105, 0, 0, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var stream = await _zstdCompression.CompressToStreamAsync(input);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);

            var actual = ((MemoryStream)stream).ToArray();

            Assert.Equal(expected, actual);

            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task CompressToStreamAsync_BytesInput_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var expected = new byte[] { 40, 181, 47, 253, 0, 72, 105, 0, 0, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var stream = await _zstdCompression.CompressToStreamAsync(input);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);

            var actual = ((MemoryStream)stream).ToArray();

            Assert.Equal(expected, actual);

            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task CompressToStreamAsync_StreamInput_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var rawStream = new MemoryStream(input);
            var expected = new byte[] { 40, 181, 47, 253, 0, 72, 105, 0, 0, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var stream = await _zstdCompression.CompressToStreamAsync(rawStream);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);
            Assert.Throws<ObjectDisposedException>(() => rawStream.Length);

            var actual = ((MemoryStream)stream).ToArray();

            Assert.Equal(expected, actual);

            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task CompressToStreamAsync_StreamInput_WithLeaveOpenTrue_LeavesStreamOpen()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var rawStream = new MemoryStream(input);
            var expected = new byte[] { 40, 181, 47, 253, 0, 72, 105, 0, 0, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var stream = await _zstdCompression.CompressToStreamAsync(rawStream, leaveOpen: true);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);
            Assert.Equal(expected.Length, stream.Length);

            var actual = ((MemoryStream)stream).ToArray();

            Assert.Equal(expected, actual);

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
            var expected = new byte[] { 40, 181, 47, 253, 0, 72, 105, 0, 0, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var stream = await _zstdCompression.CompressToStreamAsync(input, CompressionLevel.Fastest);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);

            var actual = ((MemoryStream)stream).ToArray();

            Assert.Equal(expected, actual);

            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task CompressToStreamAsync_BytesInput_WithLevel_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var expected = new byte[] { 40, 181, 47, 253, 0, 72, 105, 0, 0, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var stream = await _zstdCompression.CompressToStreamAsync(input, CompressionLevel.Fastest);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);

            var actual = ((MemoryStream)stream).ToArray();

            Assert.Equal(expected, actual);

            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task CompressToStreamAsync_StreamInput_WithLevel_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var rawStream = new MemoryStream(input);
            var expected = new byte[] { 40, 181, 47, 253, 0, 72, 105, 0, 0, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var stream = await _zstdCompression.CompressToStreamAsync(rawStream, CompressionLevel.Fastest);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);
            Assert.Throws<ObjectDisposedException>(() => rawStream.Length);

            var actual = ((MemoryStream)stream).ToArray();

            Assert.Equal(expected, actual);

            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task CompressToStreamAsync_StreamInput_WithLevelAndLeaveOpenTrue_LeavesStreamOpen()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var rawStream = new MemoryStream(input);
            var expected = new byte[] { 40, 181, 47, 253, 0, 72, 105, 0, 0, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var stream = await _zstdCompression.CompressToStreamAsync(rawStream, leaveOpen: true, CompressionLevel.Fastest);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);
            Assert.Equal(expected.Length, stream.Length);

            var actual = ((MemoryStream)stream).ToArray();

            Assert.Equal(expected, actual);

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
            var expected = new byte[] { 40, 181, 47, 253, 0, 72, 105, 0, 0, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var actual = _zstdCompression.Compress(input);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Compress_BytesInput_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var expected = new byte[] { 40, 181, 47, 253, 0, 72, 105, 0, 0, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var actual = _zstdCompression.Compress(input);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Compress_StreamInput_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);
            var expected = new byte[] { 40, 181, 47, 253, 0, 72, 105, 0, 0, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var actual = _zstdCompression.Compress(stream);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public void Compress_StreamInput_WithLeaveOpenTrue_LeavesStreamOpen()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);
            var expected = new byte[] { 40, 181, 47, 253, 0, 72, 105, 0, 0, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var actual = _zstdCompression.Compress(stream, leaveOpen: true);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
            Assert.Equal(13, stream.Length);

            stream.Dispose();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public void Compress_StringInput_WithLevel_ReturnsCompressedBytes()
        {
            // Arrange
            const string input = "Hello, World!";
            var expected = new byte[] { 40, 181, 47, 253, 0, 72, 105, 0, 0, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var actual = _zstdCompression.Compress(input, CompressionLevel.Fastest);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Compress_BytesInput_WithLevel_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var expected = new byte[] { 40, 181, 47, 253, 0, 72, 105, 0, 0, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var actual = _zstdCompression.Compress(input, CompressionLevel.Fastest);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Compress_StreamInput_WithLevel_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);
            var expected = new byte[] { 40, 181, 47, 253, 0, 72, 105, 0, 0, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var actual = _zstdCompression.Compress(stream, CompressionLevel.Fastest);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public void Compress_StreamInput_WithLevelAndLeaveOpenTrue_LeavesStreamOpen()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);
            var expected = new byte[] { 40, 181, 47, 253, 0, 72, 105, 0, 0, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var actual = _zstdCompression.Compress(stream, leaveOpen: true, CompressionLevel.Fastest);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
            Assert.Equal(13, stream.Length);

            stream.Dispose();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public void CompressToStream_StringInput_ReturnsCompressedStream()
        {
            // Arrange
            const string input = "Hello, World!";
            var expected = new byte[] { 40, 181, 47, 253, 0, 72, 105, 0, 0, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var stream = _zstdCompression.CompressToStream(input);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);

            var actual = ((MemoryStream)stream).ToArray();

            Assert.Equal(expected, actual);

            stream.Dispose();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public void CompressToStream_BytesInput_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var expected = new byte[] { 40, 181, 47, 253, 0, 72, 105, 0, 0, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var stream = _zstdCompression.CompressToStream(input);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);

            var actual = ((MemoryStream)stream).ToArray();

            Assert.Equal(expected, actual);

            stream.Dispose();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public void CompressToStream_StreamInput_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var rawStream = new MemoryStream(input);
            var expected = new byte[] { 40, 181, 47, 253, 0, 72, 105, 0, 0, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var stream = _zstdCompression.CompressToStream(rawStream);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);
            Assert.Throws<ObjectDisposedException>(() => rawStream.Length);

            var actual = ((MemoryStream)stream).ToArray();

            Assert.Equal(expected, actual);

            stream.Dispose();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public void CompressToStream_StreamInput_WithLeaveOpenTrue_LeavesStreamOpen()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var rawStream = new MemoryStream(input);
            var expected = new byte[] { 40, 181, 47, 253, 0, 72, 105, 0, 0, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var stream = _zstdCompression.CompressToStream(rawStream, leaveOpen: true);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);
            Assert.Equal(expected.Length, stream.Length);

            var actual = ((MemoryStream)stream).ToArray();

            Assert.Equal(expected, actual);

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
            var expected = new byte[] { 40, 181, 47, 253, 0, 72, 105, 0, 0, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var stream = _zstdCompression.CompressToStream(input, CompressionLevel.Fastest);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);

            var actual = ((MemoryStream)stream).ToArray();

            Assert.Equal(expected, actual);

            stream.Dispose();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public void CompressToStream_BytesInput_WithLevel_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var expected = new byte[] { 40, 181, 47, 253, 0, 72, 105, 0, 0, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var stream = _zstdCompression.CompressToStream(input, CompressionLevel.Fastest);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);

            var actual = ((MemoryStream)stream).ToArray();

            Assert.Equal(expected, actual);

            stream.Dispose();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public void CompressToStream_StreamInput_WithLevel_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var rawStream = new MemoryStream(input);
            var expected = new byte[] { 40, 181, 47, 253, 0, 72, 105, 0, 0, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var stream = _zstdCompression.CompressToStream(rawStream, CompressionLevel.Fastest);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);
            Assert.Throws<ObjectDisposedException>(() => rawStream.Length);

            var actual = ((MemoryStream)stream).ToArray();

            Assert.Equal(expected, actual);

            stream.Dispose();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public void CompressToStream_StreamInput_WithLevelAndLeaveOpenTrue_LeavesStreamOpen()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var rawStream = new MemoryStream(input);
            var expected = new byte[] { 40, 181, 47, 253, 0, 72, 105, 0, 0, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var stream = _zstdCompression.CompressToStream(rawStream, leaveOpen: true, CompressionLevel.Fastest);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);
            Assert.Equal(expected.Length, stream.Length);

            var actual = ((MemoryStream)stream).ToArray();

            Assert.Equal(expected, actual);

            rawStream.Dispose();
            stream.Dispose();

            Assert.Throws<ObjectDisposedException>(() => rawStream.Length);
            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }
    }
}