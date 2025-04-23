using Microsoft.Extensions.Options;
using Ogu.Compressions.Abstractions;
using System.IO.Compression;

namespace Ogu.Compressions.Tests.SnappyCompression
{
    public partial class SnappyCompressionTests
    {
        private readonly Compressions.SnappyCompression _snappyCompression;

        public SnappyCompressionTests()
        {
            _snappyCompression =
                new Compressions.SnappyCompression(
                    Options.Create<SnappyCompressionOptions>(new SnappyCompressionOptions()));
        }

        [Fact]
        public void Constructor_WhenCalled_InitializesCorrectly()
        {
            // Assert
            Assert.NotNull(_snappyCompression);
            Assert.Equal(EncodingNames.Snappy, _snappyCompression.EncodingName);
            Assert.Equal(CompressionLevel.Fastest, _snappyCompression.Level);
            Assert.Equal(CompressionDefaults.BufferSize, _snappyCompression.BufferSize);
        }

        [Fact]
        public async Task CompressAsync_StringInput_ReturnsCompressedBytes()
        {
            // Arrange
            const string input = "Hello, World!";
            var expected = new byte[] { 255, 6, 0, 0, 115, 78, 97, 80, 112, 89, 1, 17, 0, 0, 130, 133, 83, 195, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var actual = await _snappyCompression.CompressAsync(input);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CompressAsync_BytesInput_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var expected = new byte[] { 255, 6, 0, 0, 115, 78, 97, 80, 112, 89, 1, 17, 0, 0, 130, 133, 83, 195, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var actual = await _snappyCompression.CompressAsync(input);

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
            var expected = new byte[] { 255, 6, 0, 0, 115, 78, 97, 80, 112, 89, 1, 17, 0, 0, 130, 133, 83, 195, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var actual = await _snappyCompression.CompressAsync(stream);

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
            var expected = new byte[] { 255, 6, 0, 0, 115, 78, 97, 80, 112, 89, 1, 17, 0, 0, 130, 133, 83, 195, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var actual = await _snappyCompression.CompressAsync(stream, leaveOpen: true);

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
            var expected = new byte[] { 255, 6, 0, 0, 115, 78, 97, 80, 112, 89, 1, 17, 0, 0, 130, 133, 83, 195, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var actual = await _snappyCompression.CompressAsync(input, CompressionLevel.Fastest);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task CompressAsync_BytesInput_WithLevel_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var expected = new byte[] { 255, 6, 0, 0, 115, 78, 97, 80, 112, 89, 1, 17, 0, 0, 130, 133, 83, 195, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var actual = await _snappyCompression.CompressAsync(input, CompressionLevel.Fastest);

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
            var expected = new byte[] { 255, 6, 0, 0, 115, 78, 97, 80, 112, 89, 1, 17, 0, 0, 130, 133, 83, 195, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var actual = await _snappyCompression.CompressAsync(stream, CompressionLevel.Fastest);

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
            var expected = new byte[] { 255, 6, 0, 0, 115, 78, 97, 80, 112, 89, 1, 17, 0, 0, 130, 133, 83, 195, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var actual = await _snappyCompression.CompressAsync(stream, leaveOpen: true, CompressionLevel.Fastest);

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
            var expected = new byte[] { 255, 6, 0, 0, 115, 78, 97, 80, 112, 89, 1, 17, 0, 0, 130, 133, 83, 195, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var stream = await _snappyCompression.CompressToStreamAsync(input);

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
            var expected = new byte[] { 255, 6, 0, 0, 115, 78, 97, 80, 112, 89, 1, 17, 0, 0, 130, 133, 83, 195, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var stream = await _snappyCompression.CompressToStreamAsync(input);

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
            var expected = new byte[] { 255, 6, 0, 0, 115, 78, 97, 80, 112, 89, 1, 17, 0, 0, 130, 133, 83, 195, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var stream = await _snappyCompression.CompressToStreamAsync(rawStream);

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
            var expected = new byte[] { 255, 6, 0, 0, 115, 78, 97, 80, 112, 89, 1, 17, 0, 0, 130, 133, 83, 195, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var stream = await _snappyCompression.CompressToStreamAsync(rawStream, leaveOpen: true);

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
            var expected = new byte[] { 255, 6, 0, 0, 115, 78, 97, 80, 112, 89, 1, 17, 0, 0, 130, 133, 83, 195, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var stream = await _snappyCompression.CompressToStreamAsync(input, CompressionLevel.Fastest);

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
            var expected = new byte[] { 255, 6, 0, 0, 115, 78, 97, 80, 112, 89, 1, 17, 0, 0, 130, 133, 83, 195, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var stream = await _snappyCompression.CompressToStreamAsync(input, CompressionLevel.Fastest);

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
            var expected = new byte[] { 255, 6, 0, 0, 115, 78, 97, 80, 112, 89, 1, 17, 0, 0, 130, 133, 83, 195, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var stream = await _snappyCompression.CompressToStreamAsync(rawStream, CompressionLevel.Fastest);

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
            var expected = new byte[] { 255, 6, 0, 0, 115, 78, 97, 80, 112, 89, 1, 17, 0, 0, 130, 133, 83, 195, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var stream = await _snappyCompression.CompressToStreamAsync(rawStream, leaveOpen: true, CompressionLevel.Fastest);

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
            var expected = new byte[] { 255, 6, 0, 0, 115, 78, 97, 80, 112, 89, 1, 17, 0, 0, 130, 133, 83, 195, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var actual = _snappyCompression.Compress(input);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Compress_BytesInput_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var expected = new byte[] { 255, 6, 0, 0, 115, 78, 97, 80, 112, 89, 1, 17, 0, 0, 130, 133, 83, 195, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var actual = _snappyCompression.Compress(input);

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
            var expected = new byte[] { 255, 6, 0, 0, 115, 78, 97, 80, 112, 89, 1, 17, 0, 0, 130, 133, 83, 195, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var actual = _snappyCompression.Compress(stream);

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
            var expected = new byte[] { 255, 6, 0, 0, 115, 78, 97, 80, 112, 89, 1, 17, 0, 0, 130, 133, 83, 195, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var actual = _snappyCompression.Compress(stream, leaveOpen: true);

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
            var expected = new byte[] { 255, 6, 0, 0, 115, 78, 97, 80, 112, 89, 1, 17, 0, 0, 130, 133, 83, 195, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var actual = _snappyCompression.Compress(input, CompressionLevel.Fastest);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Compress_BytesInput_WithLevel_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var expected = new byte[] { 255, 6, 0, 0, 115, 78, 97, 80, 112, 89, 1, 17, 0, 0, 130, 133, 83, 195, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var actual = _snappyCompression.Compress(input, CompressionLevel.Fastest);

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
            var expected = new byte[] { 255, 6, 0, 0, 115, 78, 97, 80, 112, 89, 1, 17, 0, 0, 130, 133, 83, 195, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var actual = _snappyCompression.Compress(stream, CompressionLevel.Fastest);

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
            var expected = new byte[] { 255, 6, 0, 0, 115, 78, 97, 80, 112, 89, 1, 17, 0, 0, 130, 133, 83, 195, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var actual = _snappyCompression.Compress(stream, leaveOpen: true, CompressionLevel.Fastest);

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
            var expected = new byte[] { 255, 6, 0, 0, 115, 78, 97, 80, 112, 89, 1, 17, 0, 0, 130, 133, 83, 195, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var stream = _snappyCompression.CompressToStream(input);

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
            var expected = new byte[] { 255, 6, 0, 0, 115, 78, 97, 80, 112, 89, 1, 17, 0, 0, 130, 133, 83, 195, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var stream = _snappyCompression.CompressToStream(input);

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
            var expected = new byte[] { 255, 6, 0, 0, 115, 78, 97, 80, 112, 89, 1, 17, 0, 0, 130, 133, 83, 195, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var stream = _snappyCompression.CompressToStream(rawStream);

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
            var expected = new byte[] { 255, 6, 0, 0, 115, 78, 97, 80, 112, 89, 1, 17, 0, 0, 130, 133, 83, 195, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var stream = _snappyCompression.CompressToStream(rawStream, leaveOpen: true);

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
            var expected = new byte[] { 255, 6, 0, 0, 115, 78, 97, 80, 112, 89, 1, 17, 0, 0, 130, 133, 83, 195, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var stream = _snappyCompression.CompressToStream(input, CompressionLevel.Fastest);

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
            var expected = new byte[] { 255, 6, 0, 0, 115, 78, 97, 80, 112, 89, 1, 17, 0, 0, 130, 133, 83, 195, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var stream = _snappyCompression.CompressToStream(input, CompressionLevel.Fastest);

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
            var expected = new byte[] { 255, 6, 0, 0, 115, 78, 97, 80, 112, 89, 1, 17, 0, 0, 130, 133, 83, 195, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var stream = _snappyCompression.CompressToStream(rawStream, CompressionLevel.Fastest);

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
            var expected = new byte[] { 255, 6, 0, 0, 115, 78, 97, 80, 112, 89, 1, 17, 0, 0, 130, 133, 83, 195, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };

            // Act
            var stream = _snappyCompression.CompressToStream(rawStream, leaveOpen: true, CompressionLevel.Fastest);

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