using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Ogu.Compressions.Abstractions
{
    /// <summary>
    /// Provides extension methods for the <see cref="Stream"/> class.
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// Reads all bytes from the specified stream and returns them as a <see cref="byte"/> array.
        /// </summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="leaveOpen">If true, the stream will not be disposed after reading.</param>
        /// <returns>A <see cref="byte"/> array containing the bytes read from the stream.</returns>
        public static byte[] ReadAllBytes(this Stream stream, bool leaveOpen)
        {
            return stream.ReadAllBytes(leaveOpen, CompressionDefaults.BufferSize);
        }

        /// <summary>
        /// Reads all bytes from the specified stream and returns them as a <see cref="byte"/> array.
        /// </summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="leaveOpen">If true, the stream will not be disposed after reading.</param>
        /// <param name="bufferSize">The size, in bytes, of the buffer to use. The default value is 81920 bytes and must be greater than zero.</param>
        /// <returns>A <see cref="byte"/> array containing the bytes read from the stream.</returns>
        public static byte[] ReadAllBytes(this Stream stream, bool leaveOpen, int bufferSize)
        {
            if (stream is MemoryStream memoryStream)
            {
                var bytes = memoryStream.ToArray();

                if (!leaveOpen)
                {
                    memoryStream.Dispose();
                }

                return bytes;
            }

            using (memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream, bufferSize);

                var bytes = memoryStream.ToArray();

                if (!leaveOpen)
                {
                    stream.Dispose();
                }

                return bytes;
            }
        }

        /// <summary>
        /// Reads all bytes from the specified stream asynchronously and returns them as a <see cref="byte"/> array.
        /// </summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="leaveOpen">If true, the stream will not be disposed after reading.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation, which wraps the <see cref="byte"/> array containing the bytes read from the stream.</returns>
        public static Task<byte[]> ReadAllBytesAsync(this Stream stream, bool leaveOpen, CancellationToken cancellationToken = default)
        {
            return stream.ReadAllBytesAsync(leaveOpen, CompressionDefaults.BufferSize, cancellationToken);
        }

        /// <summary>
        /// Reads all bytes from the specified stream asynchronously and returns them as a <see cref="byte"/> array.
        /// </summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="leaveOpen">If true, the stream will not be disposed after reading.</param>
        /// <param name="bufferSize">The size, in bytes, of the buffer to use. The default value is 81920 bytes and must be greater than zero.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation, which wraps the <see cref="byte"/> array containing the bytes read from the stream.</returns>
        public static async Task<byte[]> ReadAllBytesAsync(this Stream stream, bool leaveOpen, int bufferSize, CancellationToken cancellationToken)
        {
            if (stream is MemoryStream memoryStream)
            {
                var bytes = memoryStream.ToArray();

                if (!leaveOpen)
                {
#if NETSTANDARD2_0
                    memoryStream.Dispose();
#else
                    await memoryStream.DisposeAsync().ConfigureAwait(false);
#endif
                }

                return bytes;
            }

            using (memoryStream = new MemoryStream())
            {
                await stream.CopyToAsync(memoryStream, bufferSize, cancellationToken).ConfigureAwait(false);

                var bytes = memoryStream.ToArray();

                if (!leaveOpen)
                {
#if NETSTANDARD2_0
                    stream.Dispose();
#else
                    await stream.DisposeAsync().ConfigureAwait(false);
#endif
                }

                return bytes;
            }
        }
    }
}