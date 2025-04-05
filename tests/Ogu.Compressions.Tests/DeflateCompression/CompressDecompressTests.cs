using System.IO.Compression;
using System.Text;

namespace Ogu.Compressions.Tests.DeflateCompression
{
    public partial class DeflateCompressionTests
    {
        [Fact]
        public async Task CompressAsyncAndDecompressAsync_StringInput_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            const string input = "Hello, World!";

            // Act
            var compressed = await _deflateCompression.CompressAsync(input);
            var decompressed = await _deflateCompression.DecompressAsync(compressed);

            // Assert
            Assert.Equal(Encoding.UTF8.GetBytes(input), decompressed);
        }

        [Fact]
        public async Task CompressAsyncAndDecompressAsync_BytesInput_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();

            // Act
            var compressed = await _deflateCompression.CompressAsync(input);
            var decompressed = await _deflateCompression.DecompressAsync(compressed);

            // Assert
            Assert.Equal(input, decompressed);
        }

        [Fact]
        public async Task CompressAsyncAndDecompressAsync_StreamInput_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);

            // Act
            var compressed = await _deflateCompression.CompressAsync(stream);
            var decompressed = await _deflateCompression.DecompressAsync(compressed);

            // Assert
            Assert.Equal(input, decompressed);
        }

        [Fact]
        public async Task CompressAsyncAndDecompressAsync_StreamInput_WithLeaveOpenTrue_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);

            // Act
            var compressed = await _deflateCompression.CompressAsync(stream, leaveOpen: true);
            var decompressed = await _deflateCompression.DecompressAsync(compressed);

            // Assert
            Assert.Equal(input, decompressed);

            await stream.DisposeAsync();
        }

        [Fact]
        public async Task CompressAsyncAndDecompressAsync_StringInput_WithLevel_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            const string input = "Hello, World!";
            var inputBytes = Encoding.UTF8.GetBytes(input);

            // Act
            var optimalCompressed = await _deflateCompression.CompressAsync(input, CompressionLevel.Optimal);
            var fastestCompressed = await _deflateCompression.CompressAsync(input, CompressionLevel.Fastest);
            var noCompressionCompressed = await _deflateCompression.CompressAsync(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = await _deflateCompression.CompressAsync(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = await _deflateCompression.DecompressAsync(optimalCompressed);
            var fastestDecompressed = await _deflateCompression.DecompressAsync(fastestCompressed);
            var noCompressionDecompressed = await _deflateCompression.DecompressAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _deflateCompression.DecompressAsync(smallestSizeCompressed);

            // Assert
            Assert.Equal(inputBytes, optimalDecompressed);
            Assert.Equal(inputBytes, fastestDecompressed);
            Assert.Equal(inputBytes, noCompressionDecompressed);
            Assert.Equal(inputBytes, smallestSizeDecompressed);
        }

        [Fact]
        public async Task CompressAsyncAndDecompressAsync_BytesInput_WithLevel_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();

            // Act
            var optimalCompressed = await _deflateCompression.CompressAsync(input, CompressionLevel.Optimal);
            var fastestCompressed = await _deflateCompression.CompressAsync(input, CompressionLevel.Fastest);
            var noCompressionCompressed = await _deflateCompression.CompressAsync(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = await _deflateCompression.CompressAsync(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = await _deflateCompression.DecompressAsync(optimalCompressed);
            var fastestDecompressed = await _deflateCompression.DecompressAsync(fastestCompressed);
            var noCompressionDecompressed = await _deflateCompression.DecompressAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _deflateCompression.DecompressAsync(smallestSizeCompressed);

            // Assert
            Assert.Equal(input, optimalDecompressed);
            Assert.Equal(input, fastestDecompressed);
            Assert.Equal(input, noCompressionDecompressed);
            Assert.Equal(input, smallestSizeDecompressed);
        }

        [Fact]
        public async Task CompressAsyncAndDecompressAsync_StreamInput_WithLevel_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);

            // Act
            var optimalCompressed = await _deflateCompression.CompressAsync(stream, leaveOpen: true, CompressionLevel.Optimal);
            var fastestCompressed = await _deflateCompression.CompressAsync(stream, leaveOpen: true, CompressionLevel.Fastest);
            var noCompressionCompressed = await _deflateCompression.CompressAsync(stream, leaveOpen: true, CompressionLevel.NoCompression);
            var smallestSizeCompressed = await _deflateCompression.CompressAsync(stream, leaveOpen: false, CompressionLevel.SmallestSize);

            var optimalDecompressed = await _deflateCompression.DecompressAsync(optimalCompressed);
            var fastestDecompressed = await _deflateCompression.DecompressAsync(fastestCompressed);
            var noCompressionDecompressed = await _deflateCompression.DecompressAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _deflateCompression.DecompressAsync(smallestSizeCompressed);

            // Assert
            Assert.Equal(input, optimalDecompressed);
            Assert.Equal(input, fastestDecompressed);
            Assert.Equal(input, noCompressionDecompressed);
            Assert.Equal(input, smallestSizeDecompressed);
        }

        [Fact]
        public async Task CompressAsyncAndDecompressAsync_StreamInput_WithLevelAndLeaveOpenTrue_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);

            // Act
            var optimalCompressed = await _deflateCompression.CompressAsync(stream, leaveOpen: true, CompressionLevel.Optimal);
            var fastestCompressed = await _deflateCompression.CompressAsync(stream, leaveOpen: true, CompressionLevel.Fastest);
            var noCompressionCompressed = await _deflateCompression.CompressAsync(stream, leaveOpen: true, CompressionLevel.NoCompression);
            var smallestSizeCompressed = await _deflateCompression.CompressAsync(stream, leaveOpen: true, CompressionLevel.SmallestSize);

            var optimalDecompressed = await _deflateCompression.DecompressAsync(optimalCompressed);
            var fastestDecompressed = await _deflateCompression.DecompressAsync(fastestCompressed);
            var noCompressionDecompressed = await _deflateCompression.DecompressAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _deflateCompression.DecompressAsync(smallestSizeCompressed);

            // Assert
            Assert.Equal(input, optimalDecompressed);
            Assert.Equal(input, fastestDecompressed);
            Assert.Equal(input, noCompressionDecompressed);
            Assert.Equal(input, smallestSizeDecompressed);

            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task CompressToStreamAsyncAndDecompressAsync_StringInput_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            const string input = "Hello, World!";

            // Act
            var compressed = await _deflateCompression.CompressToStreamAsync(input);
            var decompressed = await _deflateCompression.DecompressAsync(compressed);

            // Assert
            Assert.Equal(Encoding.UTF8.GetBytes(input), decompressed);
        }

        [Fact]
        public async Task CompressToStreamAsyncAndDecompressAsync_BytesInput_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();

            // Act
            var compressed = await _deflateCompression.CompressToStreamAsync(input);
            var decompressed = await _deflateCompression.DecompressAsync(compressed);

            // Assert
            Assert.Equal(input, decompressed);
        }

        [Fact]
        public async Task CompressToStreamAsyncAndDecompressAsync_StreamInput_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);

            // Act
            var compressed = await _deflateCompression.CompressToStreamAsync(stream);
            var decompressed = await _deflateCompression.DecompressAsync(compressed);

            // Assert
            Assert.Equal(input, decompressed);
        }

        [Fact]
        public async Task CompressToStreamAsyncAndDecompressAsync_StreamInput_WithLeaveOpenTrue_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);

            // Act
            var compressed = await _deflateCompression.CompressToStreamAsync(stream, leaveOpen: true);
            var decompressed = await _deflateCompression.DecompressAsync(compressed);

            // Assert
            Assert.Equal(input, decompressed);

            await stream.DisposeAsync();
        }

        [Fact]
        public async Task CompressToStreamAsyncAndDecompressAsync_StringInput_WithLevel_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            const string input = "Hello, World!";
            var inputBytes = Encoding.UTF8.GetBytes(input);

            // Act
            var optimalCompressed = await _deflateCompression.CompressToStreamAsync(input, CompressionLevel.Optimal);
            var fastestCompressed = await _deflateCompression.CompressToStreamAsync(input, CompressionLevel.Fastest);
            var noCompressionCompressed = await _deflateCompression.CompressToStreamAsync(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = await _deflateCompression.CompressToStreamAsync(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = await _deflateCompression.DecompressAsync(optimalCompressed);
            var fastestDecompressed = await _deflateCompression.DecompressAsync(fastestCompressed);
            var noCompressionDecompressed = await _deflateCompression.DecompressAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _deflateCompression.DecompressAsync(smallestSizeCompressed);

            // Assert
            Assert.Equal(inputBytes, optimalDecompressed);
            Assert.Equal(inputBytes, fastestDecompressed);
            Assert.Equal(inputBytes, noCompressionDecompressed);
            Assert.Equal(inputBytes, smallestSizeDecompressed);
        }

        [Fact]
        public async Task CompressToStreamAsyncAndDecompressAsync_BytesInput_WithLevel_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();

            // Act
            var optimalCompressed = await _deflateCompression.CompressToStreamAsync(input, CompressionLevel.Optimal);
            var fastestCompressed = await _deflateCompression.CompressToStreamAsync(input, CompressionLevel.Fastest);
            var noCompressionCompressed = await _deflateCompression.CompressToStreamAsync(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = await _deflateCompression.CompressToStreamAsync(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = await _deflateCompression.DecompressAsync(optimalCompressed);
            var fastestDecompressed = await _deflateCompression.DecompressAsync(fastestCompressed);
            var noCompressionDecompressed = await _deflateCompression.DecompressAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _deflateCompression.DecompressAsync(smallestSizeCompressed);

            // Assert
            Assert.Equal(input, optimalDecompressed);
            Assert.Equal(input, fastestDecompressed);
            Assert.Equal(input, noCompressionDecompressed);
            Assert.Equal(input, smallestSizeDecompressed);
        }

        [Fact]
        public async Task CompressToStreamAsyncAndDecompressAsync_StreamInput_WithLevel_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);

            // Act
            var optimalCompressed = await _deflateCompression.CompressToStreamAsync(stream, leaveOpen: true, CompressionLevel.Optimal);
            var fastestCompressed = await _deflateCompression.CompressToStreamAsync(stream, leaveOpen: true, CompressionLevel.Fastest);
            var noCompressionCompressed = await _deflateCompression.CompressToStreamAsync(stream, leaveOpen: true, CompressionLevel.NoCompression);
            var smallestSizeCompressed = await _deflateCompression.CompressToStreamAsync(stream, leaveOpen: false, CompressionLevel.SmallestSize);

            var optimalDecompressed = await _deflateCompression.DecompressAsync(optimalCompressed);
            var fastestDecompressed = await _deflateCompression.DecompressAsync(fastestCompressed);
            var noCompressionDecompressed = await _deflateCompression.DecompressAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _deflateCompression.DecompressAsync(smallestSizeCompressed);

            // Assert
            Assert.Equal(input, optimalDecompressed);
            Assert.Equal(input, fastestDecompressed);
            Assert.Equal(input, noCompressionDecompressed);
            Assert.Equal(input, smallestSizeDecompressed);
        }

        [Fact]
        public async Task CompressToStreamAsyncAndDecompressAsync_StreamInput_WithLevelAndLeaveOpenTrue_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);

            // Act
            var optimalCompressed = await _deflateCompression.CompressToStreamAsync(stream, leaveOpen: true, CompressionLevel.Optimal);
            var fastestCompressed = await _deflateCompression.CompressToStreamAsync(stream, leaveOpen: true, CompressionLevel.Fastest);
            var noCompressionCompressed = await _deflateCompression.CompressToStreamAsync(stream, leaveOpen: true, CompressionLevel.NoCompression);
            var smallestSizeCompressed = await _deflateCompression.CompressToStreamAsync(stream, leaveOpen: true, CompressionLevel.SmallestSize);

            var optimalDecompressed = await _deflateCompression.DecompressAsync(optimalCompressed);
            var fastestDecompressed = await _deflateCompression.DecompressAsync(fastestCompressed);
            var noCompressionDecompressed = await _deflateCompression.DecompressAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _deflateCompression.DecompressAsync(smallestSizeCompressed);

            // Assert
            Assert.Equal(input, optimalDecompressed);
            Assert.Equal(input, fastestDecompressed);
            Assert.Equal(input, noCompressionDecompressed);
            Assert.Equal(input, smallestSizeDecompressed);

            await stream.DisposeAsync();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public async Task CompressAsyncAndDecompressToStreamAsync_BytesInput_WithLevel_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();

            // Act
            var optimalCompressed = await _deflateCompression.CompressAsync(input, CompressionLevel.Optimal);
            var fastestCompressed = await _deflateCompression.CompressAsync(input, CompressionLevel.Fastest);
            var noCompressionCompressed = await _deflateCompression.CompressAsync(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = await _deflateCompression.CompressAsync(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = await _deflateCompression.DecompressToStreamAsync(optimalCompressed);
            var fastestDecompressed = await _deflateCompression.DecompressToStreamAsync(fastestCompressed);
            var noCompressionDecompressed = await _deflateCompression.DecompressToStreamAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _deflateCompression.DecompressToStreamAsync(smallestSizeCompressed);

            // Assert
            Assert.Equal(input, ((MemoryStream)optimalDecompressed).ToArray());
            Assert.Equal(input, ((MemoryStream)fastestDecompressed).ToArray());
            Assert.Equal(input, ((MemoryStream)noCompressionDecompressed).ToArray());
            Assert.Equal(input, ((MemoryStream)smallestSizeDecompressed).ToArray());

            await Task.WhenAll(
                optimalDecompressed.DisposeAsync().AsTask(),
                fastestDecompressed.DisposeAsync().AsTask(),
                noCompressionDecompressed.DisposeAsync().AsTask(),
                smallestSizeDecompressed.DisposeAsync().AsTask()
            );
        }

        [Fact]
        public async Task CompressToStreamAsyncAndDecompressToStreamAsync_BytesInput_WithLevel_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();

            // Act
            var optimalCompressed = await _deflateCompression.CompressToStreamAsync(input, CompressionLevel.Optimal);
            var fastestCompressed = await _deflateCompression.CompressToStreamAsync(input, CompressionLevel.Fastest);
            var noCompressionCompressed = await _deflateCompression.CompressToStreamAsync(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = await _deflateCompression.CompressToStreamAsync(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = await _deflateCompression.DecompressToStreamAsync(optimalCompressed);
            var fastestDecompressed = await _deflateCompression.DecompressToStreamAsync(fastestCompressed);
            var noCompressionDecompressed = await _deflateCompression.DecompressToStreamAsync(noCompressionCompressed);
            var smallestSizeDecompressed = await _deflateCompression.DecompressToStreamAsync(smallestSizeCompressed);

            // Assert
            Assert.Equal(input, ((MemoryStream)optimalDecompressed).ToArray());
            Assert.Equal(input, ((MemoryStream)fastestDecompressed).ToArray());
            Assert.Equal(input, ((MemoryStream)noCompressionDecompressed).ToArray());
            Assert.Equal(input, ((MemoryStream)smallestSizeDecompressed).ToArray());

            Assert.Throws<ObjectDisposedException>(() => optimalCompressed.Length);
            Assert.Throws<ObjectDisposedException>(() => fastestCompressed.Length);
            Assert.Throws<ObjectDisposedException>(() => noCompressionCompressed.Length);
            Assert.Throws<ObjectDisposedException>(() => smallestSizeCompressed.Length);

            await Task.WhenAll(
                optimalDecompressed.DisposeAsync().AsTask(),
                fastestDecompressed.DisposeAsync().AsTask(),
                noCompressionDecompressed.DisposeAsync().AsTask(),
                smallestSizeDecompressed.DisposeAsync().AsTask()
            );
        }

        [Fact]
        public void CompressAndDecompress_StringInput_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            const string input = "Hello, World!";

            // Act
            var compressed = _deflateCompression.Compress(input);
            var decompressed = _deflateCompression.Decompress(compressed);

            // Assert
            Assert.Equal(Encoding.UTF8.GetBytes(input), decompressed);
        }

        [Fact]
        public void CompressAndDecompress_BytesInput_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();

            // Act
            var compressed = _deflateCompression.Compress(input);
            var decompressed = _deflateCompression.Decompress(compressed);

            // Assert
            Assert.Equal(input, decompressed);
        }

        [Fact]
        public void CompressAndDecompress_StreamInput_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);

            // Act
            var compressed = _deflateCompression.Compress(stream);
            var decompressed = _deflateCompression.Decompress(compressed);

            // Assert
            Assert.Equal(input, decompressed);
        }

        [Fact]
        public void CompressAndDecompress_StreamInput_WithLeaveOpenTrue_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);

            // Act
            var compressed = _deflateCompression.Compress(stream, leaveOpen: true);
            var decompressed = _deflateCompression.Decompress(compressed);

            // Assert
            Assert.Equal(input, decompressed);

            stream.Dispose();
        }

        [Fact]
        public void CompressAndDecompress_StringInput_WithLevel_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            const string input = "Hello, World!";
            var inputBytes = Encoding.UTF8.GetBytes(input);

            // Act
            var optimalCompressed = _deflateCompression.Compress(input, CompressionLevel.Optimal);
            var fastestCompressed = _deflateCompression.Compress(input, CompressionLevel.Fastest);
            var noCompressionCompressed = _deflateCompression.Compress(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = _deflateCompression.Compress(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = _deflateCompression.Decompress(optimalCompressed);
            var fastestDecompressed = _deflateCompression.Decompress(fastestCompressed);
            var noCompressionDecompressed = _deflateCompression.Decompress(noCompressionCompressed);
            var smallestSizeDecompressed = _deflateCompression.Decompress(smallestSizeCompressed);

            // Assert
            Assert.Equal(inputBytes, optimalDecompressed);
            Assert.Equal(inputBytes, fastestDecompressed);
            Assert.Equal(inputBytes, noCompressionDecompressed);
            Assert.Equal(inputBytes, smallestSizeDecompressed);
        }

        [Fact]
        public void CompressAndDecompress_BytesInput_WithLevel_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();

            // Act
            var optimalCompressed = _deflateCompression.Compress(input, CompressionLevel.Optimal);
            var fastestCompressed = _deflateCompression.Compress(input, CompressionLevel.Fastest);
            var noCompressionCompressed = _deflateCompression.Compress(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = _deflateCompression.Compress(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = _deflateCompression.Decompress(optimalCompressed);
            var fastestDecompressed = _deflateCompression.Decompress(fastestCompressed);
            var noCompressionDecompressed = _deflateCompression.Decompress(noCompressionCompressed);
            var smallestSizeDecompressed = _deflateCompression.Decompress(smallestSizeCompressed);

            // Assert
            Assert.Equal(input, optimalDecompressed);
            Assert.Equal(input, fastestDecompressed);
            Assert.Equal(input, noCompressionDecompressed);
            Assert.Equal(input, smallestSizeDecompressed);
        }

        [Fact]
        public void CompressAndDecompress_StreamInput_WithLevel_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);

            // Act
            var optimalCompressed = _deflateCompression.Compress(stream, leaveOpen: true, CompressionLevel.Optimal);
            var fastestCompressed = _deflateCompression.Compress(stream, leaveOpen: true, CompressionLevel.Fastest);
            var noCompressionCompressed = _deflateCompression.Compress(stream, leaveOpen: true, CompressionLevel.NoCompression);
            var smallestSizeCompressed = _deflateCompression.Compress(stream, leaveOpen: false, CompressionLevel.SmallestSize);

            var optimalDecompressed = _deflateCompression.Decompress(optimalCompressed);
            var fastestDecompressed = _deflateCompression.Decompress(fastestCompressed);
            var noCompressionDecompressed = _deflateCompression.Decompress(noCompressionCompressed);
            var smallestSizeDecompressed = _deflateCompression.Decompress(smallestSizeCompressed);

            // Assert
            Assert.Equal(input, optimalDecompressed);
            Assert.Equal(input, fastestDecompressed);
            Assert.Equal(input, noCompressionDecompressed);
            Assert.Equal(input, smallestSizeDecompressed);
        }

        [Fact]
        public void CompressAndDecompress_StreamInput_WithLevelAndLeaveOpenTrue_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);

            // Act
            var optimalCompressed = _deflateCompression.Compress(stream, leaveOpen: true, CompressionLevel.Optimal);
            var fastestCompressed = _deflateCompression.Compress(stream, leaveOpen: true, CompressionLevel.Fastest);
            var noCompressionCompressed = _deflateCompression.Compress(stream, leaveOpen: true, CompressionLevel.NoCompression);
            var smallestSizeCompressed = _deflateCompression.Compress(stream, leaveOpen: true, CompressionLevel.SmallestSize);

            var optimalDecompressed = _deflateCompression.Decompress(optimalCompressed);
            var fastestDecompressed = _deflateCompression.Decompress(fastestCompressed);
            var noCompressionDecompressed = _deflateCompression.Decompress(noCompressionCompressed);
            var smallestSizeDecompressed = _deflateCompression.Decompress(smallestSizeCompressed);

            // Assert
            Assert.Equal(input, optimalDecompressed);
            Assert.Equal(input, fastestDecompressed);
            Assert.Equal(input, noCompressionDecompressed);
            Assert.Equal(input, smallestSizeDecompressed);

            stream.Dispose();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }

        [Fact]
        public void CompressToStreamAndDecompress_StringInput_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            const string input = "Hello, World!";

            // Act
            var compressed = _deflateCompression.CompressToStream(input);
            var decompressed = _deflateCompression.Decompress(compressed);

            // Assert
            Assert.Equal(Encoding.UTF8.GetBytes(input), decompressed);
        }

        [Fact]
        public void CompressToStreamAndDecompressAsync_BytesInput_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();

            // Act
            var compressed = _deflateCompression.CompressToStream(input);
            var decompressed = _deflateCompression.Decompress(compressed);

            // Assert
            Assert.Equal(input, decompressed);
        }

        [Fact]
        public void CompressToStreamAndDecompress_StreamInput_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);

            // Act
            var compressed = _deflateCompression.CompressToStream(stream);
            var decompressed = _deflateCompression.Decompress(compressed);

            // Assert
            Assert.Equal(input, decompressed);
        }

        [Fact]
        public void CompressToStreamAndDecompress_StreamInput_WithLeaveOpenTrue_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);

            // Act
            var compressed = _deflateCompression.CompressToStream(stream, leaveOpen: true);
            var decompressed = _deflateCompression.Decompress(compressed);

            // Assert
            Assert.Equal(input, decompressed);

            stream.Dispose();
        }

        [Fact]
        public void CompressToStreamAndDecompress_StringInput_WithLevel_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            const string input = "Hello, World!";
            var inputBytes = Encoding.UTF8.GetBytes(input);

            // Act
            var optimalCompressed = _deflateCompression.CompressToStream(input, CompressionLevel.Optimal);
            var fastestCompressed = _deflateCompression.CompressToStream(input, CompressionLevel.Fastest);
            var noCompressionCompressed = _deflateCompression.CompressToStream(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = _deflateCompression.CompressToStream(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = _deflateCompression.Decompress(optimalCompressed);
            var fastestDecompressed = _deflateCompression.Decompress(fastestCompressed);
            var noCompressionDecompressed = _deflateCompression.Decompress(noCompressionCompressed);
            var smallestSizeDecompressed = _deflateCompression.Decompress(smallestSizeCompressed);

            // Assert
            Assert.Equal(inputBytes, optimalDecompressed);
            Assert.Equal(inputBytes, fastestDecompressed);
            Assert.Equal(inputBytes, noCompressionDecompressed);
            Assert.Equal(inputBytes, smallestSizeDecompressed);
        }

        [Fact]
        public void CompressToStreamAndDecompress_BytesInput_WithLevel_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();

            // Act
            var optimalCompressed = _deflateCompression.CompressToStream(input, CompressionLevel.Optimal);
            var fastestCompressed = _deflateCompression.CompressToStream(input, CompressionLevel.Fastest);
            var noCompressionCompressed = _deflateCompression.CompressToStream(input, CompressionLevel.NoCompression);
            var smallestSizeCompressed = _deflateCompression.CompressToStream(input, CompressionLevel.SmallestSize);

            var optimalDecompressed = _deflateCompression.Decompress(optimalCompressed);
            var fastestDecompressed = _deflateCompression.Decompress(fastestCompressed);
            var noCompressionDecompressed = _deflateCompression.Decompress(noCompressionCompressed);
            var smallestSizeDecompressed = _deflateCompression.Decompress(smallestSizeCompressed);

            // Assert
            Assert.Equal(input, optimalDecompressed);
            Assert.Equal(input, fastestDecompressed);
            Assert.Equal(input, noCompressionDecompressed);
            Assert.Equal(input, smallestSizeDecompressed);
        }

        [Fact]
        public void CompressToStreamAndDecompress_StreamInput_WithLevel_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);

            // Act
            var optimalCompressed = _deflateCompression.CompressToStream(stream, leaveOpen: true, CompressionLevel.Optimal);
            var fastestCompressed = _deflateCompression.CompressToStream(stream, leaveOpen: true, CompressionLevel.Fastest);
            var noCompressionCompressed = _deflateCompression.CompressToStream(stream, leaveOpen: true, CompressionLevel.NoCompression);
            var smallestSizeCompressed = _deflateCompression.CompressToStream(stream, leaveOpen: false, CompressionLevel.SmallestSize);

            var optimalDecompressed = _deflateCompression.Decompress(optimalCompressed);
            var fastestDecompressed = _deflateCompression.Decompress(fastestCompressed);
            var noCompressionDecompressed = _deflateCompression.Decompress(noCompressionCompressed);
            var smallestSizeDecompressed = _deflateCompression.Decompress(smallestSizeCompressed);

            // Assert
            Assert.Equal(input, optimalDecompressed);
            Assert.Equal(input, fastestDecompressed);
            Assert.Equal(input, noCompressionDecompressed);
            Assert.Equal(input, smallestSizeDecompressed);
        }

        [Fact]
        public void CompressToStreamAndDecompress_StreamInput_WithLevelAndLeaveOpenTrue_RoundTrip_ReturnsOriginalBytes()
        {
            // Arrange
            var input = "Hello, World!"u8.ToArray();
            var stream = new MemoryStream(input);

            // Act
            var optimalCompressed = _deflateCompression.CompressToStream(stream, leaveOpen: true, CompressionLevel.Optimal);
            var fastestCompressed = _deflateCompression.CompressToStream(stream, leaveOpen: true, CompressionLevel.Fastest);
            var noCompressionCompressed = _deflateCompression.CompressToStream(stream, leaveOpen: true, CompressionLevel.NoCompression);
            var smallestSizeCompressed = _deflateCompression.CompressToStream(stream, leaveOpen: true, CompressionLevel.SmallestSize);

            var optimalDecompressed = _deflateCompression.Decompress(optimalCompressed);
            var fastestDecompressed = _deflateCompression.Decompress(fastestCompressed);
            var noCompressionDecompressed = _deflateCompression.Decompress(noCompressionCompressed);
            var smallestSizeDecompressed = _deflateCompression.Decompress(smallestSizeCompressed);

            // Assert
            Assert.Equal(input, optimalDecompressed);
            Assert.Equal(input, fastestDecompressed);
            Assert.Equal(input, noCompressionDecompressed);
            Assert.Equal(input, smallestSizeDecompressed);

            stream.Dispose();

            Assert.Throws<ObjectDisposedException>(() => stream.Length);
        }
    }
}