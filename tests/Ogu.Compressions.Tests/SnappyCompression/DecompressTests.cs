namespace Ogu.Compressions.Tests.SnappyCompression
{
    public partial class SnappyCompressionTests
    {
        [Fact]
        public async Task DecompressAsync_BytesInput_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 255, 6, 0, 0, 115, 78, 97, 80, 112, 89, 1, 17, 0, 0, 130, 133, 83, 195, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };
            var expected = "Hello, World!"u8.ToArray();

            // Act
            var actual = await _snappyCompression.DecompressAsync(input);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task DecompressAsync_StreamInput_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 255, 6, 0, 0, 115, 78, 97, 80, 112, 89, 1, 17, 0, 0, 130, 133, 83, 195, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };
            var stream = new MemoryStream(input);
            var expected = "Hello, World!"u8.ToArray();

            // Act
            var actual = await _snappyCompression.DecompressAsync(stream);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task DecompressAsync_StreamInput_WithLeaveOpenTrue_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 255, 6, 0, 0, 115, 78, 97, 80, 112, 89, 1, 17, 0, 0, 130, 133, 83, 195, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };
            var stream = new MemoryStream(input);
            var expected = "Hello, World!"u8.ToArray();

            // Act
            var actual = await _snappyCompression.DecompressAsync(stream, true);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
            Assert.Equal(31, stream.Length);

            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task DecompressToStreamAsync_BytesInput_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 255, 6, 0, 0, 115, 78, 97, 80, 112, 89, 1, 17, 0, 0, 130, 133, 83, 195, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };
            var expected = "Hello, World!"u8.ToArray();

            // Act
            var stream = await _snappyCompression.DecompressToStreamAsync(input);

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
            var input = new byte[] { 255, 6, 0, 0, 115, 78, 97, 80, 112, 89, 1, 17, 0, 0, 130, 133, 83, 195, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };
            var rawStream = new MemoryStream(input);
            var expected = "Hello, World!"u8.ToArray();

            // Act
            var stream = await _snappyCompression.DecompressToStreamAsync(rawStream);

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
            var input = new byte[] { 255, 6, 0, 0, 115, 78, 97, 80, 112, 89, 1, 17, 0, 0, 130, 133, 83, 195, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };
            var rawStream = new MemoryStream(input);
            var expected = "Hello, World!"u8.ToArray();

            // Act
            var stream = await _snappyCompression.DecompressToStreamAsync(rawStream, true);

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
            var input = new byte[] { 255, 6, 0, 0, 115, 78, 97, 80, 112, 89, 1, 17, 0, 0, 130, 133, 83, 195, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };
            var rawStream = new MemoryStream(input);
            var httpContent = new StreamContent(rawStream);
            var expected = "Hello, World!"u8.ToArray();

            // Act
            var stream = await _snappyCompression.DecompressToStreamAsync(httpContent);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);
            Assert.Throws<ObjectDisposedException>(() => rawStream.Length);
            await Assert.ThrowsAsync<ObjectDisposedException>(async () =>
            {
                var streamLength = (await httpContent.ReadAsStreamAsync()).Length;
            });

            var actual = ((MemoryStream)stream).ToArray();

            Assert.Equal(expected, actual);

            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task DecompressToStreamAsync_HttpContentInput_WithLeaveOpenTrue_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 255, 6, 0, 0, 115, 78, 97, 80, 112, 89, 1, 17, 0, 0, 130, 133, 83, 195, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };
            var rawStream = new MemoryStream(input);
            var httpContent = new StreamContent(rawStream);
            var expected = "Hello, World!"u8.ToArray();

            // Act
            var stream = await _snappyCompression.DecompressToStreamAsync(httpContent, leaveOpen: true);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);
            Assert.Equal(31, rawStream.Length);

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
            var input = new byte[] { 255, 6, 0, 0, 115, 78, 97, 80, 112, 89, 1, 17, 0, 0, 130, 133, 83, 195, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };
            var expected = "Hello, World!"u8.ToArray();

            // Act
            var actual = _snappyCompression.Decompress(input);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Decompress_StreamInput_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 255, 6, 0, 0, 115, 78, 97, 80, 112, 89, 1, 17, 0, 0, 130, 133, 83, 195, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };
            var stream = new MemoryStream(input);
            var expected = "Hello, World!"u8.ToArray();

            // Act
            var actual = _snappyCompression.Decompress(stream);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public void Decompress_StreamInput_WithLeaveOpenTrue_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 255, 6, 0, 0, 115, 78, 97, 80, 112, 89, 1, 17, 0, 0, 130, 133, 83, 195, 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };
            var stream = new MemoryStream(input);
            var expected = "Hello, World!"u8.ToArray();

            // Act
            var actual = _snappyCompression.Decompress(stream, true);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
            Assert.Equal(31, stream.Length);

            stream.Dispose();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }
    }
}