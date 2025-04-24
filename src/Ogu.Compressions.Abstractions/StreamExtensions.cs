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
                stream.CopyTo(memoryStream);

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
        public static async Task<byte[]> ReadAllBytesAsync(this Stream stream, bool leaveOpen, CancellationToken cancellationToken = default)
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
#if NETSTANDARD2_0
                await stream.CopyToAsync(memoryStream).ConfigureAwait(false);
#else
                await stream.CopyToAsync(memoryStream, cancellationToken).ConfigureAwait(false);
#endif
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