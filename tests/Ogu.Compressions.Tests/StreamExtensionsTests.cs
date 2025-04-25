using Ogu.Compressions.Abstractions;
using MemoryStream = System.IO.MemoryStream;

namespace Ogu.Compressions.Tests
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

    }
}