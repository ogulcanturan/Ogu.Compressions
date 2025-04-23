using Ogu.Compressions.Abstractions;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Ogu.Compressions
{
    public class DecompressionHandler : DelegatingHandler
    {
        private readonly ICompressionFactory _compressionFactory;

        public DecompressionHandler(ICompressionFactory compressionFactory)
        {
            _compressionFactory = compressionFactory;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            if (IsContentEncodingHeaderAbsent(response))
            {
                return response;
            }

            var streamContent = await GetDecompressedStreamContentOrDefaultAsync(_compressionFactory, response, cancellationToken);

            if (streamContent == null)
            {
                return response;
            }

            response.Content.Headers.ContentEncoding.Clear();

            foreach (var header in response.Content.Headers)
            {
                streamContent.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            response.Content = streamContent;

            return response;
        }

        private static bool IsContentEncodingHeaderAbsent(HttpResponseMessage response)
        {
            return (response?.Content.Headers.ContentEncoding.Count ?? 0) == 0;
        }

        private static async Task<StreamContent> GetDecompressedStreamContentOrDefaultAsync(ICompressionFactory compressionFactory, HttpResponseMessage response, CancellationToken cancellationToken)
        {
            StreamContent streamContent = null;

            foreach (var encodingName in response.Content.Headers.ContentEncoding.Reverse())
            {
                if (!CompressionHelper.TryConvertEncodingNameToCompressionType(encodingName, out var compressionType) || compressionType == CompressionType.None)
                {
                    return null;
                }
                
                var compression = compressionFactory.Get(compressionType);

                if (compression == null)
                {
                    return null;
                }

                streamContent = await DecompressToStreamAsync(compression, streamContent ?? response.Content, cancellationToken);
            }

            return streamContent;
        }

        private static async Task<StreamContent> DecompressToStreamAsync(ICompression compression, HttpContent httpContent, CancellationToken cancellationToken = default)
        {
            var stream = await compression.DecompressToStreamAsync(httpContent, cancellationToken);

            return new StreamContent(stream);
        }
    }
}