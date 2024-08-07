using System.IO.Compression;

namespace Ogu.Compressions
{
    public class CompressionOptions
    {
        /// <summary>
        /// What level of compression to use for the stream. The default is Fastest.
        /// </summary>
        public CompressionLevel Level { get; set; } = CompressionLevel.Fastest;

        /// <summary>
        /// The size, in bytes, of the buffer. This value must be greater than zero. The default size is 81920.
        /// </summary>
        public int BufferSize { get; set; } = 81920;

    }
}