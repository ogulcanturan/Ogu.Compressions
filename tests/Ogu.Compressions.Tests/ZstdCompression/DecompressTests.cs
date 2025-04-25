namespace Ogu.Compressions.Tests.ZstdCompression
{
    public partial class ZstdCompressionTests
    {
        [Fact]
        public async Task DecompressAsync_BytesInput_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 40, 181, 47, 253, 0, 72, 105, 0, 0, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };
            var expected = "Hello, World!"u8.ToArray();

            // Act
            var actual = await _zstdCompression.DecompressAsync(input);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task DecompressAsync_StreamInput_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 40, 181, 47, 253, 0, 72, 105, 0, 0, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };
            var stream = new MemoryStream(input);
            var expected = "Hello, World!"u8.ToArray();

            // Act
            var actual = await _zstdCompression.DecompressAsync(stream);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task DecompressAsync_StreamInput_WithLeaveOpenTrue_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 40, 181, 47, 253, 0, 72, 105, 0, 0, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };
            var stream = new MemoryStream(input);
            var expected = "Hello, World!"u8.ToArray();
            const bool leaveOpen = true;

            // Act
            var actual = await _zstdCompression.DecompressAsync(stream, leaveOpen);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
            Assert.Equal(22, stream.Length);

            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task DecompressToStreamAsync_BytesInput_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 40, 181, 47, 253, 0, 72, 105, 0, 0, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };
            var expected = "Hello, World!"u8.ToArray();

            // Act
            var stream = await _zstdCompression.DecompressToStreamAsync(input);

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
            var input = new byte[] { 40, 181, 47, 253, 0, 72, 105, 0, 0, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };
            var rawStream = new MemoryStream(input);
            var expected = "Hello, World!"u8.ToArray();

            // Act
            var stream = await _zstdCompression.DecompressToStreamAsync(rawStream);

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
            var input = new byte[] { 40, 181, 47, 253, 0, 72, 105, 0, 0, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };
            var rawStream = new MemoryStream(input);
            var expected = "Hello, World!"u8.ToArray();
            const bool leaveOpen = true;

            // Act
            var stream = await _zstdCompression.DecompressToStreamAsync(rawStream, leaveOpen);

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
            var input = new byte[] { 40, 181, 47, 253, 0, 72, 105, 0, 0, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };
            var rawStream = new MemoryStream(input);
            var httpContent = new StreamContent(rawStream);
            var expected = "Hello, World!"u8.ToArray();

            // Act
            var stream = await _zstdCompression.DecompressToStreamAsync(httpContent);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);
            Assert.Throws<ObjectDisposedException>(() => rawStream.Length);
            await Assert.ThrowsAsync<ObjectDisposedException>(async () => _ = (await httpContent.ReadAsStreamAsync()).Length);

            var actual = ((MemoryStream)stream).ToArray();

            Assert.Equal(expected, actual);

            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task DecompressToStreamAsync_HttpContentInput_WithLeaveOpenTrue_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 40, 181, 47, 253, 0, 72, 105, 0, 0, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };
            var rawStream = new MemoryStream(input);
            var httpContent = new StreamContent(rawStream);
            var expected = "Hello, World!"u8.ToArray();
            const bool leaveOpen = true;

            // Act
            var stream = await _zstdCompression.DecompressToStreamAsync(httpContent, leaveOpen);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);
            Assert.Equal(22, rawStream.Length);

            var actual = ((MemoryStream)stream).ToArray();

            Assert.Equal(expected, actual);

            await stream.DisposeAsync();
            httpContent.Dispose();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
            Assert.Throws<ObjectDisposedException>(() => rawStream.Length);
            await Assert.ThrowsAsync<ObjectDisposedException>(async () => _ = (await httpContent.ReadAsStreamAsync()).Length);
        }

        [Fact]
        public void Decompress_BytesInput_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 40, 181, 47, 253, 0, 72, 105, 0, 0, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };
            var expected = "Hello, World!"u8.ToArray();

            // Act
            var actual = _zstdCompression.Decompress(input);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Decompress_StreamInput_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 40, 181, 47, 253, 0, 72, 105, 0, 0, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };
            var stream = new MemoryStream(input);
            var expected = "Hello, World!"u8.ToArray();

            // Act
            var actual = _zstdCompression.Decompress(stream);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public void Decompress_StreamInput_WithLeaveOpenTrue_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 40, 181, 47, 253, 0, 72, 105, 0, 0, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };
            var stream = new MemoryStream(input);
            var expected = "Hello, World!"u8.ToArray();
            const bool leaveOpen = true;

            // Act
            var actual = _zstdCompression.Decompress(stream, leaveOpen);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
            Assert.Equal(22, stream.Length);

            stream.Dispose();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }
    }
}