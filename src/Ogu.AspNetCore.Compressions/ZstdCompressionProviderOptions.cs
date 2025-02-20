using Microsoft.Extensions.Options;
using Ogu.Compressions.Abstractions;

namespace Ogu.AspNetCore.Compressions
{
    public sealed class ZstdCompressionProviderOptions : CompressionOptions, IOptions<ZstdCompressionProviderOptions>
    {
        public ZstdCompressionProviderOptions() { }

        public ZstdCompressionProviderOptions(CompressionOptions options)
        {
            Level = options.Level;
            BufferSize = options.BufferSize;
        }

        /// <inheritdoc />
        ZstdCompressionProviderOptions IOptions<ZstdCompressionProviderOptions>.Value => this;
    }
}