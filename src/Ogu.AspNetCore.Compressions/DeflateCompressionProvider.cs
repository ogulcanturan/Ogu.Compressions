using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Options;
using System.IO;
using System.IO.Compression;
using Ogu.Compressions.Abstractions;

namespace Ogu.AspNetCore.Compressions
{
    public class DeflateCompressionProvider : ICompressionProvider
    {
        private readonly DeflateCompressionProviderOptions _options;

        public DeflateCompressionProvider(IOptions<DeflateCompressionProviderOptions> opts)
        {
            _options = opts.Value;
        }

        public string EncodingName { get; } = EncodingNames.Deflate;

        public bool SupportsFlush { get; } = true;

        public Stream CreateStream(Stream outputStream)
        {
            return new DeflateStream(outputStream, _options.Level, leaveOpen: true);
        }
    }
}