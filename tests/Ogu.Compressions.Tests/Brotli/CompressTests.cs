using Ogu.Compressions.Abstractions;
using System.IO.Compression;

namespace Ogu.Compressions.Tests.Brotli
{
    public partial class BrotliCompressionTests
    {
        private readonly BrotliCompression _brotliCompression;

        public BrotliCompressionTests()
        {
            _brotliCompression =
                new BrotliCompression(new BrotliCompressionOptions());
        }

        [Fact]
        public void Constructor_WhenCalled_InitializesCorrectly()
        {
            // Assert
            Assert.NotNull(_brotliCompression);
            Assert.Equal(CompressionDefaults.EncodingNames.Brotli, _brotliCompression.EncodingName);
            Assert.Equal(CompressionLevel.Fastest, _brotliCompression.Level);
            Assert.Equal(CompressionDefaults.BufferSize, _brotliCompression.BufferSize);
        }

        [Fact]
        public async Task CompressAsync_StringInput_ReturnsCompressedBytes()
        {
            // Arrange
            const string input = "Hello, World!";
            var expected = new byte[] { 11, 6, 128, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33, 3 };

            // Act
            var actual = await _brotliCompression.CompressAsync(input);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CompressAsync_BytesInput_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var expected = new byte[] { 11, 6, 128, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33, 3 };

            // Act
            var actual = await _brotliCompression.CompressAsync(input);

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
            stream.Position = stream.Length;
            var expected = new byte[] { 11, 6, 128, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33, 3 };

            // Act
            var actual = await _brotliCompression.CompressAsync(stream);

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
            stream.Position = stream.Length;
            var expected = new byte[] { 11, 6, 128, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33, 3 };

            // Act
            var actual = await _brotliCompression.CompressAsync(stream, leaveOpen: true);

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
            var expected = new byte[] { 11, 6, 128, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33, 3 };

            // Act
            var actual = await _brotliCompression.CompressAsync(input, CompressionLevel.Optimal);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CompressAsync_BytesInput_WithLevel_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var expected = new byte[] { 11, 6, 128, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33, 3 };

            // Act
            var actual = await _brotliCompression.CompressAsync(input, CompressionLevel.Optimal);

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
            stream.Position = stream.Length;
            var expected = new byte[] { 11, 6, 128, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33, 3 };

            // Act
            var actual = await _brotliCompression.CompressAsync(stream, CompressionLevel.Optimal);

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
            var expected = new byte[] { 11, 6, 128, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33, 3 };
            const bool leaveOpen = true;

            // Act
            var actual = await _brotliCompression.CompressAsync(stream, CompressionLevel.Optimal, leaveOpen);

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
            var expected = new byte[] { 11, 6, 128, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33, 3 };

            // Act
            var stream = await _brotliCompression.CompressToStreamAsync(input);

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
            var expected = new byte[] { 11, 6, 128, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33, 3 };

            // Act
            var stream = await _brotliCompression.CompressToStreamAsync(input);

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
            rawStream.Position = rawStream.Length;
            var expected = new byte[] { 11, 6, 128, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33, 3 };

            // Act
            var stream = await _brotliCompression.CompressToStreamAsync(rawStream);

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
            rawStream.Position = rawStream.Length;
            var expected = new byte[] { 11, 6, 128, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33, 3 };

            // Act
            var stream = await _brotliCompression.CompressToStreamAsync(rawStream, leaveOpen: true);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);
            Assert.Equal(17, stream.Length);

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
            var expected = new byte[] { 11, 6, 128, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33, 3 };

            // Act
            var stream = await _brotliCompression.CompressToStreamAsync(input, CompressionLevel.Optimal);

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
            var expected = new byte[] { 11, 6, 128, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33, 3 };

            // Act
            var stream = await _brotliCompression.CompressToStreamAsync(input, CompressionLevel.Optimal);

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
            rawStream.Position = rawStream.Length;
            var expected = new byte[] { 11, 6, 128, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33, 3 };

            // Act
            var stream = await _brotliCompression.CompressToStreamAsync(rawStream, CompressionLevel.Optimal);

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
            rawStream.Position = rawStream.Length;
            var expected = new byte[] { 11, 6, 128, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33, 3 };
            const bool leaveOpen = true;

            // Act
            var stream = await _brotliCompression.CompressToStreamAsync(rawStream, CompressionLevel.Optimal, leaveOpen);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);
            Assert.Equal(17, stream.Length);

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
            var expected = new byte[] { 11, 6, 128, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33, 3 };

            // Act
            var actual = _brotliCompression.Compress(input);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Compress_BytesInput_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var expected = new byte[] { 11, 6, 128, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33, 3 };

            // Act
            var actual = _brotliCompression.Compress(input);

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
            var expected = new byte[] { 11, 6, 128, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33, 3 };

            // Act
            var actual = _brotliCompression.Compress(stream);

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
            var expected = new byte[] { 11, 6, 128, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33, 3 };

            // Act
            var actual = _brotliCompression.Compress(stream, leaveOpen: true);

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
            var expected = new byte[] { 11, 6, 128, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33, 3 };

            // Act
            var actual = _brotliCompression.Compress(input, CompressionLevel.Optimal);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Compress_BytesInput_WithLevel_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var expected = new byte[] { 11, 6, 128, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33, 3 };

            // Act
            var actual = _brotliCompression.Compress(input, CompressionLevel.Optimal);

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
            stream.Position = stream.Length;
            var expected = new byte[] { 11, 6, 128, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33, 3 };

            // Act
            var actual = _brotliCompression.Compress(stream, CompressionLevel.Optimal);

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
            stream.Position = stream.Length;
            var expected = new byte[] { 11, 6, 128, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33, 3 };
            const bool leaveOpen = true;

            // Act
            var actual = _brotliCompression.Compress(stream, CompressionLevel.Optimal, leaveOpen);

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
            var expected = new byte[] { 11, 6, 128, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33, 3 };

            // Act
            var stream = _brotliCompression.CompressToStream(input);

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
            var expected = new byte[] { 11, 6, 128, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33, 3 };

            // Act
            var stream = _brotliCompression.CompressToStream(input);

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
            rawStream.Position = rawStream.Length;
            var expected = new byte[] { 11, 6, 128, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33, 3 };

            // Act
            var stream = _brotliCompression.CompressToStream(rawStream);

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
            rawStream.Position = rawStream.Length;
            var expected = new byte[] { 11, 6, 128, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33, 3 };

            // Act
            var stream = _brotliCompression.CompressToStream(rawStream, leaveOpen: true);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);
            Assert.Equal(17, stream.Length);

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
            var expected = new byte[] { 11, 6, 128, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33, 3 };

            // Act
            var stream = _brotliCompression.CompressToStream(input, CompressionLevel.Optimal);

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
            var expected = new byte[] { 11, 6, 128, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33, 3 };

            // Act
            var stream = _brotliCompression.CompressToStream(input, CompressionLevel.Optimal);

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
            rawStream.Position = rawStream.Length;
            var expected = new byte[] { 11, 6, 128, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33, 3 };

            // Act
            var stream = _brotliCompression.CompressToStream(rawStream, CompressionLevel.Optimal);

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
            rawStream.Position = rawStream.Length;
            var expected = new byte[] { 11, 6, 128, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33, 3 };
            const bool leaveOpen = true;

            // Act
            var stream = _brotliCompression.CompressToStream(rawStream, CompressionLevel.Optimal, leaveOpen);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);
            Assert.Equal(17, stream.Length);

            var actual = ((MemoryStream)stream).ToArray();

            Assert.Equal(expected, actual);

            rawStream.Dispose();
            stream.Dispose();

            Assert.Throws<ObjectDisposedException>(() => rawStream.Length);
            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }
    }
}