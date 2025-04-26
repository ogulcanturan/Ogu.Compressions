using Ogu.Compressions.Abstractions;
using MemoryStream = System.IO.MemoryStream;

namespace Ogu.Compressions.Tests.Abstractions
{
    public class StreamExtensionsTests
    {
        [Fact]
        public void ReadAllBytes_MemoryStreamInput_ReturnsBytes()
        {
            // Arrange
            var expected = "Hello, World!"u8.ToArray();
            var input = new MemoryStream(expected);

            // Act
            var actual = input.ReadAllBytes(leaveOpen: false);

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(expected, actual);

            Assert.Throws<ObjectDisposedException>(() => input.Length);
        }

        [Fact]
        public void ReadAllBytes_MemoryStreamInput_WithLeaveOpenTrue_ReturnsBytes()
        {
            // Arrange
            var expected = "Hello, World!"u8.ToArray();
            var input = new MemoryStream(expected);

            // Act
            var actual = input.ReadAllBytes(leaveOpen: true);

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
            Assert.Equal(13, input.Length);

            input.Dispose();

            Assert.Throws<ObjectDisposedException>(() => input.Length);
        }


        [Fact]
        public async Task ReadAllBytesAsync_MemoryStreamInput_ReturnsBytes()
        {
            // Arrange
            var expected = "Hello, World!"u8.ToArray();
            var input = new MemoryStream(expected);

            // Act
            var actual = await input.ReadAllBytesAsync(leaveOpen: false);

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(expected, actual);

            Assert.Throws<ObjectDisposedException>(() => input.Length);
        }

        [Fact]
        public async Task ReadAllBytesAsync_MemoryStreamInput_WithLeaveOpenTrue_ReturnsBytes()
        {
            // Arrange
            var expected = "Hello, World!"u8.ToArray();
            var input = new MemoryStream(expected);

            // Act
            var actual = await input.ReadAllBytesAsync(leaveOpen: true);

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(expected, actual);
            Assert.Equal(13, input.Length);

            await input.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => input.Length);
        }

        [Fact]
        public void HasDisposed_DisposedStream_ReturnsTrue()
        {
            // Arrange
            var stream = new MemoryStream();
            stream.Dispose();

            // Act
            var hasDisposed = stream.HasDisposed();

            // Assert
            Assert.True(hasDisposed);
        }

        [Fact]
        public void HasDisposed_GivenActiveStream_ReturnsFalse()
        {
            // Arrange
            var stream = new MemoryStream();

            // Act
            var hasDisposed = stream.HasDisposed();

            // Assert
            Assert.False(hasDisposed);

            stream.Dispose();
        }

        [Fact]
        public void HasDisposed_NullStream_ThrowsArgumentNullException()
        {
            // Arrange
            MemoryStream? stream = null;

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => stream.HasDisposed());
            Assert.Equal(nameof(stream), exception.ParamName);
        }
    }
}