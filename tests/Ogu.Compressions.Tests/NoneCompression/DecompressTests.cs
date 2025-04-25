namespace Ogu.Compressions.Tests.NoneCompression
{
    public partial class NoneCompressionTests
    {
        [Fact]
        public async Task DecompressAsync_BytesInput_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33};
            var expected = "Hello, World!"u8.ToArray();

            // Act
            var actual = await _noneCompression.DecompressAsync(input);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task DecompressAsync_StreamInput_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33};
            var stream = new MemoryStream(input);
            var expected = "Hello, World!"u8.ToArray();

            // Act
            var actual = await _noneCompression.DecompressAsync(stream);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task DecompressAsync_StreamInput_WithLeaveOpenTrue_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33};
            var stream = new MemoryStream(input);
            var expected = "Hello, World!"u8.ToArray();
            const bool leaveOpen = true;

            // Act
            var actual = await _noneCompression.DecompressAsync(stream, leaveOpen);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
            Assert.Equal(13, stream.Length);

            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task DecompressToStreamAsync_BytesInput_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33};
            var expected = "Hello, World!"u8.ToArray();

            // Act
            var stream = await _noneCompression.DecompressToStreamAsync(input);

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
            var input = new byte[] { 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33};
            var rawStream = new MemoryStream(input);
            var expected = "Hello, World!"u8.ToArray();

            // Act
            var stream = await _noneCompression.DecompressToStreamAsync(rawStream);

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
        public async Task DecompressToStreamAsync_StreamInput_WithLeaveOpenTrue_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33};
            var rawStream = new MemoryStream(input);
            var expected = "Hello, World!"u8.ToArray();
            const bool leaveOpen = true;

            // Act
            var stream = await _noneCompression.DecompressToStreamAsync(rawStream, leaveOpen);

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
            var input = new byte[] { 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33};
            var rawStream = new MemoryStream(input);
            var httpContent = new StreamContent(rawStream);

            // Act
            var stream = await _noneCompression.DecompressToStreamAsync(httpContent);

            // Assert
            Assert.NotNull(stream);
            Assert.NotSame(rawStream, stream);

            var actualBytes = new byte[input.Length];
            var bytesRead = await stream.ReadAsync(actualBytes);

            Assert.Equal(input.Length, bytesRead);
            Assert.Equal(input, actualBytes);

            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => rawStream.Length);
            Assert.Throws<ObjectDisposedException>(() => stream.Length);
            await Assert.ThrowsAsync<ObjectDisposedException>(async () => _ = (await httpContent.ReadAsStreamAsync()).Length);
        }

        [Fact]
        public async Task DecompressToStreamAsync_HttpContentInput_WithLeaveOpenTrue_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33 };
            var rawStream = new MemoryStream(input);
            var httpContent = new StreamContent(rawStream);
            const bool leaveOpen = true;

            // Act
            var stream = await _noneCompression.DecompressToStreamAsync(httpContent, leaveOpen);

            // Assert
            Assert.NotNull(stream);
            Assert.NotSame(rawStream, stream);

            var actualBytes = new byte[input.Length];
            var bytesRead = await stream.ReadAsync(actualBytes);

            Assert.Equal(input.Length, bytesRead);
            Assert.Equal(input, actualBytes);

            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => rawStream.Length);
            Assert.Throws<ObjectDisposedException>(() => stream.Length);
            await Assert.ThrowsAsync<ObjectDisposedException>(async () => _ = (await httpContent.ReadAsStreamAsync()).Length);
        }

        [Fact]
        public void Decompress_BytesInput_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33};
            var expected = "Hello, World!"u8.ToArray();

            // Act
            var actual = _noneCompression.Decompress(input);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Decompress_StreamInput_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33};
            var stream = new MemoryStream(input);
            var expected = "Hello, World!"u8.ToArray();

            // Act
            var actual = _noneCompression.Decompress(stream);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public void Decompress_StreamInput_WithLeaveOpenTrue_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 72, 101, 108, 108, 111, 44, 32, 87, 111, 114, 108, 100, 33};
            var stream = new MemoryStream(input);
            var expected = "Hello, World!"u8.ToArray();
            const bool leaveOpen = true;

            // Act
            var actual = _noneCompression.Decompress(stream, leaveOpen);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
            Assert.Equal(13, stream.Length);

            stream.Dispose();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }
    }
}