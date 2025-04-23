namespace Ogu.Compressions.Tests.BrotliCompression
{
    public partial class BrotliCompressionTests
    {
        [Fact]
        public async Task DecompressAsync_BytesInput_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 11, 6, 128, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33, 3 };
            var expected = "Hello, World!"u8.ToArray();

            // Act
            var actual = await _brotliCompression.DecompressAsync(input);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task DecompressAsync_StreamInput_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 11, 6, 128, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33, 3 };
            var stream = new MemoryStream(input);
            var expected = "Hello, World!"u8.ToArray();

            // Act
            var actual = await _brotliCompression.DecompressAsync(stream);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task DecompressAsync_StreamInput_WithLeaveOpenTrue_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 11, 6, 128, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33, 3 };
            var stream = new MemoryStream(input);
            var expected = "Hello, World!"u8.ToArray();

            // Act
            var actual = await _brotliCompression.DecompressAsync(stream, true);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
            Assert.Equal(17, stream.Length);

            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task DecompressToStreamAsync_BytesInput_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 11, 6, 128, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33, 3 };
            var expected = "Hello, World!"u8.ToArray();

            // Act
            var stream = await _brotliCompression.DecompressToStreamAsync(input);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);

            var actual = ((MemoryStream)stream).ToArray();

            Assert.Equal(expected, actual);

            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task DecompressToStreamAsync_StreamInput_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 11, 6, 128, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33, 3 };
            var rawStream = new MemoryStream(input);
            var expected = "Hello, World!"u8.ToArray();

            // Act
            var stream = await _brotliCompression.DecompressToStreamAsync(rawStream);

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
        public async Task DecompressToStreamAsync_StreamInput_WithLeaveOpenTrue_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 11, 6, 128, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33, 3 };
            var rawStream = new MemoryStream(input);
            var expected = "Hello, World!"u8.ToArray();

            // Act
            var stream = await _brotliCompression.DecompressToStreamAsync(rawStream, true);

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
        public async Task DecompressToStreamAsync_HttpContentInput_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var compressed = await _brotliCompression.CompressToStreamAsync(input);
            var httpContent = new StreamContent(compressed);

            // Act
            var stream = await _brotliCompression.DecompressToStreamAsync(httpContent);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);
            Assert.Throws<ObjectDisposedException>(() => compressed.Length);
            await Assert.ThrowsAsync<ObjectDisposedException>(async () =>
            {
                var streamLength = (await httpContent.ReadAsStreamAsync()).Length;
            });

            var actual = ((MemoryStream)stream).ToArray();

            Assert.Equal(input, actual);

            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task DecompressToStreamAsync_HttpContentInput_WithLeaveOpenTrue_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 11, 6, 128, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33, 3 };
            var rawStream = new MemoryStream(input);
            var httpContent = new StreamContent(rawStream);
            var expected = "Hello, World!"u8.ToArray();

            // Act
            var stream = await _brotliCompression.DecompressToStreamAsync(httpContent, leaveOpen: true);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);
            Assert.Equal(17, rawStream.Length);

            var actual = ((MemoryStream)stream).ToArray();

            Assert.Equal(expected, actual);

            await stream.DisposeAsync();
            httpContent.Dispose();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
            Assert.Throws<ObjectDisposedException>(() => rawStream.Length);
            await Assert.ThrowsAsync<ObjectDisposedException>(async () =>
            {
                var streamLength = (await httpContent.ReadAsStreamAsync()).Length;
            });
        }

        [Fact]
        public void Decompress_BytesInput_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 11, 6, 128, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33, 3 };
            var expected = "Hello, World!"u8.ToArray();

            // Act
            var actual = _brotliCompression.Decompress(input);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Decompress_StreamInput_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 11, 6, 128, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33, 3 };
            var stream = new MemoryStream(input);
            var expected = "Hello, World!"u8.ToArray();

            // Act
            var actual = _brotliCompression.Decompress(stream);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public void Decompress_StreamInput_WithLeaveOpenTrue_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 11, 6, 128, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33, 3 };
            var stream = new MemoryStream(input);
            var expected = "Hello, World!"u8.ToArray();

            // Act
            var actual = _brotliCompression.Decompress(stream, true);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
            Assert.Equal(17, stream.Length);

            stream.Dispose();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }
    }
}