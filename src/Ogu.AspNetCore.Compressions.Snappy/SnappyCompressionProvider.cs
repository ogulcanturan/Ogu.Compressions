using Ogu.Compressions.Abstractions;
using Snappier;
using System.IO;
using System.IO.Compression;
using ICompressionProvider = Microsoft.AspNetCore.ResponseCompression.ICompressionProvider;

namespace Ogu.AspNetCore.Compressions
{
    /// <summary>
    /// Provides compression provider for the Snappy (snappy) compression, 
    /// </summary>
    public class SnappyCompressionProvider : ICompressionProvider
    {
        public string EncodingName => CompressionDefaults.EncodingNames.Snappy;

        public bool SupportsFlush => true;

        public Stream CreateStream(Stream outputStream)
        {
            return new SnappyStream(outputStream, CompressionMode.Compress, leaveOpen: true);
        }
    }
}