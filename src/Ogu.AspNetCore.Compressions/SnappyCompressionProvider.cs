using Microsoft.AspNetCore.ResponseCompression;
using Ogu.Compressions.Abstractions;
using Snappier;
using System.IO;
using System.IO.Compression;

namespace Ogu.AspNetCore.Compressions
{
    public class SnappyCompressionProvider : ICompressionProvider
    {
        public string EncodingName => EncodingNames.Snappy;

        public bool SupportsFlush => true;

        public Stream CreateStream(Stream outputStream)
        {
            return new SnappyStream(outputStream, CompressionMode.Compress, leaveOpen: true);
        }
    }
}