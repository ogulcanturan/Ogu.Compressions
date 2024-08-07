using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Ogu.Compressions
{
    public static class StreamExtensions
    {
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