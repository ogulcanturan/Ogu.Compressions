using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Ogu.Compressions.Abstractions
{
    /// <summary>
    /// Provider interface for retrieving compression implementations and performing asynchronous compression and decompression operations.
    /// </summary>
    public interface ICompressionProvider
    {
        /// <summary>
        /// Gets the available compression implementations.
        /// </summary>
        IEnumerable<ICompression> Compressions { get; }

        /// <summary>
        /// Retrieves a compression implementation based on the specified <see cref="CompressionType"/>.
        /// Returns <c>null</c> if no matching service is found.
        /// </summary>
        /// <param name="type">The type of compression to retrieve.</param>
        /// <returns>A <see cref="ICompression"/> instance if available; otherwise, <c>null</c>.</returns>
        ICompression GetCompression(CompressionType type);

        /// <summary>
        /// Retrieves a compression implementation based on the specified encoding name.
        /// Returns <c>null</c> if no matching implementation is found or if no applicable service is available.
        /// </summary>
        /// <param name="encodingName">The name of the encoding. e.g., "br", "deflate", "snappy", "zstd", "gzip" or "none" (used internally to indicate no compression; does not correspond to a real encoding header).</param>
        /// <returns>An <see cref="ICompression"/> instance if available; otherwise, <c>null</c>.</returns>
        ICompression GetCompression(string encodingName);

        /// <summary>
        /// Retrieves a compression implementation based on the specified <see cref="CompressionType"/>.
        /// Throws <see cref="CompressionNotAvailableException"/> if no matching service is found.
        /// </summary>
        /// <param name="type">The type of compression to retrieve.</param>
        /// <returns>A <see cref="ICompression"/> instance.</returns>
        /// <exception cref="CompressionNotAvailableException">Thrown when no compression implementation is registered for the specified type.</exception>
        ICompression GetRequiredCompression(CompressionType type);

        /// <summary>
        /// Retrieves a compression implementation based on the specified encoding name.
        /// Throws <see cref="CompressionNotAvailableException"/> if no matching implementation is found or if no applicable service is available.
        /// </summary>
        /// <param name="encodingName">The name of the encoding. e.g., "br", "deflate", "snappy", "zstd", "gzip" or "none" (used internally to indicate no compression; does not correspond to a real encoding header).</param>
        /// <returns>A <see cref="ICompression"/> instance.</returns>
        /// <exception cref="CompressionNotAvailableException">Thrown when no matching implementation is found or if no applicable service available for the specified type.</exception>
        ICompression GetRequiredCompression(string encodingName);

        /// <summary>
        /// Asynchronously compresses the given input string.
        /// </summary>
        /// <param name="type">The type of compression to be used.</param>
        /// <param name="input">The string to compress.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous compression operation, which wraps the <see cref="byte" /> array containing the compressed data.
        /// </returns>
        /// <exception cref="CompressionNotAvailableException">Thrown when no compression implementation is registered for the specified type.</exception>
        Task<byte[]> CompressAsync(CompressionType type, string input, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously compresses the given input string.
        /// </summary>
        /// <param name="encodingName">The name of the encoding. e.g., "br", "deflate", "snappy", "zstd", "gzip" or "none" (used internally to indicate no compression; does not correspond to a real encoding header).</param>
        /// <param name="input">The string to compress.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous compression operation, which wraps the <see cref="byte" /> array containing the compressed data.
        /// </returns>
        /// <exception cref="CompressionNotAvailableException">Thrown when no matching implementation is found or if no applicable service available for the specified type.</exception>
        Task<byte[]> CompressAsync(string encodingName, string input, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously compresses the given <see cref="byte" /> array.
        /// </summary>
        /// <param name="type">The type of compression to be used.</param>
        /// <param name="bytes">The <see cref="byte" /> array to compress.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous compression operation, which wraps the <see cref="byte" /> array containing the compressed data.
        /// </returns>
        /// <exception cref="CompressionNotAvailableException">Thrown when no compression implementation is registered for the specified type.</exception>
        Task<byte[]> CompressAsync(CompressionType type, byte[] bytes, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously compresses the given <see cref="byte" /> array.
        /// </summary>
        /// <param name="encodingName">The name of the encoding. e.g., "br", "deflate", "snappy", "zstd", "gzip" or "none" (used internally to indicate no compression; does not correspond to a real encoding header).</param>
        /// <param name="bytes">The <see cref="byte" /> array to compress.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous compression operation, which wraps the <see cref="byte" /> array containing the compressed data.
        /// </returns>
        /// <exception cref="CompressionNotAvailableException">Thrown when no matching implementation is found or if no applicable service available for the specified type.</exception>
        Task<byte[]> CompressAsync(string encodingName, byte[] bytes, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously compresses the given <paramref name="stream"/>.
        /// </summary>
        /// <param name="type">The type of compression to be used.</param>
        /// <param name="stream">The stream to compress.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous compression operation, which wraps the <see cref="byte" /> array containing the compressed data.
        /// </returns>
        /// <remarks>Closes the underlying <paramref name="stream"/> after the operation is completed.</remarks>
        /// <exception cref="CompressionNotAvailableException">Thrown when no compression implementation is registered for the specified type.</exception>
        Task<byte[]> CompressAsync(CompressionType type, Stream stream, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously compresses the given <paramref name="stream"/>.
        /// </summary>
        /// <param name="encodingName">The name of the encoding. e.g., "br", "deflate", "snappy", "zstd", "gzip" or "none" (used internally to indicate no compression; does not correspond to a real encoding header).</param>
        /// <param name="stream">The stream to compress.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous compression operation, which wraps the <see cref="byte" /> array containing the compressed data.
        /// </returns>
        /// <remarks>Closes the underlying <paramref name="stream"/> after the operation is completed.</remarks>
        /// <exception cref="CompressionNotAvailableException">Thrown when no matching implementation is found or if no applicable service available for the specified type.</exception>
        Task<byte[]> CompressAsync(string encodingName, Stream stream, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously compresses the given <paramref name="stream"/>, with an option to leave the stream open after the operation.
        /// </summary>
        /// <param name="type">The type of compression to be used.</param>
        /// <param name="stream">The stream to compress.</param>
        /// <param name="leaveOpen">Indicates whether the <paramref name="stream"/> should remain open after the operation.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous compression operation, which wraps the <see cref="byte" /> array containing the compressed data.</returns>
        /// <exception cref="CompressionNotAvailableException">Thrown when no compression implementation is registered for the specified type.</exception>
        Task<byte[]> CompressAsync(CompressionType type, Stream stream, bool leaveOpen, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously compresses the given <paramref name="stream"/>, with an option to leave the stream open after the operation.
        /// </summary>
        /// <param name="encodingName">The name of the encoding. e.g., "br", "deflate", "snappy", "zstd", "gzip" or "none" (used internally to indicate no compression; does not correspond to a real encoding header).</param>
        /// <param name="stream">The stream to compress.</param>
        /// <param name="leaveOpen">Indicates whether the <paramref name="stream"/> should remain open after the operation.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous compression operation, which wraps the <see cref="byte" /> array containing the compressed data.</returns>
        /// <exception cref="CompressionNotAvailableException">Thrown when no matching implementation is found or if no applicable service available for the specified type.</exception>
        Task<byte[]> CompressAsync(string encodingName, Stream stream, bool leaveOpen, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously compresses the given input string using the specified compression level.
        /// </summary>
        /// <param name="type">The type of compression to be used.</param>
        /// <param name="input">The string to compress.</param>
        /// <param name="level">The compression level to use.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous compression operation, which wraps the <see cref="byte" /> array containing the compressed data.
        /// </returns>
        /// <exception cref="CompressionNotAvailableException">Thrown when no compression implementation is registered for the specified type.</exception>
        Task<byte[]> CompressAsync(CompressionType type, string input, CompressionLevel level, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously compresses the given input string using the specified compression level.
        /// </summary>
        /// <param name="encodingName">The name of the encoding. e.g., "br", "deflate", "snappy", "zstd", "gzip" or "none" (used internally to indicate no compression; does not correspond to a real encoding header).</param>
        /// <param name="input">The string to compress.</param>
        /// <param name="level">The compression level to use.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous compression operation, which wraps the <see cref="byte" /> array containing the compressed data.
        /// </returns>
        /// <exception cref="CompressionNotAvailableException">Thrown when no matching implementation is found or if no applicable service available for the specified type.</exception>
        Task<byte[]> CompressAsync(string encodingName, string input, CompressionLevel level, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously compresses the given <see cref="byte" /> array using the specified compression level.
        /// </summary>
        /// <param name="type">The type of compression to be used.</param>
        /// <param name="bytes">The <see cref="byte" /> array to compress.</param>
        /// <param name="level">The compression level to use.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous compression operation, which wraps the <see cref="byte" /> array containing the compressed data.</returns>
        /// <exception cref="CompressionNotAvailableException">Thrown when no compression implementation is registered for the specified type.</exception>
        Task<byte[]> CompressAsync(CompressionType type, byte[] bytes, CompressionLevel level, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously compresses the given <see cref="byte" /> array using the specified compression level.
        /// </summary>
        /// <param name="encodingName">The name of the encoding. e.g., "br", "deflate", "snappy", "zstd", "gzip" or "none" (used internally to indicate no compression; does not correspond to a real encoding header).</param>
        /// <param name="bytes">The <see cref="byte" /> array to compress.</param>
        /// <param name="level">The compression level to use.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous compression operation, which wraps the <see cref="byte" /> array containing the compressed data.</returns>
        /// <exception cref="CompressionNotAvailableException">Thrown when no matching implementation is found or if no applicable service available for the specified type.</exception>
        Task<byte[]> CompressAsync(string encodingName, byte[] bytes, CompressionLevel level, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously compresses the given <paramref name="stream"/> using the specified compression level.
        /// </summary>
        /// <param name="type">The type of compression to be used.</param>
        /// <param name="stream">The stream to compress.</param>
        /// <param name="level">The compression level to use.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous compression operation, which wraps the <see cref="byte" /> array containing the compressed data.
        /// </returns>
        /// <remarks>Closes the underlying <paramref name="stream"/> after the operation is completed.</remarks>
        /// <exception cref="CompressionNotAvailableException">Thrown when no compression implementation is registered for the specified type.</exception>
        Task<byte[]> CompressAsync(CompressionType type, Stream stream, CompressionLevel level, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously compresses the given <paramref name="stream"/> using the specified compression level.
        /// </summary>
        /// <param name="encodingName">The name of the encoding. e.g., "br", "deflate", "snappy", "zstd", "gzip" or "none" (used internally to indicate no compression; does not correspond to a real encoding header).</param>
        /// <param name="stream">The stream to compress.</param>
        /// <param name="level">The compression level to use.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous compression operation, which wraps the <see cref="byte" /> array containing the compressed data.
        /// </returns>
        /// <remarks>Closes the underlying <paramref name="stream"/> after the operation is completed.</remarks>
        /// <exception cref="CompressionNotAvailableException">Thrown when no matching implementation is found or if no applicable service available for the specified type.</exception>
        Task<byte[]> CompressAsync(string encodingName, Stream stream, CompressionLevel level, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously compresses the given <paramref name="stream"/> using the specified compression level, with an option to leave the stream open after the operation.
        /// </summary>
        /// <param name="type">The type of compression to be used.</param>
        /// <param name="stream">The stream to compress.</param>
        /// <param name="level">The compression level to use.</param>
        /// <param name="leaveOpen">Indicates whether the <paramref name="stream"/> should remain open after the operation.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous compression operation, which wraps the <see cref="byte" /> array containing the compressed data.
        /// </returns>
        /// <exception cref="CompressionNotAvailableException">Thrown when no compression implementation is registered for the specified type.</exception>
        Task<byte[]> CompressAsync(CompressionType type, Stream stream, CompressionLevel level, bool leaveOpen, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously compresses the given <paramref name="stream"/> using the specified compression level, with an option to leave the stream open after the operation.
        /// </summary>
        /// <param name="encodingName">The name of the encoding. e.g., "br", "deflate", "snappy", "zstd", "gzip" or "none" (used internally to indicate no compression; does not correspond to a real encoding header).</param>
        /// <param name="stream">The stream to compress.</param>
        /// <param name="level">The compression level to use.</param>
        /// <param name="leaveOpen">Indicates whether the <paramref name="stream"/> should remain open after the operation.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous compression operation, which wraps the <see cref="byte" /> array containing the compressed data.
        /// </returns>
        /// <exception cref="CompressionNotAvailableException">Thrown when no matching implementation is found or if no applicable service available for the specified type.</exception>
        Task<byte[]> CompressAsync(string encodingName, Stream stream, CompressionLevel level, bool leaveOpen, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously compresses the given input string to a stream.
        /// </summary>
        /// <param name="type">The type of compression to be used.</param>
        /// <param name="input">The string to compress.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous compression operation, which wraps the stream containing the compressed data.
        /// </returns>
        /// <exception cref="CompressionNotAvailableException">Thrown when no compression implementation is registered for the specified type.</exception>
        Task<Stream> CompressToStreamAsync(CompressionType type, string input, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously compresses the given input string to a stream.
        /// </summary>
        /// <param name="encodingName">The name of the encoding. e.g., "br", "deflate", "snappy", "zstd", "gzip" or "none" (used internally to indicate no compression; does not correspond to a real encoding header).</param>
        /// <param name="input">The string to compress.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous compression operation, which wraps the stream containing the compressed data.
        /// </returns>
        /// <exception cref="CompressionNotAvailableException">Thrown when no matching implementation is found or if no applicable service available for the specified type.</exception>
        Task<Stream> CompressToStreamAsync(string encodingName, string input, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously compresses the given <see cref="byte" /> array to a stream.
        /// </summary>
        /// <param name="type">The type of compression to be used.</param>
        /// <param name="bytes">The <see cref="byte" /> array to compress.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous compression operation, which wraps the stream containing the compressed data.
        /// </returns>
        /// <exception cref="CompressionNotAvailableException">Thrown when no compression implementation is registered for the specified type.</exception>
        Task<Stream> CompressToStreamAsync(CompressionType type, byte[] bytes, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously compresses the given <see cref="byte" /> array to a stream.
        /// </summary>
        /// <param name="encodingName">The name of the encoding. e.g., "br", "deflate", "snappy", "zstd", "gzip" or "none" (used internally to indicate no compression; does not correspond to a real encoding header).</param>
        /// <param name="bytes">The <see cref="byte" /> array to compress.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous compression operation, which wraps the stream containing the compressed data.
        /// </returns>
        /// <exception cref="CompressionNotAvailableException">Thrown when no matching implementation is found or if no applicable service available for the specified type.</exception>
        Task<Stream> CompressToStreamAsync(string encodingName, byte[] bytes, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously compresses the given <paramref name="stream"/> to another stream.
        /// </summary>
        /// <param name="type">The type of compression to be used.</param>
        /// <param name="stream">The stream to compress.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous compression operation, which wraps the stream containing the compressed data.
        /// </returns>
        /// <remarks>Closes the underlying <paramref name="stream"/> after the operation is completed.</remarks>
        /// <exception cref="CompressionNotAvailableException">Thrown when no compression implementation is registered for the specified type.</exception>
        Task<Stream> CompressToStreamAsync(CompressionType type, Stream stream, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously compresses the given <paramref name="stream"/> to another stream.
        /// </summary>
        /// <param name="encodingName">The name of the encoding. e.g., "br", "deflate", "snappy", "zstd", "gzip" or "none" (used internally to indicate no compression; does not correspond to a real encoding header).</param>
        /// <param name="stream">The stream to compress.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous compression operation, which wraps the stream containing the compressed data.
        /// </returns>
        /// <remarks>Closes the underlying <paramref name="stream"/> after the operation is completed.</remarks>
        /// <exception cref="CompressionNotAvailableException">Thrown when no matching implementation is found or if no applicable service available for the specified type.</exception>
        Task<Stream> CompressToStreamAsync(string encodingName, Stream stream, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously compresses the given <paramref name="stream"/> to another stream, with an option to leave the stream open after the operation.
        /// </summary>
        /// <param name="type">The type of compression to be used.</param>
        /// <param name="stream">The stream to compress.</param>
        /// <param name="leaveOpen">Indicates whether the <paramref name="stream"/> should remain open after the operation.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous compression operation, which wraps the stream containing the compressed data.
        /// </returns>
        /// <exception cref="CompressionNotAvailableException">Thrown when no compression implementation is registered for the specified type.</exception>
        Task<Stream> CompressToStreamAsync(CompressionType type, Stream stream, bool leaveOpen, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously compresses the given <paramref name="stream"/> to another stream, with an option to leave the stream open after the operation.
        /// </summary>
        /// <param name="encodingName">The name of the encoding. e.g., "br", "deflate", "snappy", "zstd", "gzip" or "none" (used internally to indicate no compression; does not correspond to a real encoding header).</param>
        /// <param name="stream">The stream to compress.</param>
        /// <param name="leaveOpen">Indicates whether the <paramref name="stream"/> should remain open after the operation.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous compression operation, which wraps the stream containing the compressed data.
        /// </returns>
        /// <exception cref="CompressionNotAvailableException">Thrown when no matching implementation is found or if no applicable service available for the specified type.</exception>
        Task<Stream> CompressToStreamAsync(string encodingName, Stream stream, bool leaveOpen, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously compresses the given input string to a stream using the specified compression level.
        /// </summary>
        /// <param name="type">The type of compression to be used.</param>
        /// <param name="input">The string to compress.</param>
        /// <param name="level">The compression level to use.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous compression operation, which wraps the stream containing the compressed data.
        /// </returns>
        /// <exception cref="CompressionNotAvailableException">Thrown when no compression implementation is registered for the specified type.</exception>
        Task<Stream> CompressToStreamAsync(CompressionType type, string input, CompressionLevel level, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously compresses the given input string to a stream using the specified compression level.
        /// </summary>
        /// <param name="encodingName">The name of the encoding. e.g., "br", "deflate", "snappy", "zstd", "gzip" or "none" (used internally to indicate no compression; does not correspond to a real encoding header).</param>
        /// <param name="input">The string to compress.</param>
        /// <param name="level">The compression level to use.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous compression operation, which wraps the stream containing the compressed data.
        /// </returns>
        /// <exception cref="CompressionNotAvailableException">Thrown when no matching implementation is found or if no applicable service available for the specified type.</exception>
        Task<Stream> CompressToStreamAsync(string encodingName, string input, CompressionLevel level, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously compresses the given <see cref="byte" /> array to a stream using the specified compression level.
        /// </summary>
        /// <param name="type">The type of compression to be used.</param>
        /// <param name="bytes">The <see cref="byte" /> array to compress.</param>
        /// <param name="level">The compression level to use.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous compression operation, which wraps the stream containing the compressed data.
        /// </returns>
        /// <exception cref="CompressionNotAvailableException">Thrown when no compression implementation is registered for the specified type.</exception>
        Task<Stream> CompressToStreamAsync(CompressionType type, byte[] bytes, CompressionLevel level, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously compresses the given <see cref="byte" /> array to a stream using the specified compression level.
        /// </summary>
        /// <param name="encodingName">The name of the encoding. e.g., "br", "deflate", "snappy", "zstd", "gzip" or "none" (used internally to indicate no compression; does not correspond to a real encoding header).</param>
        /// <param name="bytes">The <see cref="byte" /> array to compress.</param>
        /// <param name="level">The compression level to use.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous compression operation, which wraps the stream containing the compressed data.
        /// </returns>
        /// <exception cref="CompressionNotAvailableException">Thrown when no matching implementation is found or if no applicable service available for the specified type.</exception>
        Task<Stream> CompressToStreamAsync(string encodingName, byte[] bytes, CompressionLevel level, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously compresses the given <paramref name="stream"/> to another stream using the specified compression level.
        /// </summary>
        /// <param name="type">The type of compression to be used.</param>
        /// <param name="stream">The stream to compress.</param>
        /// <param name="level">The compression level to use.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous compression operation, which wraps the stream containing the compressed data.
        /// </returns>
        /// <remarks>Closes the underlying <paramref name="stream"/> after the operation is completed.</remarks>
        /// <exception cref="CompressionNotAvailableException">Thrown when no compression implementation is registered for the specified type.</exception>
        Task<Stream> CompressToStreamAsync(CompressionType type, Stream stream, CompressionLevel level, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously compresses the given <paramref name="stream"/> to another stream using the specified compression level.
        /// </summary>
        /// <param name="encodingName">The name of the encoding. e.g., "br", "deflate", "snappy", "zstd", "gzip" or "none" (used internally to indicate no compression; does not correspond to a real encoding header).</param>
        /// <param name="stream">The stream to compress.</param>
        /// <param name="level">The compression level to use.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous compression operation, which wraps the stream containing the compressed data.
        /// </returns>
        /// <remarks>Closes the underlying <paramref name="stream"/> after the operation is completed.</remarks>
        /// <exception cref="CompressionNotAvailableException">Thrown when no matching implementation is found or if no applicable service available for the specified type.</exception>
        Task<Stream> CompressToStreamAsync(string encodingName, Stream stream, CompressionLevel level, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously compresses the given <paramref name="stream"/> to another stream using the specified compression level, with an option to leave the stream open after the operation.
        /// </summary>
        /// <param name="type">The type of compression to be used.</param>
        /// <param name="stream">The stream to compress.</param>
        /// <param name="level">The compression level to use.</param>
        /// <param name="leaveOpen">Indicates whether the <paramref name="stream"/> should remain open after the operation.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous compression operation, which wraps the stream containing the compressed data.
        /// </returns>
        /// <remarks>
        /// The returned stream is positioned at the beginning. Caller is responsible for disposing the stream.
        /// </remarks>
        /// <exception cref="CompressionNotAvailableException">Thrown when no compression implementation is registered for the specified type.</exception>
        Task<Stream> CompressToStreamAsync(CompressionType type, Stream stream, CompressionLevel level, bool leaveOpen, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously compresses the given <paramref name="stream"/> to another stream using the specified compression level, with an option to leave the stream open after the operation.
        /// </summary>
        /// <param name="encodingName">The name of the encoding. e.g., "br", "deflate", "snappy", "zstd", "gzip" or "none" (used internally to indicate no compression; does not correspond to a real encoding header).</param>
        /// <param name="stream">The stream to compress.</param>
        /// <param name="level">The compression level to use.</param>
        /// <param name="leaveOpen">Indicates whether the <paramref name="stream"/> should remain open after the operation.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous compression operation, which wraps the stream containing the compressed data.
        /// </returns>
        /// <remarks>
        /// The returned stream is positioned at the beginning. Caller is responsible for disposing the stream.
        /// </remarks>
        /// <exception cref="CompressionNotAvailableException">Thrown when no matching implementation is found or if no applicable service available for the specified type.</exception>
        Task<Stream> CompressToStreamAsync(string encodingName, Stream stream, CompressionLevel level, bool leaveOpen, CancellationToken cancellationToken = default);

        /// <summary>
        /// Compresses the given input string.
        /// </summary>
        /// <param name="type">The type of compression to be used.</param>
        /// <param name="input">The string to compress.</param>
        /// <returns>A <see cref="byte" /> array containing the compressed data.</returns>
        /// <exception cref="CompressionNotAvailableException">Thrown when no compression implementation is registered for the specified type.</exception>
        byte[] Compress(CompressionType type, string input);

        /// <summary>
        /// Compresses the given input string.
        /// </summary>
        /// <param name="encodingName">The name of the encoding. e.g., "br", "deflate", "snappy", "zstd", "gzip" or "none" (used internally to indicate no compression; does not correspond to a real encoding header).</param>
        /// <param name="input">The string to compress.</param>
        /// <returns>A <see cref="byte" /> array containing the compressed data.</returns>
        /// <exception cref="CompressionNotAvailableException">Thrown when no matching implementation is found or if no applicable service available for the specified type.</exception>
        byte[] Compress(string encodingName, string input);

        /// <summary>
        /// Compresses the given <see cref="byte" /> array.
        /// </summary>
        /// <param name="type">The type of compression to be used.</param>
        /// <param name="bytes">The <see cref="byte" /> array to compress.</param>
        /// <returns>A <see cref="byte" /> array containing the compressed data.</returns>
        /// <exception cref="CompressionNotAvailableException">Thrown when no compression implementation is registered for the specified type.</exception>
        byte[] Compress(CompressionType type, byte[] bytes);

        /// <summary>
        /// Compresses the given <see cref="byte" /> array.
        /// </summary>
        /// <param name="encodingName">The name of the encoding. e.g., "br", "deflate", "snappy", "zstd", "gzip" or "none" (used internally to indicate no compression; does not correspond to a real encoding header).</param>
        /// <param name="bytes">The <see cref="byte" /> array to compress.</param>
        /// <returns>A <see cref="byte" /> array containing the compressed data.</returns>
        /// <exception cref="CompressionNotAvailableException">Thrown when no matching implementation is found or if no applicable service available for the specified type.</exception>
        byte[] Compress(string encodingName, byte[] bytes);

        /// <summary>
        /// Compresses the given <paramref name="stream"/>.
        /// </summary>
        /// <param name="type">The type of compression to be used.</param>
        /// <param name="stream">The stream to compress.</param>
        /// <returns>A <see cref="byte" /> array containing the compressed data.</returns>
        /// <remarks>Closes the underlying <paramref name="stream" /> after the operation is completed.</remarks>
        /// <exception cref="CompressionNotAvailableException">Thrown when no compression implementation is registered for the specified type.</exception>
        byte[] Compress(CompressionType type, Stream stream);

        /// <summary>
        /// Compresses the given <paramref name="stream"/>.
        /// </summary>
        /// <param name="encodingName">The name of the encoding. e.g., "br", "deflate", "snappy", "zstd", "gzip" or "none" (used internally to indicate no compression; does not correspond to a real encoding header).</param>
        /// <param name="stream">The stream to compress.</param>
        /// <returns>A <see cref="byte" /> array containing the compressed data.</returns>
        /// <remarks>Closes the underlying <paramref name="stream" /> after the operation is completed.</remarks>
        /// <exception cref="CompressionNotAvailableException">Thrown when no matching implementation is found or if no applicable service available for the specified type.</exception>
        byte[] Compress(string encodingName, Stream stream);

        /// <summary>
        /// Compresses the given <paramref name="stream"/>, with an option to leave the stream open after the operation.
        /// </summary>
        /// <param name="type">The type of compression to be used.</param>
        /// <param name="stream">The stream to compress.</param>
        /// <param name="leaveOpen">Indicates whether the <paramref name="stream" /> should remain open after the operation.</param>
        /// <returns>A <see cref="byte" /> array containing the compressed data.</returns>
        /// <exception cref="CompressionNotAvailableException">Thrown when no compression implementation is registered for the specified type.</exception>
        byte[] Compress(CompressionType type, Stream stream, bool leaveOpen);

        /// <summary>
        /// Compresses the given <paramref name="stream"/>, with an option to leave the stream open after the operation.
        /// </summary>
        /// <param name="encodingName">The name of the encoding. e.g., "br", "deflate", "snappy", "zstd", "gzip" or "none" (used internally to indicate no compression; does not correspond to a real encoding header).</param>
        /// <param name="stream">The stream to compress.</param>
        /// <param name="leaveOpen">Indicates whether the <paramref name="stream" /> should remain open after the operation.</param>
        /// <returns>A <see cref="byte" /> array containing the compressed data.</returns>
        /// <exception cref="CompressionNotAvailableException">Thrown when no matching implementation is found or if no applicable service available for the specified type.</exception>
        byte[] Compress(string encodingName, Stream stream, bool leaveOpen);

        /// <summary>
        /// Compresses the given input string using the specified compression level.
        /// </summary>
        /// <param name="type">The type of compression to be used.</param>
        /// <param name="input">The string to compress.</param>
        /// <param name="level">The compression level to use.</param>
        /// <returns>A <see cref="byte" /> array containing the compressed data.</returns>
        /// <exception cref="CompressionNotAvailableException">Thrown when no compression implementation is registered for the specified type.</exception>
        byte[] Compress(CompressionType type, string input, CompressionLevel level);

        /// <summary>
        /// Compresses the given input string using the specified compression level.
        /// </summary>
        /// <param name="encodingName">The name of the encoding. e.g., "br", "deflate", "snappy", "zstd", "gzip" or "none" (used internally to indicate no compression; does not correspond to a real encoding header).</param>
        /// <param name="input">The string to compress.</param>
        /// <param name="level">The compression level to use.</param>
        /// <returns>A <see cref="byte" /> array containing the compressed data.</returns>
        /// <exception cref="CompressionNotAvailableException">Thrown when no matching implementation is found or if no applicable service available for the specified type.</exception>
        byte[] Compress(string encodingName, string input, CompressionLevel level);

        /// <summary>
        /// Compresses the given <see cref="byte" /> array using the specified compression level.
        /// </summary>
        /// <param name="type">The type of compression to be used.</param>
        /// <param name="bytes">The <see cref="byte" /> array to compress.</param>
        /// <param name="level">The compression level to use.</param>
        /// <returns>A <see cref="byte" /> array containing the compressed data.</returns>
        /// <exception cref="CompressionNotAvailableException">Thrown when no compression implementation is registered for the specified type.</exception>
        byte[] Compress(CompressionType type, byte[] bytes, CompressionLevel level);

        /// <summary>
        /// Compresses the given <see cref="byte" /> array using the specified compression level.
        /// </summary>
        /// <param name="encodingName">The name of the encoding. e.g., "br", "deflate", "snappy", "zstd", "gzip" or "none" (used internally to indicate no compression; does not correspond to a real encoding header).</param>
        /// <param name="bytes">The <see cref="byte" /> array to compress.</param>
        /// <param name="level">The compression level to use.</param>
        /// <returns>A <see cref="byte" /> array containing the compressed data.</returns>
        /// <exception cref="CompressionNotAvailableException">Thrown when no matching implementation is found or if no applicable service available for the specified type.</exception>
        byte[] Compress(string encodingName, byte[] bytes, CompressionLevel level);

        /// <summary>
        /// Compresses the given <paramref name="stream"/> using the specified compression level.
        /// </summary>
        /// <param name="type">The type of compression to be used.</param>
        /// <param name="stream">The stream to compress.</param>
        /// <param name="level">The compression level to use.</param>
        /// <returns>A <see cref="byte" /> array containing the compressed data.</returns>
        /// <remarks>Closes the underlying <paramref name="stream"/> after the operation is completed.</remarks>
        /// <exception cref="CompressionNotAvailableException">Thrown when no compression implementation is registered for the specified type.</exception>
        byte[] Compress(CompressionType type, Stream stream, CompressionLevel level);

        /// <summary>
        /// Compresses the given <paramref name="stream"/> using the specified compression level.
        /// </summary>
        /// <param name="encodingName">The name of the encoding. e.g., "br", "deflate", "snappy", "zstd", "gzip" or "none" (used internally to indicate no compression; does not correspond to a real encoding header).</param>
        /// <param name="stream">The stream to compress.</param>
        /// <param name="level">The compression level to use.</param>
        /// <returns>A <see cref="byte" /> array containing the compressed data.</returns>
        /// <remarks>Closes the underlying <paramref name="stream"/> after the operation is completed.</remarks>
        /// <exception cref="CompressionNotAvailableException">Thrown when no matching implementation is found or if no applicable service available for the specified type.</exception>
        byte[] Compress(string encodingName, Stream stream, CompressionLevel level);

        /// <summary>
        /// Compresses the given <paramref name="stream"/> using the specified compression level, with an option to leave the stream open after the operation.
        /// </summary>
        /// <param name="type">The type of compression to be used.</param>
        /// <param name="stream">The stream to compress.</param>
        /// <param name="level">The compression level to use.</param>
        /// <param name="leaveOpen">Indicates whether the <paramref name="stream"/> should remain open after the operation.</param>
        /// <returns>A <see cref="byte" /> array containing the compressed data.</returns>
        /// <exception cref="CompressionNotAvailableException">Thrown when no compression implementation is registered for the specified type.</exception>
        byte[] Compress(CompressionType type, Stream stream, CompressionLevel level, bool leaveOpen);

        /// <summary>
        /// Compresses the given <paramref name="stream"/> using the specified compression level, with an option to leave the stream open after the operation.
        /// </summary>
        /// <param name="encodingName">The name of the encoding. e.g., "br", "deflate", "snappy", "zstd", "gzip" or "none" (used internally to indicate no compression; does not correspond to a real encoding header).</param>
        /// <param name="stream">The stream to compress.</param>
        /// <param name="level">The compression level to use.</param>
        /// <param name="leaveOpen">Indicates whether the <paramref name="stream"/> should remain open after the operation.</param>
        /// <returns>A <see cref="byte" /> array containing the compressed data.</returns>
        /// <exception cref="CompressionNotAvailableException">Thrown when no matching implementation is found or if no applicable service available for the specified type.</exception>
        byte[] Compress(string encodingName, Stream stream, CompressionLevel level, bool leaveOpen);

        /// <summary>
        /// Compresses the given input string to a stream.
        /// </summary>
        /// <param name="type">The type of compression to be used.</param>
        /// <param name="input">The string to compress.</param>
        /// <returns>A stream containing the compressed data.</returns>
        /// <remarks>
        /// The returned stream is positioned at the beginning. Caller is responsible for disposing the stream.
        /// </remarks>
        /// <exception cref="CompressionNotAvailableException">Thrown when no compression implementation is registered for the specified type.</exception>
        Stream CompressToStream(CompressionType type, string input);

        /// <summary>
        /// Compresses the given input string to a stream.
        /// </summary>
        /// <param name="encodingName">The name of the encoding. e.g., "br", "deflate", "snappy", "zstd", "gzip" or "none" (used internally to indicate no compression; does not correspond to a real encoding header).</param>
        /// <param name="input">The string to compress.</param>
        /// <returns>A stream containing the compressed data.</returns>
        /// <remarks>
        /// The returned stream is positioned at the beginning. Caller is responsible for disposing the stream.
        /// </remarks>
        /// <exception cref="CompressionNotAvailableException">Thrown when no matching implementation is found or if no applicable service available for the specified type.</exception>
        Stream CompressToStream(string encodingName, string input);

        /// <summary>
        /// Compresses the given <see cref="byte" /> array to a stream.
        /// </summary>
        /// <param name="type">The type of compression to be used.</param>
        /// <param name="bytes">The <see cref="byte" /> array to compress.</param>
        /// <returns>A stream containing the compressed data.</returns>
        /// <remarks>
        /// The returned stream is positioned at the beginning. Caller is responsible for disposing the stream.
        /// </remarks>
        /// <exception cref="CompressionNotAvailableException">Thrown when no compression implementation is registered for the specified type.</exception>
        Stream CompressToStream(CompressionType type, byte[] bytes);

        /// <summary>
        /// Compresses the given <see cref="byte" /> array to a stream.
        /// </summary>
        /// <param name="encodingName">The name of the encoding. e.g., "br", "deflate", "snappy", "zstd", "gzip" or "none" (used internally to indicate no compression; does not correspond to a real encoding header).</param>
        /// <param name="bytes">The <see cref="byte" /> array to compress.</param>
        /// <returns>A stream containing the compressed data.</returns>
        /// <remarks>
        /// The returned stream is positioned at the beginning. Caller is responsible for disposing the stream.
        /// </remarks>
        /// <exception cref="CompressionNotAvailableException">Thrown when no matching implementation is found or if no applicable service available for the specified type.</exception>
        Stream CompressToStream(string encodingName, byte[] bytes);

        /// <summary>
        /// Compresses the given <paramref name="stream"/> to another stream using the specified compression level.
        /// </summary>
        /// <param name="type">The type of compression to be used.</param>
        /// <param name="stream">The stream to compress.</param>
        /// <returns>A stream containing the compressed data.</returns>
        /// <remarks>
        /// Closes the underlying <paramref name="stream"/> after the operation is completed.
        /// <para>
        /// The returned stream is positioned at the beginning. Caller is responsible for disposing the stream.
        /// </para>
        /// </remarks>
        /// <exception cref="CompressionNotAvailableException">Thrown when no compression implementation is registered for the specified type.</exception>
        Stream CompressToStream(CompressionType type, Stream stream);

        /// <summary>
        /// Compresses the given <paramref name="stream"/> to another stream using the specified compression level.
        /// </summary>
        /// <param name="encodingName">The name of the encoding. e.g., "br", "deflate", "snappy", "zstd", "gzip" or "none" (used internally to indicate no compression; does not correspond to a real encoding header).</param>
        /// <param name="stream">The stream to compress.</param>
        /// <returns>A stream containing the compressed data.</returns>
        /// <remarks>
        /// Closes the underlying <paramref name="stream"/> after the operation is completed.
        /// <para>
        /// The returned stream is positioned at the beginning. Caller is responsible for disposing the stream.
        /// </para>
        /// </remarks>
        /// <exception cref="CompressionNotAvailableException">Thrown when no matching implementation is found or if no applicable service available for the specified type.</exception>
        Stream CompressToStream(string encodingName, Stream stream);

        /// <summary>
        /// Compresses the given <paramref name="stream"/> to another stream, with an option to leave the stream open after the operation.
        /// </summary>
        /// <param name="type">The type of compression to be used.</param>
        /// <param name="stream">The stream to compress.</param>
        /// <param name="leaveOpen">Indicates whether the <paramref name="stream"/> should remain open after the operation.</param>
        /// <returns>A stream containing the compressed data.</returns>
        /// <remarks>
        /// The returned stream is positioned at the beginning. Caller is responsible for disposing the stream.
        /// </remarks>
        /// <exception cref="CompressionNotAvailableException">Thrown when no compression implementation is registered for the specified type.</exception>
        Stream CompressToStream(CompressionType type, Stream stream, bool leaveOpen);

        /// <summary>
        /// Compresses the given <paramref name="stream"/> to another stream, with an option to leave the stream open after the operation.
        /// </summary>
        /// <param name="encodingName">The name of the encoding. e.g., "br", "deflate", "snappy", "zstd", "gzip" or "none" (used internally to indicate no compression; does not correspond to a real encoding header).</param>
        /// <param name="stream">The stream to compress.</param>
        /// <param name="leaveOpen">Indicates whether the <paramref name="stream"/> should remain open after the operation.</param>
        /// <returns>A stream containing the compressed data.</returns>
        /// <remarks>
        /// The returned stream is positioned at the beginning. Caller is responsible for disposing the stream.
        /// </remarks>
        /// <exception cref="CompressionNotAvailableException">Thrown when no matching implementation is found or if no applicable service available for the specified type.</exception>
        Stream CompressToStream(string encodingName, Stream stream, bool leaveOpen);

        /// <summary>
        /// Compresses the given input string to a stream using the specified compression level.
        /// </summary>
        /// <param name="type">The type of compression to be used.</param>
        /// <param name="input">The string to compress.</param>
        /// <param name="level">The compression level to use.</param>
        /// <returns>A stream containing the compressed data.</returns>
        /// <remarks>
        /// The returned stream is positioned at the beginning. Caller is responsible for disposing the stream.
        /// </remarks>
        /// <exception cref="CompressionNotAvailableException">Thrown when no compression implementation is registered for the specified type.</exception>
        Stream CompressToStream(CompressionType type, string input, CompressionLevel level);

        /// <summary>
        /// Compresses the given input string to a stream using the specified compression level.
        /// </summary>
        /// <param name="encodingName">The name of the encoding. e.g., "br", "deflate", "snappy", "zstd", "gzip" or "none" (used internally to indicate no compression; does not correspond to a real encoding header).</param>
        /// <param name="input">The string to compress.</param>
        /// <param name="level">The compression level to use.</param>
        /// <returns>A stream containing the compressed data.</returns>
        /// <remarks>
        /// The returned stream is positioned at the beginning. Caller is responsible for disposing the stream.
        /// </remarks>
        /// <exception cref="CompressionNotAvailableException">Thrown when no matching implementation is found or if no applicable service available for the specified type.</exception>
        Stream CompressToStream(string encodingName, string input, CompressionLevel level);

        /// <summary>
        /// Compresses the given <see cref="byte" /> array to a stream using the specified compression level.
        /// </summary>
        /// <param name="type">The type of compression to be used.</param>
        /// <param name="bytes">The <see cref="byte" /> array to compress.</param>
        /// <param name="level">The compression level to use.</param>
        /// <returns>A stream containing the compressed data.</returns>
        /// <remarks>
        /// The returned stream is positioned at the beginning. Caller is responsible for disposing the stream.
        /// </remarks>
        /// <exception cref="CompressionNotAvailableException">Thrown when no compression implementation is registered for the specified type.</exception>
        Stream CompressToStream(CompressionType type, byte[] bytes, CompressionLevel level);

        /// <summary>
        /// Compresses the given <see cref="byte" /> array to a stream using the specified compression level.
        /// </summary>
        /// <param name="encodingName">The name of the encoding. e.g., "br", "deflate", "snappy", "zstd", "gzip" or "none" (used internally to indicate no compression; does not correspond to a real encoding header).</param>
        /// <param name="bytes">The <see cref="byte" /> array to compress.</param>
        /// <param name="level">The compression level to use.</param>
        /// <returns>A stream containing the compressed data.</returns>
        /// <remarks>
        /// The returned stream is positioned at the beginning. Caller is responsible for disposing the stream.
        /// </remarks>
        /// <exception cref="CompressionNotAvailableException">Thrown when no matching implementation is found or if no applicable service available for the specified type.</exception>
        Stream CompressToStream(string encodingName, byte[] bytes, CompressionLevel level);

        /// <summary>
        /// Compresses the given <paramref name="stream"/> to another stream using the specified compression level.
        /// </summary>
        /// <param name="type">The type of compression to be used.</param>
        /// <param name="stream">The stream to compress.</param>
        /// <param name="level">The compression level to use.</param>
        /// <returns>A stream containing the compressed data.</returns>
        /// <remarks>
        /// Closes the underlying <paramref name="stream"/> after the operation is completed.
        /// <para>
        /// The returned stream is positioned at the beginning. Caller is responsible for disposing the stream.
        /// </para>
        /// </remarks>
        /// <exception cref="CompressionNotAvailableException">Thrown when no compression implementation is registered for the specified type.</exception>
        Stream CompressToStream(CompressionType type, Stream stream, CompressionLevel level);

        /// <summary>
        /// Compresses the given <paramref name="stream"/> to another stream using the specified compression level.
        /// </summary>
        /// <param name="encodingName">The name of the encoding. e.g., "br", "deflate", "snappy", "zstd", "gzip" or "none" (used internally to indicate no compression; does not correspond to a real encoding header).</param>
        /// <param name="stream">The stream to compress.</param>
        /// <param name="level">The compression level to use.</param>
        /// <returns>A stream containing the compressed data.</returns>
        /// <remarks>
        /// Closes the underlying <paramref name="stream"/> after the operation is completed.
        /// <para>
        /// The returned stream is positioned at the beginning. Caller is responsible for disposing the stream.
        /// </para>
        /// </remarks>
        /// <exception cref="CompressionNotAvailableException">Thrown when no matching implementation is found or if no applicable service available for the specified type.</exception>
        Stream CompressToStream(string encodingName, Stream stream, CompressionLevel level);

        /// <summary>
        /// Compresses the given <paramref name="stream"/> to another stream using the specified compression level, with an option to leave the stream open after the operation.
        /// </summary>
        /// <param name="type">The type of compression to be used.</param>
        /// <param name="stream">The stream to compress.</param>
        /// <param name="level">The compression level to use.</param>
        /// <param name="leaveOpen">Indicates whether the <paramref name="stream"/> should remain open after the operation.</param>
        /// <returns>A stream containing the compressed data.</returns>
        /// <remarks>
        /// The returned stream is positioned at the beginning. Caller is responsible for disposing the stream.
        /// </remarks>
        /// <exception cref="CompressionNotAvailableException">Thrown when no compression implementation is registered for the specified type.</exception>
        Stream CompressToStream(CompressionType type, Stream stream, CompressionLevel level, bool leaveOpen);

        /// <summary>
        /// Compresses the given <paramref name="stream"/> to another stream using the specified compression level, with an option to leave the stream open after the operation.
        /// </summary>
        /// <param name="encodingName">The name of the encoding. e.g., "br", "deflate", "snappy", "zstd", "gzip" or "none" (used internally to indicate no compression; does not correspond to a real encoding header).</param>
        /// <param name="stream">The stream to compress.</param>
        /// <param name="level">The compression level to use.</param>
        /// <param name="leaveOpen">Indicates whether the <paramref name="stream"/> should remain open after the operation.</param>
        /// <returns>A stream containing the compressed data.</returns>
        /// <remarks>
        /// The returned stream is positioned at the beginning. Caller is responsible for disposing the stream.
        /// </remarks>
        /// <exception cref="CompressionNotAvailableException">Thrown when no matching implementation is found or if no applicable service available for the specified type.</exception>
        Stream CompressToStream(string encodingName, Stream stream, CompressionLevel level, bool leaveOpen);

        /// <summary>
        /// Asynchronously decompresses the given <see cref="byte" /> array.
        /// </summary>
        /// <param name="type">The type of compression to be used.</param>
        /// <param name="bytes">The <see cref="byte" /> array to decompress.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous decompression operation, which wraps the <see cref="byte" /> array containing the decompressed data.</returns>
        /// <exception cref="CompressionNotAvailableException">Thrown when no compression implementation is registered for the specified type.</exception>
        Task<byte[]> DecompressAsync(CompressionType type, byte[] bytes, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously decompresses the given <see cref="byte" /> array.
        /// </summary>
        /// <param name="encodingName">The name of the encoding. e.g., "br", "deflate", "snappy", "zstd", "gzip" or "none" (used internally to indicate no compression; does not correspond to a real encoding header).</param>
        /// <param name="bytes">The <see cref="byte" /> array to decompress.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous decompression operation, which wraps the <see cref="byte" /> array containing the decompressed data.</returns>
        /// <exception cref="CompressionNotAvailableException">Thrown when no matching implementation is found or if no applicable service available for the specified type.</exception>
        Task<byte[]> DecompressAsync(string encodingName, byte[] bytes, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously decompresses the given <paramref name="stream"/>.
        /// </summary>
        /// <param name="type">The type of compression to be used.</param>
        /// <param name="stream">The stream to decompress.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous decompression operation, which wraps the <see cref="byte" /> array containing the decompressed data.</returns>
        /// <remarks>Closes the underlying <paramref name="stream"/> after the operation is completed.</remarks>
        /// <exception cref="CompressionNotAvailableException">Thrown when no compression implementation is registered for the specified type.</exception>
        Task<byte[]> DecompressAsync(CompressionType type, Stream stream, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously decompresses the given <paramref name="stream"/>.
        /// </summary>
        /// <param name="encodingName">The name of the encoding. e.g., "br", "deflate", "snappy", "zstd", "gzip" or "none" (used internally to indicate no compression; does not correspond to a real encoding header).</param>
        /// <param name="stream">The stream to decompress.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous decompression operation, which wraps the <see cref="byte" /> array containing the decompressed data.</returns>
        /// <remarks>Closes the underlying <paramref name="stream"/> after the operation is completed.</remarks>
        /// <exception cref="CompressionNotAvailableException">Thrown when no matching implementation is found or if no applicable service available for the specified type.</exception>
        Task<byte[]> DecompressAsync(string encodingName, Stream stream, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously decompresses the given <paramref name="stream"/>, with an option to leave the stream open after the operation.
        /// </summary>
        /// <param name="type">The type of compression to be used.</param>
        /// <param name="stream">The stream to decompress.</param>
        /// <param name="leaveOpen">Indicates whether the <paramref name="stream"/> should remain open after the operation.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous decompression operation, which wraps the <see cref="byte" /> array containing the decompressed data.</returns>
        /// <exception cref="CompressionNotAvailableException">Thrown when no compression implementation is registered for the specified type.</exception>
        Task<byte[]> DecompressAsync(CompressionType type, Stream stream, bool leaveOpen, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously decompresses the given <paramref name="stream"/>, with an option to leave the stream open after the operation.
        /// </summary>
        /// <param name="encodingName">The name of the encoding. e.g., "br", "deflate", "snappy", "zstd", "gzip" or "none" (used internally to indicate no compression; does not correspond to a real encoding header).</param>
        /// <param name="stream">The stream to decompress.</param>
        /// <param name="leaveOpen">Indicates whether the <paramref name="stream"/> should remain open after the operation.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous decompression operation, which wraps the <see cref="byte" /> array containing the decompressed data.</returns>
        /// <exception cref="CompressionNotAvailableException">Thrown when no matching implementation is found or if no applicable service available for the specified type.</exception>
        Task<byte[]> DecompressAsync(string encodingName, Stream stream, bool leaveOpen, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously decompresses the given <see cref="byte" /> array to a stream.
        /// </summary>
        /// <param name="type">The type of compression to be used.</param>
        /// <param name="bytes">The <see cref="byte" /> array to decompress.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous decompression operation, which wraps the stream containing the decompressed stream.</returns>
        /// <remarks>
        /// The returned stream is positioned at the beginning. Caller is responsible for disposing the stream.
        /// </remarks>
        /// <exception cref="CompressionNotAvailableException">Thrown when no compression implementation is registered for the specified type.</exception>
        Task<Stream> DecompressToStreamAsync(CompressionType type, byte[] bytes, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously decompresses the given <see cref="byte" /> array to a stream.
        /// </summary>
        /// <param name="encodingName">The name of the encoding. e.g., "br", "deflate", "snappy", "zstd", "gzip" or "none" (used internally to indicate no compression; does not correspond to a real encoding header).</param>
        /// <param name="bytes">The <see cref="byte" /> array to decompress.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous decompression operation, which wraps the stream containing the decompressed stream.</returns>
        /// <remarks>
        /// The returned stream is positioned at the beginning. Caller is responsible for disposing the stream.
        /// </remarks>
        /// <exception cref="CompressionNotAvailableException">Thrown when no matching implementation is found or if no applicable service available for the specified type.</exception>
        Task<Stream> DecompressToStreamAsync(string encodingName, byte[] bytes, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously decompresses the given <paramref name="stream"/> to another stream.
        /// </summary>
        /// <param name="type">The type of compression to be used.</param>
        /// <param name="stream">The stream to decompress.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous decompression operation, which wraps the stream containing the decompressed stream.</returns>
        /// <remarks>
        /// Closes the underlying <paramref name="stream"/> after the operation is completed.
        /// <para>
        /// The returned stream is positioned at the beginning. Caller is responsible for disposing the stream.
        /// </para>
        /// </remarks>
        /// <exception cref="CompressionNotAvailableException">Thrown when no compression implementation is registered for the specified type.</exception>
        Task<Stream> DecompressToStreamAsync(CompressionType type, Stream stream, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously decompresses the given <paramref name="stream"/> to another stream.
        /// </summary>
        /// <param name="encodingName">The name of the encoding. e.g., "br", "deflate", "snappy", "zstd", "gzip" or "none" (used internally to indicate no compression; does not correspond to a real encoding header).</param>
        /// <param name="stream">The stream to decompress.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous decompression operation, which wraps the stream containing the decompressed stream.</returns>
        /// <remarks>
        /// Closes the underlying <paramref name="stream"/> after the operation is completed.
        /// <para>
        /// The returned stream is positioned at the beginning. Caller is responsible for disposing the stream.
        /// </para>
        /// </remarks>
        /// <exception cref="CompressionNotAvailableException">Thrown when no matching implementation is found or if no applicable service available for the specified type.</exception>
        Task<Stream> DecompressToStreamAsync(string encodingName, Stream stream, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously decompresses the given <paramref name="stream"/> to another stream, with an option to leave the stream open after the operation.
        /// </summary>
        /// <param name="type">The type of compression to be used.</param>
        /// <param name="stream">The stream to decompress.</param>
        /// <param name="leaveOpen">Indicates whether the <paramref name="stream"/> should remain open after the operation.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous decompression operation, which wraps the stream containing the decompressed stream.</returns>
        /// <remarks>
        /// The returned stream is positioned at the beginning. Caller is responsible for disposing the stream.
        /// </remarks>
        /// <exception cref="CompressionNotAvailableException">Thrown when no compression implementation is registered for the specified type.</exception>
        Task<Stream> DecompressToStreamAsync(CompressionType type, Stream stream, bool leaveOpen, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously decompresses the given <paramref name="stream"/> to another stream, with an option to leave the stream open after the operation.
        /// </summary>
        /// <param name="encodingName">The name of the encoding. e.g., "br", "deflate", "snappy", "zstd", "gzip" or "none" (used internally to indicate no compression; does not correspond to a real encoding header).</param>
        /// <param name="stream">The stream to decompress.</param>
        /// <param name="leaveOpen">Indicates whether the <paramref name="stream"/> should remain open after the operation.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous decompression operation, which wraps the stream containing the decompressed stream.</returns>
        /// <remarks>
        /// The returned stream is positioned at the beginning. Caller is responsible for disposing the stream.
        /// </remarks>
        /// <exception cref="CompressionNotAvailableException">Thrown when no matching implementation is found or if no applicable service available for the specified type.</exception>
        Task<Stream> DecompressToStreamAsync(string encodingName, Stream stream, bool leaveOpen, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously decompresses the content of an HTTP request to a stream.
        /// </summary>
        /// <param name="type">The type of compression to be used.</param>
        /// <param name="httpContent">The HTTP content to decompress.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous decompression operation, which wraps the stream containing the decompressed stream.</returns>
        /// <remarks>
        /// Closes the underlying <paramref name="httpContent"/> stream after the operation is completed.
        /// <para>
        /// The returned stream is positioned at the beginning. Caller is responsible for disposing the stream.
        /// </para>
        /// </remarks>
        /// <exception cref="CompressionNotAvailableException">Thrown when no compression implementation is registered for the specified type.</exception>
        Task<Stream> DecompressToStreamAsync(CompressionType type, HttpContent httpContent, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously decompresses the content of an HTTP request to a stream.
        /// </summary>
        /// <param name="encodingName">The name of the encoding. e.g., "br", "deflate", "snappy", "zstd", "gzip" or "none" (used internally to indicate no compression; does not correspond to a real encoding header).</param>
        /// <param name="httpContent">The HTTP content to decompress.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous decompression operation, which wraps the stream containing the decompressed stream.</returns>
        /// <remarks>
        /// Closes the underlying <paramref name="httpContent"/> stream after the operation is completed.
        /// <para>
        /// The returned stream is positioned at the beginning. Caller is responsible for disposing the stream.
        /// </para>
        /// </remarks>
        /// <exception cref="CompressionNotAvailableException">Thrown when no matching implementation is found or if no applicable service available for the specified type.</exception>
        Task<Stream> DecompressToStreamAsync(string encodingName, HttpContent httpContent, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously decompresses the content of an HTTP request to a stream, with an option to leave the stream open after the operation.
        /// </summary>
        /// <param name="type">The type of compression to be used.</param>
        /// <param name="httpContent">The HTTP content to decompress.</param>
        /// <param name="leaveOpen">Indicates whether the <paramref name="httpContent"/> stream should remain open after the operation.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous decompression operation, which wraps the stream containing the decompressed stream.</returns>
        /// <remarks>
        /// The returned stream is positioned at the beginning. Caller is responsible for disposing the stream.
        /// </remarks>
        /// <exception cref="CompressionNotAvailableException">Thrown when no compression implementation is registered for the specified type.</exception>
        Task<Stream> DecompressToStreamAsync(CompressionType type, HttpContent httpContent, bool leaveOpen, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously decompresses the content of an HTTP request to a stream, with an option to leave the stream open after the operation.
        /// </summary>
        /// <param name="encodingName">The name of the encoding. e.g., "br", "deflate", "snappy", "zstd", "gzip" or "none" (used internally to indicate no compression; does not correspond to a real encoding header).</param>
        /// <param name="httpContent">The HTTP content to decompress.</param>
        /// <param name="leaveOpen">Indicates whether the <paramref name="httpContent"/> stream should remain open after the operation.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous decompression operation, which wraps the stream containing the decompressed stream.</returns>
        /// <remarks>
        /// The returned stream is positioned at the beginning. Caller is responsible for disposing the stream.
        /// </remarks>
        /// <exception cref="CompressionNotAvailableException">Thrown when no matching implementation is found or if no applicable service available for the specified type.</exception>
        Task<Stream> DecompressToStreamAsync(string encodingName, HttpContent httpContent, bool leaveOpen, CancellationToken cancellationToken = default);

        /// <summary>
        /// Decompresses the given <see cref="byte" /> array.
        /// </summary>
        /// <param name="type">The type of compression to be used.</param>
        /// <param name="bytes">The <see cref="byte" /> array to decompress.</param>
        /// <returns>A <see cref="byte" /> array containing the decompressed data.</returns>
        /// <exception cref="CompressionNotAvailableException">Thrown when no compression implementation is registered for the specified type.</exception>
        byte[] Decompress(CompressionType type, byte[] bytes);

        /// <summary>
        /// Decompresses the given <see cref="byte" /> array.
        /// </summary>
        /// <param name="encodingName">The name of the encoding. e.g., "br", "deflate", "snappy", "zstd", "gzip" or "none" (used internally to indicate no compression; does not correspond to a real encoding header).</param>
        /// <param name="bytes">The <see cref="byte" /> array to decompress.</param>
        /// <returns>A <see cref="byte" /> array containing the decompressed data.</returns>
        /// <exception cref="CompressionNotAvailableException">Thrown when no matching implementation is found or if no applicable service available for the specified type.</exception>
        byte[] Decompress(string encodingName, byte[] bytes);

        /// <summary>
        /// Decompresses the given <paramref name="stream"/>.
        /// </summary>
        /// <param name="type">The type of compression to be used.</param>
        /// <param name="stream">The stream to decompress.</param>
        /// <returns>A <see cref="byte" /> array containing the decompressed data.</returns>
        /// <remarks>Closes the underlying <paramref name="stream"/> after the operation is completed.</remarks>
        /// <exception cref="CompressionNotAvailableException">Thrown when no compression implementation is registered for the specified type.</exception>
        byte[] Decompress(CompressionType type, Stream stream);

        /// <summary>
        /// Decompresses the given <paramref name="stream"/>.
        /// </summary>
        /// <param name="encodingName">The name of the encoding. e.g., "br", "deflate", "snappy", "zstd", "gzip" or "none" (used internally to indicate no compression; does not correspond to a real encoding header).</param>
        /// <param name="stream">The stream to decompress.</param>
        /// <returns>A <see cref="byte" /> array containing the decompressed data.</returns>
        /// <remarks>Closes the underlying <paramref name="stream"/> after the operation is completed.</remarks>
        /// <exception cref="CompressionNotAvailableException">Thrown when no matching implementation is found or if no applicable service available for the specified type.</exception>
        byte[] Decompress(string encodingName, Stream stream);

        /// <summary>
        /// Decompresses the given <paramref name="stream"/>, with an option to leave the stream open after the operation.
        /// </summary>
        /// <param name="type">The type of compression to be used.</param>
        /// <param name="stream">The stream to decompress.</param>
        /// <param name="leaveOpen">Indicates whether the <paramref name="stream"/> should remain open after the operation.</param>
        /// <returns>A <see cref="byte" /> array containing the decompressed data.</returns>
        /// <exception cref="CompressionNotAvailableException">Thrown when no compression implementation is registered for the specified type.</exception>
        byte[] Decompress(CompressionType type, Stream stream, bool leaveOpen);

        /// <summary>
        /// Decompresses the given <paramref name="stream"/>, with an option to leave the stream open after the operation.
        /// </summary>
        /// <param name="encodingName">The name of the encoding. e.g., "br", "deflate", "snappy", "zstd", "gzip" or "none" (used internally to indicate no compression; does not correspond to a real encoding header).</param>
        /// <param name="stream">The stream to decompress.</param>
        /// <param name="leaveOpen">Indicates whether the <paramref name="stream"/> should remain open after the operation.</param>
        /// <returns>A <see cref="byte" /> array containing the decompressed data.</returns>
        /// <exception cref="CompressionNotAvailableException">Thrown when no matching implementation is found or if no applicable service available for the specified type.</exception>
        byte[] Decompress(string encodingName, Stream stream, bool leaveOpen);
    }
}