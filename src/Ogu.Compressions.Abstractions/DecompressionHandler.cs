using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Ogu.Compressions.Abstractions
{
    /// <summary>
    /// A custom HTTP handler for decompressing response content based on the Content-Encoding header.
    /// </summary>
    /// <remarks>
    /// This handler is responsible for checking the response's Content-Encoding header and decompressing the content
    /// if the appropriate encoding is detected. It uses the <see cref="ICompressionProvider"/> to get the necessary 
    /// compression implementation for decompressing the content.
    /// </remarks>
    public sealed class DecompressionHandler : DelegatingHandler
    {
        private readonly ICompressionProvider _compressionProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="DecompressionHandler"/> class.
        /// </summary>
        /// <param name="compressionProvider">The compression provider used for retrieving compression implementations.</param>
        /// <remarks>
        /// The <paramref name="compressionProvider"/> is injected to provide the necessary compression algorithms for 
        /// decompressing the response content based on the Content-Encoding header.
        /// </remarks>
        public DecompressionHandler(ICompressionProvider compressionProvider)
        {
            _compressionProvider = compressionProvider;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            if (IsContentEncodingHeaderAbsent(response))
            {
                return response;
            }

            var streamContent = await GetDecompressedStreamContentOrDefaultAsync(_compressionProvider, response, cancellationToken);

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

        private static async Task<StreamContent> GetDecompressedStreamContentOrDefaultAsync(ICompressionProvider compressionProvider, HttpResponseMessage response, CancellationToken cancellationToken)
        {
            if (!TryGetCompressions(compressionProvider, response.Content.Headers.ContentEncoding, out var compressors))
            {
                return null;
            }

            StreamContent streamContent = null;

            foreach (var compressor in compressors.Reverse())
            {
                streamContent = await DecompressToStreamAsync(compressor, streamContent ?? response.Content, cancellationToken);
            }

            return streamContent;
        }

        private static bool TryGetCompressions(ICompressionProvider compressionProvider, IEnumerable<string> encodingNames, out IEnumerable<ICompression> compressions)
        {
            var compressionList = new List<ICompression>();
            compressions = compressionList;

            foreach (var encodingName in encodingNames)
            {
                if (!CompressionHelper.TryConvertEncodingNameToCompressionType(encodingName, out var compressionType) || compressionType == CompressionType.None)
                {
                    return false;
                }

                var compression = compressionProvider.GetCompression(compressionType);

                if (compression == null)
                {
                    return false;
                }

                compressionList.Add(compression);
            }

            return true;
        }

        private static async Task<StreamContent> DecompressToStreamAsync(ICompression compression, HttpContent httpContent, CancellationToken cancellationToken = default)
        {
            var stream = await compression.DecompressToStreamAsync(httpContent, cancellationToken);

            return new StreamContent(stream);
        }
    }
}