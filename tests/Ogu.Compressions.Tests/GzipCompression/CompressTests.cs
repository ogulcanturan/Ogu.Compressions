using Microsoft.Extensions.Options;
using System.IO.Compression;
using Ogu.Compressions.Abstractions;

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
#if DEBUG
            var expected = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };
#endif

            // Act
            var actual = await _gzipCompression.CompressAsync(input);

            // Assert
            Assert.NotEmpty(actual);
#if DEBUG
            Assert.Equal(expected, actual);
#endif
        }

        [Fact]
        public async Task CompressAsync_BytesInput_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var expected = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };

            // Act
            var actual = await _gzipCompression.CompressAsync(input);

            // Assert
            Assert.NotEmpty(actual);
#if DEBUG
            Assert.Equal(expected, actual);
#endif
        }

        [Fact]
        public async Task CompressAsync_StreamInput_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);
            var expected = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };

            // Act
            var actual = await _gzipCompression.CompressAsync(stream);

            // Assert
            Assert.NotEmpty(actual);
#if DEBUG
            Assert.Equal(expected, actual);
#endif
            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task CompressAsync_StreamInput_WithLeaveOpenTrue_LeavesStreamOpen()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);
            var expected = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };

            // Act
            var actual = await _gzipCompression.CompressAsync(stream, leaveOpen: true);

            // Assert
            Assert.NotEmpty(actual);
#if DEBUG
            Assert.Equal(expected, actual);
            Assert.Equal(13, stream.Length);
#endif

            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task CompressAsync_StringInput_WithLevel_ReturnsCompressedBytes()
        {
            // Arrange
            const string input = "Hello, World!";
            var expected = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };

            // Act
            var actual = await _gzipCompression.CompressAsync(input, CompressionLevel.Fastest);

            // Assert
            Assert.NotEmpty(actual);
#if DEBUG
            Assert.Equal(expected, actual);
#endif
        }

        [Fact]
        public async Task CompressAsync_BytesInput_WithLevel_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var expected = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };

            // Act
            var actual = await _gzipCompression.CompressAsync(input, CompressionLevel.Fastest);

            // Assert
            Assert.NotEmpty(actual);
#if DEBUG
            Assert.Equal(expected, actual);
#endif
        }

        [Fact]
        public async Task CompressAsync_StreamInput_WithLevel_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);
            var expected = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };

            // Act
            var actual = await _gzipCompression.CompressAsync(stream, CompressionLevel.Fastest);

            // Assert
            Assert.NotEmpty(actual);
#if DEBUG
            Assert.Equal(expected, actual);
#endif
            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task CompressAsync_StreamInput_WithLevelAndLeaveOpenTrue_LeavesStreamOpen()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);
            var expected = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };

            // Act
            var actual = await _gzipCompression.CompressAsync(stream, leaveOpen: true, CompressionLevel.Fastest);

            // Assert
            Assert.NotEmpty(actual);
#if DEBUG
            Assert.Equal(expected, actual);
            Assert.Equal(13, stream.Length);
#endif

            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task CompressToStreamAsync_StringInput_ReturnsCompressedStream()
        {
            // Arrange
            const string input = "Hello, World!";
            var expected = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };

            // Act
            var stream = await _gzipCompression.CompressToStreamAsync(input);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);

            var actual = ((MemoryStream)stream).ToArray();

#if DEBUG
            Assert.Equal(expected, actual);
#endif

            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task CompressToStreamAsync_BytesInput_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var expected = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };

            // Act
            var stream = await _gzipCompression.CompressToStreamAsync(input);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);

            var actual = ((MemoryStream)stream).ToArray();

#if DEBUG
            Assert.Equal(expected, actual);
#endif

            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task CompressToStreamAsync_StreamInput_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var rawStream = new MemoryStream(input);
            var expected = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };

            // Act
            var stream = await _gzipCompression.CompressToStreamAsync(rawStream);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);
            Assert.Throws<ObjectDisposedException>(() => rawStream.Length);

            var actual = ((MemoryStream)stream).ToArray();

#if DEBUG
            Assert.Equal(expected, actual);
#endif

            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task CompressToStreamAsync_StreamInput_WithLeaveOpenTrue_LeavesStreamOpen()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var rawStream = new MemoryStream(input);
            var expected = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };

            // Act
            var stream = await _gzipCompression.CompressToStreamAsync(rawStream, leaveOpen: true);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);
#if DEBUG
            Assert.Equal(33, stream.Length);

            var actual = ((MemoryStream)stream).ToArray();

            Assert.Equal(expected, actual);
#endif

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
            var expected = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };

            // Act
            var stream = await _gzipCompression.CompressToStreamAsync(input, CompressionLevel.Fastest);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);

#if DEBUG
            var actual = ((MemoryStream)stream).ToArray();

            Assert.Equal(expected, actual);
#endif

            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task CompressToStreamAsync_BytesInput_WithLevel_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var expected = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };

            // Act
            var stream = await _gzipCompression.CompressToStreamAsync(input, CompressionLevel.Fastest);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);

#if DEBUG
            var actual = ((MemoryStream)stream).ToArray();

            Assert.Equal(expected, actual);
#endif

            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task CompressToStreamAsync_StreamInput_WithLevel_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var rawStream = new MemoryStream(input);
            var expected = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };

            // Act
            var stream = await _gzipCompression.CompressToStreamAsync(rawStream, CompressionLevel.Fastest);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);
            Assert.Throws<ObjectDisposedException>(() => rawStream.Length);

#if DEBUG
            var actual = ((MemoryStream)stream).ToArray();

            Assert.Equal(expected, actual);
#endif

            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task CompressToStreamAsync_StreamInput_WithLevelAndLeaveOpenTrue_LeavesStreamOpen()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var rawStream = new MemoryStream(input);
            var expected = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };

            // Act
            var stream = await _gzipCompression.CompressToStreamAsync(rawStream, leaveOpen: true, CompressionLevel.Fastest);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);
#if DEBUG
            Assert.Equal(33, stream.Length);

            var actual = ((MemoryStream)stream).ToArray();

            Assert.Equal(expected, actual);
#endif

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
            var expected = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };

            // Act
            var actual = _gzipCompression.Compress(input);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Compress_BytesInput_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var expected = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };

            // Act
            var actual = _gzipCompression.Compress(input);

            // Assert
            Assert.NotEmpty(actual);
#if DEBUG
            Assert.Equal(expected, actual);
#endif
        }

        [Fact]
        public void Compress_StreamInput_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);
            var expected = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };

            // Act
            var actual = _gzipCompression.Compress(stream);

            // Assert
            Assert.NotEmpty(actual);
#if DEBUG
            Assert.Equal(expected, actual);
#endif
            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public void Compress_StreamInput_WithLeaveOpenTrue_LeavesStreamOpen()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);
            var expected = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };

            // Act
            var actual = _gzipCompression.Compress(stream, leaveOpen: true);

            // Assert
            Assert.NotEmpty(actual);
#if DEBUG
            Assert.Equal(expected, actual);
            Assert.Equal(13, stream.Length);
#endif

            stream.Dispose();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public void Compress_StringInput_WithLevel_ReturnsCompressedBytes()
        {
            // Arrange
            const string input = "Hello, World!";
            var expected = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };

            // Act
            var actual = _gzipCompression.Compress(input, CompressionLevel.Fastest);

            // Assert
            Assert.NotEmpty(actual);
#if DEBUG
            Assert.Equal(expected, actual);
#endif
        }

        [Fact]
        public void Compress_BytesInput_WithLevel_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var expected = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };

            // Act
            var actual = _gzipCompression.Compress(input, CompressionLevel.Fastest);

            // Assert
            Assert.NotEmpty(actual);
#if DEBUG
            Assert.Equal(expected, actual);
#endif
        }

        [Fact]
        public void Compress_StreamInput_WithLevel_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);
            var expected = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };

            // Act
            var actual = _gzipCompression.Compress(stream, CompressionLevel.Fastest);

            // Assert
            Assert.NotEmpty(actual);
#if DEBUG
            Assert.Equal(expected, actual);
#endif
            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public void Compress_StreamInput_WithLevelAndLeaveOpenTrue_LeavesStreamOpen()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);
            var expected = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };

            // Act
            var actual = _gzipCompression.Compress(stream, leaveOpen: true, CompressionLevel.Fastest);

            // Assert
            Assert.NotEmpty(actual);
#if DEBUG
            Assert.Equal(expected, actual);
            Assert.Equal(13, stream.Length);
#endif

            stream.Dispose();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public void CompressToStream_StringInput_ReturnsCompressedStream()
        {
            // Arrange
            const string input = "Hello, World!";
            var expected = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };

            // Act
            var stream = _gzipCompression.CompressToStream(input);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);

#if DEBUG
            var actual = ((MemoryStream)stream).ToArray();

            Assert.Equal(expected, actual);
#endif

            stream.Dispose();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public void CompressToStream_BytesInput_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var expected = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };

            // Act
            var stream = _gzipCompression.CompressToStream(input);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);

#if DEBUG
            var actual = ((MemoryStream)stream).ToArray();

            Assert.Equal(expected, actual);
#endif

            stream.Dispose();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public void CompressToStream_StreamInput_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var rawStream = new MemoryStream(input);
            var expected = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };

            // Act
            var stream = _gzipCompression.CompressToStream(rawStream);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);
            Assert.Throws<ObjectDisposedException>(() => rawStream.Length);

            var actual = ((MemoryStream)stream).ToArray();

#if DEBUG
            Assert.Equal(expected, actual);
#endif

            stream.Dispose();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public void CompressToStream_StreamInput_WithLeaveOpenTrue_LeavesStreamOpen()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var rawStream = new MemoryStream(input);
            var expected = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };

            // Act
            var stream = _gzipCompression.CompressToStream(rawStream, leaveOpen: true);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);
#if DEBUG
            Assert.Equal(33, stream.Length);

            var actual = ((MemoryStream)stream).ToArray();

            Assert.Equal(expected, actual);
#endif

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
            var expected = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };

            // Act
            var stream = _gzipCompression.CompressToStream(input, CompressionLevel.Fastest);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);

#if DEBUG
            var actual = ((MemoryStream)stream).ToArray();

            Assert.Equal(expected, actual);
#endif

            stream.Dispose();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public void CompressToStream_BytesInput_WithLevel_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var expected = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };

            // Act
            var stream = _gzipCompression.CompressToStream(input, CompressionLevel.Fastest);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);

#if DEBUG
            var actual = ((MemoryStream)stream).ToArray();

            Assert.Equal(expected, actual);
#endif

            stream.Dispose();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public void CompressToStream_StreamInput_WithLevel_ReturnsCompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var rawStream = new MemoryStream(input);
            var expected = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };

            // Act
            var stream = _gzipCompression.CompressToStream(rawStream, CompressionLevel.Fastest);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);
            Assert.Throws<ObjectDisposedException>(() => rawStream.Length);

#if DEBUG
            var actual = ((MemoryStream)stream).ToArray();

            Assert.Equal(expected, actual);
#endif

            stream.Dispose();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public void CompressToStream_StreamInput_WithLevelAndLeaveOpenTrue_LeavesStreamOpen()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var rawStream = new MemoryStream(input);
            var expected = new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 10, 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0, 208, 195, 74, 236, 13, 0, 0, 0 };

            // Act
            var stream = _gzipCompression.CompressToStream(rawStream, leaveOpen: true, CompressionLevel.Fastest);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);
#if DEBUG
            Assert.Equal(33, stream.Length);

            var actual = ((MemoryStream)stream).ToArray();

            Assert.Equal(expected, actual);
#endif

            rawStream.Dispose();
            stream.Dispose();

            Assert.Throws<ObjectDisposedException>(() => rawStream.Length);
            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }
    }
}