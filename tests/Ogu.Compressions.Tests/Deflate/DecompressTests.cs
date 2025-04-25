namespace Ogu.Compressions.Tests.Deflate
{
    public partial class DeflateCompressionTests
    {
        [Fact]
        public async Task DecompressAsync_BytesInput_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0 };
            var expected = "Hello, World!"u8.ToArray();

            // Act
            var actual = await _deflateCompression.DecompressAsync(input);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task DecompressAsync_StreamInput_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0 };
            var stream = new MemoryStream(input);
            var expected = "Hello, World!"u8.ToArray();

            // Act
            var actual = await _deflateCompression.DecompressAsync(stream);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task DecompressAsync_StreamInput_WithLeaveOpenTrue_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0 };
            var stream = new MemoryStream(input);
            var expected = "Hello, World!"u8.ToArray();
            const bool leaveOpen = true;

            // Act
            var actual = await _deflateCompression.DecompressAsync(stream, leaveOpen);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
            Assert.Equal(15, stream.Length);

            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task DecompressToStreamAsync_BytesInput_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0 };
            var expected = "Hello, World!"u8.ToArray();

            // Act
            var stream = await _deflateCompression.DecompressToStreamAsync(input);

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
            var input = new byte[] { 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0 };
            var rawStream = new MemoryStream(input);
            var expected = "Hello, World!"u8.ToArray();

            // Act
            var stream = await _deflateCompression.DecompressToStreamAsync(rawStream);

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
            var input = new byte[] { 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0 };
            var rawStream = new MemoryStream(input);
            var expected = "Hello, World!"u8.ToArray();
            const bool leaveOpen = true;

            // Act
            var stream = await _deflateCompression.DecompressToStreamAsync(rawStream, leaveOpen);

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
            var input = new byte[] { 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0 };
            var rawStream = new MemoryStream(input);
            var httpContent = new StreamContent(rawStream);
            var expected = "Hello, World!"u8.ToArray();

            // Act
            var stream = await _deflateCompression.DecompressToStreamAsync(httpContent);

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
            var input = new byte[] { 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0 };
            var rawStream = new MemoryStream(input);
            var httpContent = new StreamContent(rawStream);
            var expected = "Hello, World!"u8.ToArray();
            const bool leaveOpen = true;

            // Act
            var stream = await _deflateCompression.DecompressToStreamAsync(httpContent, leaveOpen);

            // Assert
            Assert.NotNull(stream);
            Assert.IsType<MemoryStream>(stream);
            Assert.Equal(15, rawStream.Length);

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
            var input = new byte[] { 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0 };
            var expected = "Hello, World!"u8.ToArray();

            // Act
            var actual = _deflateCompression.Decompress(input);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Decompress_StreamInput_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0 };
            var stream = new MemoryStream(input);
            var expected = "Hello, World!"u8.ToArray();

            // Act
            var actual = _deflateCompression.Decompress(stream);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public void Decompress_StreamInput_WithLeaveOpenTrue_ReturnsDecompressedBytes()
        {
            // Arrange
            var input = new byte[] { 243, 72, 205, 201, 201, 215, 81, 8, 207, 47, 202, 73, 81, 4, 0 };
            var stream = new MemoryStream(input);
            var expected = "Hello, World!"u8.ToArray();
            const bool leaveOpen = true;

            // Act
            var actual = _deflateCompression.Decompress(stream, leaveOpen);

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(expected, actual);
            Assert.Equal(15, stream.Length);

            stream.Dispose();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }
    }
}