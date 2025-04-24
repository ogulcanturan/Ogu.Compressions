using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Ogu.Compressions.Abstractions
{
    /// <summary>
    /// Defines the interface for compression algorithms to performing asynchronous compression and decompression operations.
    /// </summary>
    public interface ICompression
    {
        /// <summary>
        /// Gets the name of the encoding used for underlying compression.
        /// </summary>
        string EncodingName { get; }

        /// <summary>
        /// Gets the size, in bytes, of the buffer. The default size is 81920.
        /// </summary>
        /// <remarks>
        /// Value must be greater than zero.
        /// </remarks>
        int BufferSize { get; }

        /// <summary>
        /// Gets the type of compression. This value identifies which compression algorithm is used.
        /// </summary>
        CompressionType Type { get; }

        /// <summary>
        /// Gets the compression level to use for the underlying stream.
        /// </summary>
        CompressionLevel Level { get; }

        /// <summary>
        /// Asynchronously compresses the given input string.
        /// </summary>
        /// <param name="input">The string to compress.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous compression operation, which wraps the <see cref="byte" /> array containing the compressed data.
        /// </returns>
        Task<byte[]> CompressAsync(string input, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously compresses the given <see cref="byte" /> array.
        /// </summary>
        /// <param name="bytes">The <see cref="byte" /> array to compress.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous compression operation, which wraps the <see cref="byte" /> array containing the compressed data.
        /// </returns>
        Task<byte[]> CompressAsync(byte[] bytes, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously compresses the given <paramref name="stream"/>.
        /// </summary>
        /// <param name="stream">The stream to compress.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous compression operation, which wraps the <see cref="byte" /> array containing the compressed data.
        /// </returns>
        /// <remarks>Closes the underlying <paramref name="stream"/> after the operation is completed.</remarks>
        Task<byte[]> CompressAsync(Stream stream, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously compresses the given <paramref name="stream"/>, with an option to leave the stream open after the operation.
        /// </summary>
        /// <param name="stream">The stream to compress.</param>
        /// <param name="leaveOpen">Indicates whether the <paramref name="stream"/> should remain open after the operation.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous compression operation, which wraps the <see cref="byte" /> array containing the compressed data.</returns>
        Task<byte[]> CompressAsync(Stream stream, bool leaveOpen, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously compresses the given input string using the specified compression level.
        /// </summary>
        /// <param name="input">The string to compress.</param>
        /// <param name="level">The compression level to use.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous compression operation, which wraps the <see cref="byte" /> array containing the compressed data.
        /// </returns>
        Task<byte[]> CompressAsync(string input, CompressionLevel level, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously compresses the given <see cref="byte" /> array using the specified compression level.
        /// </summary>
        /// <param name="bytes">The <see cref="byte" /> array to compress.</param>
        /// <param name="level">The compression level to use.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous compression operation, which wraps the <see cref="byte" /> array containing the compressed data.</returns>
        Task<byte[]> CompressAsync(byte[] bytes, CompressionLevel level, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously compresses the given <paramref name="stream"/> using the specified compression level.
        /// </summary>
        /// <param name="stream">The stream to compress.</param>
        /// <param name="level">The compression level to use.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous compression operation, which wraps the <see cref="byte" /> array containing the compressed data.
        /// </returns>
        /// <remarks>Closes the underlying <paramref name="stream"/> after the operation is completed.</remarks>
        Task<byte[]> CompressAsync(Stream stream, CompressionLevel level, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously compresses the given <paramref name="stream"/> using the specified compression level, with an option to leave the stream open after the operation.
        /// </summary>
        /// <param name="stream">The stream to compress.</param>
        /// <param name="leaveOpen">Indicates whether the <paramref name="stream"/> should remain open after the operation.</param>
        /// <param name="level">The compression level to use.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous compression operation, which wraps the <see cref="byte" /> array containing the compressed data.
        /// </returns>
        Task<byte[]> CompressAsync(Stream stream, bool leaveOpen, CompressionLevel level, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously compresses the given input string to a stream.
        /// </summary>
        /// <param name="input">The string to compress.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous compression operation, which wraps the stream containing the compressed data.
        /// </returns>
        Task<Stream> CompressToStreamAsync(string input, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously compresses the given <see cref="byte" /> array to a stream.
        /// </summary>
        /// <param name="bytes">The <see cref="byte" /> array to compress.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous compression operation, which wraps the stream containing the compressed data.
        /// </returns>
        Task<Stream> CompressToStreamAsync(byte[] bytes, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously compresses the given <paramref name="stream"/> to another stream.
        /// </summary>
        /// <param name="stream">The stream to compress.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous compression operation, which wraps the stream containing the compressed data.
        /// </returns>
        /// <remarks>Closes the underlying <paramref name="stream"/> after the operation is completed.</remarks>
        Task<Stream> CompressToStreamAsync(Stream stream, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously compresses the given <paramref name="stream"/> to another stream, with an option to leave the stream open after the operation.
        /// </summary>
        /// <param name="stream">The stream to compress.</param>
        /// <param name="leaveOpen">Indicates whether the <paramref name="stream"/> should remain open after the operation.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous compression operation, which wraps the stream containing the compressed data.
        /// </returns>
        Task<Stream> CompressToStreamAsync(Stream stream, bool leaveOpen, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously compresses the given input string to a stream using the specified compression level.
        /// </summary>
        /// <param name="input">The string to compress.</param>
        /// <param name="level">The compression level to use.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous compression operation, which wraps the stream containing the compressed data.
        /// </returns>
        Task<Stream> CompressToStreamAsync(string input, CompressionLevel level, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously compresses the given <see cref="byte" /> array to a stream using the specified compression level.
        /// </summary>
        /// <param name="bytes">The <see cref="byte" /> array to compress.</param>
        /// <param name="level">The compression level to use.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous compression operation, which wraps the stream containing the compressed data.
        /// </returns>
        Task<Stream> CompressToStreamAsync(byte[] bytes, CompressionLevel level, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously compresses the given <paramref name="stream"/> to another stream using the specified compression level.
        /// </summary>
        /// <param name="stream">The stream to compress.</param>
        /// <param name="level">The compression level to use.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous compression operation, which wraps the stream containing the compressed data.
        /// </returns>
        /// <remarks>Closes the underlying <paramref name="stream"/> after the operation is completed.</remarks>
        Task<Stream> CompressToStreamAsync(Stream stream, CompressionLevel level, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously compresses the given <paramref name="stream"/> to another stream using the specified compression level, with an option to leave the stream open after the operation.
        /// </summary>
        /// <param name="stream">The stream to compress.</param>
        /// <param name="leaveOpen">Indicates whether the <paramref name="stream"/> should remain open after the operation.</param>
        /// <param name="level">The compression level to use.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous compression operation, which wraps the stream containing the compressed data.
        /// </returns>
        /// <remarks>
        /// The returned stream is positioned at the beginning. Caller is responsible for disposing the stream.
        /// </remarks>
        Task<Stream> CompressToStreamAsync(Stream stream, bool leaveOpen, CompressionLevel level, CancellationToken cancellationToken = default);

        /// <summary>
        /// Compresses the given input string.
        /// </summary>
        /// <param name="input">The string to compress.</param>
        /// <returns>A <see cref="byte" /> array containing the compressed data.</returns>
        byte[] Compress(string input);

        /// <summary>
        /// Compresses the given <see cref="byte" /> array.
        /// </summary>
        /// <param name="bytes">The <see cref="byte" /> array to compress.</param>
        /// <returns>A <see cref="byte" /> array containing the compressed data.</returns>
        byte[] Compress(byte[] bytes);

        /// <summary>
        /// Compresses the given <paramref name="stream"/>.
        /// </summary>
        /// <param name="stream">The stream to compress.</param>
        /// <returns>A <see cref="byte" /> array containing the compressed data.</returns>
        /// <remarks>Closes the underlying <paramref name="stream" /> after the operation is completed.</remarks>
        byte[] Compress(Stream stream);

        /// <summary>
        /// Compresses the given <paramref name="stream"/>, with an option to leave the stream open after the operation.
        /// </summary>
        /// <param name="stream">The stream to compress.</param>
        /// <param name="leaveOpen">Indicates whether the <paramref name="stream" /> should remain open after the operation.</param>
        /// <returns>A <see cref="byte" /> array containing the compressed data.</returns>
        byte[] Compress(Stream stream, bool leaveOpen);

        /// <summary>
        /// Compresses the given input string using the specified compression level.
        /// </summary>
        /// <param name="input">The string to compress.</param>
        /// <param name="level">The compression level to use.</param>
        /// <returns>A <see cref="byte" /> array containing the compressed data.</returns>
        byte[] Compress(string input, CompressionLevel level);

        /// <summary>
        /// Compresses the given <see cref="byte" /> array using the specified compression level.
        /// </summary>
        /// <param name="bytes">The <see cref="byte" /> array to compress.</param>
        /// <param name="level">The compression level to use.</param>
        /// <returns>A <see cref="byte" /> array containing the compressed data.</returns>
        byte[] Compress(byte[] bytes, CompressionLevel level);

        /// <summary>
        /// Compresses the given <paramref name="stream"/> using the specified compression level.
        /// </summary>
        /// <param name="stream">The stream to compress.</param>
        /// <param name="level">The compression level to use.</param>
        /// <returns>A <see cref="byte" /> array containing the compressed data.</returns>
        /// <remarks>Closes the underlying <paramref name="stream"/> after the operation is completed.</remarks>
        byte[] Compress(Stream stream, CompressionLevel level);

        /// <summary>
        /// Compresses the given <paramref name="stream"/> using the specified compression level, with an option to leave the stream open after the operation.
        /// </summary>
        /// <param name="stream">The stream to compress.</param>
        /// <param name="leaveOpen">Indicates whether the <paramref name="stream"/> should remain open after the operation.</param>
        /// <param name="level">The compression level to use.</param>
        /// <returns>A <see cref="byte" /> array containing the compressed data.</returns>
        byte[] Compress(Stream stream, bool leaveOpen, CompressionLevel level);

        /// <summary>
        /// Compresses the given input string to a stream.
        /// </summary>
        /// <param name="input">The string to compress.</param>
        /// <returns>A stream containing the compressed data.</returns>
        /// <remarks>
        /// The returned stream is positioned at the beginning. Caller is responsible for disposing the stream.
        /// </remarks>
        Stream CompressToStream(string input);

        /// <summary>
        /// Compresses the given <see cref="byte" /> array to a stream.
        /// </summary>
        /// <param name="bytes">The <see cref="byte" /> array to compress.</param>
        /// <returns>A stream containing the compressed data.</returns>
        /// <remarks>
        /// The returned stream is positioned at the beginning. Caller is responsible for disposing the stream.
        /// </remarks>
        Stream CompressToStream(byte[] bytes);

        /// <summary>
        /// Compresses the given <paramref name="stream"/> to another stream using the specified compression level.
        /// </summary>
        /// <param name="stream">The stream to compress.</param>
        /// <returns>A stream containing the compressed data.</returns>
        /// <remarks>
        /// Closes the underlying <paramref name="stream"/> after the operation is completed.
        /// <para>
        /// The returned stream is positioned at the beginning. Caller is responsible for disposing the stream.
        /// </para>
        /// </remarks>
        Stream CompressToStream(Stream stream);

        /// <summary>
        /// Compresses the given <paramref name="stream"/> to another stream, with an option to leave the stream open after the operation.
        /// </summary>
        /// <param name="stream">The stream to compress.</param>
        /// <param name="leaveOpen">Indicates whether the <paramref name="stream"/> should remain open after the operation.</param>
        /// <returns>A stream containing the compressed data.</returns>
        /// <remarks>
        /// The returned stream is positioned at the beginning. Caller is responsible for disposing the stream.
        /// </remarks>
        Stream CompressToStream(Stream stream, bool leaveOpen);

        /// <summary>
        /// Compresses the given input string to a stream using the specified compression level.
        /// </summary>
        /// <param name="input">The string to compress.</param>
        /// <param name="level">The compression level to use.</param>
        /// <returns>A stream containing the compressed data.</returns>
        /// <remarks>
        /// The returned stream is positioned at the beginning. Caller is responsible for disposing the stream.
        /// </remarks>
        Stream CompressToStream(string input, CompressionLevel level);

        /// <summary>
        /// Compresses the given <see cref="byte" /> array to a stream using the specified compression level.
        /// </summary>
        /// <param name="bytes">The <see cref="byte" /> array to compress.</param>
        /// <param name="level">The compression level to use.</param>
        /// <returns>A stream containing the compressed data.</returns>
        /// <remarks>
        /// The returned stream is positioned at the beginning. Caller is responsible for disposing the stream.
        /// </remarks>
        Stream CompressToStream(byte[] bytes, CompressionLevel level);

        /// <summary>
        /// Compresses the given <paramref name="stream"/> to another stream using the specified compression level.
        /// </summary>
        /// <param name="stream">The stream to compress.</param>
        /// <param name="level">The compression level to use.</param>
        /// <returns>A stream containing the compressed data.</returns>
        /// <remarks>
        /// Closes the underlying <paramref name="stream"/> after the operation is completed.
        /// <para>
        /// The returned stream is positioned at the beginning. Caller is responsible for disposing the stream.
        /// </para>
        /// </remarks>
        Stream CompressToStream(Stream stream, CompressionLevel level);

        /// <summary>
        /// Compresses the given <paramref name="stream"/> to another stream using the specified compression level, with an option to leave the stream open after the operation.
        /// </summary>
        /// <param name="stream">The stream to compress.</param>
        /// <param name="leaveOpen">Indicates whether the <paramref name="stream"/> should remain open after the operation.</param>
        /// <param name="level">The compression level to use.</param>
        /// <returns>A stream containing the compressed data.</returns>
        /// <remarks>
        /// The returned stream is positioned at the beginning. Caller is responsible for disposing the stream.
        /// </remarks>
        Stream CompressToStream(Stream stream, bool leaveOpen, CompressionLevel level);

        /// <summary>
        /// Asynchronously decompresses the given <see cref="byte" /> array.
        /// </summary>
        /// <param name="bytes">The <see cref="byte" /> array to decompress.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous decompression operation, which wraps the <see cref="byte" /> array containing the decompressed data.</returns>
        Task<byte[]> DecompressAsync(byte[] bytes, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously decompresses the given <paramref name="stream"/>.
        /// </summary>
        /// <param name="stream">The stream to decompress.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous decompression operation, which wraps the <see cref="byte" /> array containing the decompressed data.</returns>
        /// <remarks>Closes the underlying <paramref name="stream"/> after the operation is completed.</remarks>
        Task<byte[]> DecompressAsync(Stream stream, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously decompresses the given <paramref name="stream"/>, with an option to leave the stream open after the operation.
        /// </summary>
        /// <param name="stream">The stream to decompress.</param>
        /// <param name="leaveOpen">Indicates whether the <paramref name="stream"/> should remain open after the operation.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous decompression operation, which wraps the <see cref="byte" /> array containing the decompressed data.</returns>
        Task<byte[]> DecompressAsync(Stream stream, bool leaveOpen, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously decompresses the given <see cref="byte" /> array to a stream.
        /// </summary>
        /// <param name="bytes">The <see cref="byte" /> array to decompress.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous decompression operation, which wraps the stream containing the decompressed stream.</returns>
        /// <remarks>
        /// The returned stream is positioned at the beginning. Caller is responsible for disposing the stream.
        /// </remarks>
        Task<Stream> DecompressToStreamAsync(byte[] bytes, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously decompresses the given <paramref name="stream"/> to another stream.
        /// </summary>
        /// <param name="stream">The stream to decompress.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous decompression operation, which wraps the stream containing the decompressed stream.</returns>
        /// <remarks>
        /// Closes the underlying <paramref name="stream"/> after the operation is completed.
        /// <para>
        /// The returned stream is positioned at the beginning. Caller is responsible for disposing the stream.
        /// </para>
        /// </remarks>
        Task<Stream> DecompressToStreamAsync(Stream stream, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously decompresses the given <paramref name="stream"/> to another stream, with an option to leave the stream open after the operation.
        /// </summary>
        /// <param name="stream">The stream to decompress.</param>
        /// <param name="leaveOpen">Indicates whether the <paramref name="stream"/> should remain open after the operation.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous decompression operation, which wraps the stream containing the decompressed stream.</returns>
        /// <remarks>
        /// The returned stream is positioned at the beginning. Caller is responsible for disposing the stream.
        /// </remarks>
        Task<Stream> DecompressToStreamAsync(Stream stream, bool leaveOpen, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously decompresses the content of an HTTP request to a stream.
        /// </summary>
        /// <param name="httpContent">The HTTP content to decompress.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous decompression operation, which wraps the stream containing the decompressed stream.</returns>
        /// <remarks>
        /// Closes the underlying <paramref name="httpContent"/> stream after the operation is completed.
        /// <para>
        /// The returned stream is positioned at the beginning. Caller is responsible for disposing the stream.
        /// </para>
        /// </remarks>
        Task<Stream> DecompressToStreamAsync(HttpContent httpContent, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously decompresses the content of an HTTP request to a stream, with an option to leave the stream open after the operation.
        /// </summary>
        /// <param name="httpContent">The HTTP content to decompress.</param>
        /// <param name="leaveOpen">Indicates whether the <paramref name="httpContent"/> stream should remain open after the operation.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous decompression operation, which wraps the stream containing the decompressed stream.</returns>
        /// <remarks>
        /// The returned stream is positioned at the beginning. Caller is responsible for disposing the stream.
        /// </remarks>
        Task<Stream> DecompressToStreamAsync(HttpContent httpContent, bool leaveOpen, CancellationToken cancellationToken = default);

        /// <summary>
        /// Decompresses the given <see cref="byte" /> array.
        /// </summary>
        /// <param name="bytes">The <see cref="byte" /> array to decompress.</param>
        /// <returns>A <see cref="byte" /> array containing the decompressed data.</returns>
        byte[] Decompress(byte[] bytes);

        /// <summary>
        /// Decompresses the given <paramref name="stream"/>.
        /// </summary>
        /// <param name="stream">The stream to decompress.</param>
        /// <returns>A <see cref="byte" /> array containing the decompressed data.</returns>
        /// <remarks>Closes the underlying <paramref name="stream"/> after the operation is completed.</remarks>
        byte[] Decompress(Stream stream);

        /// <summary>
        /// Decompresses the given <paramref name="stream"/>, with an option to leave the stream open after the operation.
        /// </summary>
        /// <param name="stream">The stream to decompress.</param>
        /// <param name="leaveOpen">Indicates whether the <paramref name="stream"/> should remain open after the operation.</param>
        /// <returns>A <see cref="byte" /> array containing the decompressed data.</returns>
        byte[] Decompress(Stream stream, bool leaveOpen);
    }
}