using Ogu.Compressions.Abstractions;
using System.IO.Compression;

namespace Ogu.Compressions.Tests.None
{
    public partial class NoneCompressionTests
    {
        private readonly NoneCompression _noneCompression;

        public NoneCompressionTests()
        {
            _noneCompression = new NoneCompression(new NoneCompressionOptions());
        }

        [Fact]
        public void Constructor_WhenCalled_InitializesCorrectly()
        {
            // Assert
            Assert.NotNull(_noneCompression);
            Assert.Equal(CompressionDefaults.EncodingNames.None, _noneCompression.EncodingName);
            Assert.Equal(CompressionLevel.Fastest, _noneCompression.Level);
            Assert.Equal(CompressionDefaults.BufferSize, _noneCompression.BufferSize);
        }

        [Fact]
        public async Task CompressAsync_StringInput_ReturnsCompressedBytes()
        {
            // Arrange
            const string input = "Hello, World!";
            var expected = new byte[] { 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var actual = await _noneCompression.CompressAsync(input);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CompressAsync_BytesInput_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var expected = new byte[] { 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var actual = await _noneCompression.CompressAsync(input);

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
            var expected = new byte[] { 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var actual = await _noneCompression.CompressAsync(stream);

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
            var expected = new byte[] { 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var actual = await _noneCompression.CompressAsync(stream, leaveOpen: true);

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
            var expected = new byte[] { 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var actual = await _noneCompression.CompressAsync(input, CompressionLevel.Fastest);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CompressAsync_BytesInput_WithLevel_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var expected = new byte[] { 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var actual = await _noneCompression.CompressAsync(input, CompressionLevel.Fastest);

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
            var expected = new byte[] { 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var actual = await _noneCompression.CompressAsync(stream, CompressionLevel.Fastest);

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
            var expected = new byte[] { 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };
            const bool leaveOpen = true;

            // Act
            var actual = await _noneCompression.CompressAsync(stream, CompressionLevel.Fastest, leaveOpen);

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
            var expected = new byte[] { 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var stream = await _noneCompression.CompressToStreamAsync(input);

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
            var expected = new byte[] { 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var stream = await _noneCompression.CompressToStreamAsync(input);

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
            var expected = new byte[] { 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var stream = await _noneCompression.CompressToStreamAsync(rawStream);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);
            Assert.Equal(rawStream, stream);

            var actual = ((MemoryStream)stream).ToArray();

            Assert.Equal(expected, actual);

            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => rawStream.Length);
            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task CompressToStreamAsync_StreamInput_WithLeaveOpenTrue_LeavesStreamOpen()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var rawStream = new MemoryStream(input);
            var expected = new byte[] { 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var stream = await _noneCompression.CompressToStreamAsync(rawStream, leaveOpen: true);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);
            Assert.Equal(13, stream.Length);

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
            var expected = new byte[] { 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var stream = await _noneCompression.CompressToStreamAsync(input, CompressionLevel.Fastest);

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
            var expected = new byte[] { 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var stream = await _noneCompression.CompressToStreamAsync(input, CompressionLevel.Fastest);

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
            var expected = new byte[] { 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var stream = await _noneCompression.CompressToStreamAsync(rawStream, CompressionLevel.Fastest);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);
            Assert.Equal(rawStream, stream);

            var actual = ((MemoryStream)stream).ToArray();

            Assert.Equal(expected, actual);

            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => rawStream.Length);
            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task CompressToStreamAsync_StreamInput_WithLevelAndLeaveOpenTrue_LeavesStreamOpen()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var rawStream = new MemoryStream(input);
            var expected = new byte[] { 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };
            const bool leaveOpen = true;

            // Act
            var stream = await _noneCompression.CompressToStreamAsync(rawStream, CompressionLevel.Fastest, leaveOpen);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);
            Assert.Equal(13, stream.Length);

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
            var expected = new byte[] { 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var actual = _noneCompression.Compress(input);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Compress_BytesInput_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var expected = new byte[] { 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var actual = _noneCompression.Compress(input);

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
            var expected = new byte[] { 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var actual = _noneCompression.Compress(stream);

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
            var expected = new byte[] { 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var actual = _noneCompression.Compress(stream, leaveOpen: true);

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
            var expected = new byte[] { 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var actual = _noneCompression.Compress(input, CompressionLevel.Fastest);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Compress_BytesInput_WithLevel_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var expected = new byte[] { 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var actual = _noneCompression.Compress(input, CompressionLevel.Fastest);

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
            var expected = new byte[] { 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var actual = _noneCompression.Compress(stream, CompressionLevel.Fastest);

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
            var expected = new byte[] { 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };
            const bool leaveOpen = true;

            // Act
            var actual = _noneCompression.Compress(stream, CompressionLevel.Fastest, leaveOpen);

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
            var expected = new byte[] { 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var stream = _noneCompression.CompressToStream(input);

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
            var expected = new byte[] { 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var stream = _noneCompression.CompressToStream(input);

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
            var expected = new byte[] { 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var stream = _noneCompression.CompressToStream(rawStream);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);
            Assert.Equal(rawStream, stream);

            var actual = ((MemoryStream)stream).ToArray();

            Assert.Equal(expected, actual);

            stream.Dispose();

            Assert.Throws<ObjectDisposedException>(() => rawStream.Length);
            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public void CompressToStream_StreamInput_WithLeaveOpenTrue_LeavesStreamOpen()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var rawStream = new MemoryStream(input);
            var expected = new byte[] { 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var stream = _noneCompression.CompressToStream(rawStream, leaveOpen: true);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);
            Assert.Equal(13, stream.Length);

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
            var expected = new byte[] { 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var stream = _noneCompression.CompressToStream(input, CompressionLevel.Fastest);

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
            var expected = new byte[] { 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var stream = _noneCompression.CompressToStream(input, CompressionLevel.Fastest);

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
            var expected = new byte[] { 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var stream = _noneCompression.CompressToStream(rawStream, CompressionLevel.Fastest);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);
            Assert.Equal(rawStream, stream);

            var actual = ((MemoryStream)stream).ToArray();

            Assert.Equal(expected, actual);

            stream.Dispose();

            Assert.Throws<ObjectDisposedException>(() => rawStream.Length);
            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public void CompressToStream_StreamInput_WithLevelAndLeaveOpenTrue_LeavesStreamOpen()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var rawStream = new MemoryStream(input);
            var expected = new byte[] { 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };
            const bool leaveOpen = true;

            // Act
            var stream = _noneCompression.CompressToStream(rawStream, CompressionLevel.Fastest, leaveOpen);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);
            Assert.Equal(13, stream.Length);

            var actual = ((MemoryStream)stream).ToArray();

            Assert.Equal(expected, actual);

            rawStream.Dispose();
            stream.Dispose();

            Assert.Throws<ObjectDisposedException>(() => rawStream.Length);
            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }
    }
}
