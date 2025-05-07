using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Ogu.Compressions.Abstractions
{
    /// <summary>
    /// A custom HTTP handler for decompressing response content based on the 'Content-Encoding' header.
    /// </summary>
    /// <remarks>
    /// This handler is responsible for checking the response's 'Content-Encoding' header and decompressing the content
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

            if (!IsContentEncodingHeaderAbsent(response))
            {
                await DecompressAsync(_compressionProvider, response, cancellationToken);
            }

            return response;
        }

        private static bool IsContentEncodingHeaderAbsent(HttpResponseMessage response)
        {
            return (response?.Content.Headers.ContentEncoding.Count ?? 0) == 0;
        }

        private static async Task DecompressAsync(ICompressionProvider compressionProvider, HttpResponseMessage response, CancellationToken cancellationToken)
        {
            if (!TryGetCompressions(compressionProvider, response.Content.Headers.ContentEncoding, out var compressorsWithEncodingNames))
            {
                return;
            }

            Stream stream = null;

            try
            {
                foreach (var compression in compressorsWithEncodingNames.Reverse())
                {
                    if (stream != null)
                    {
                        stream = await compression.DecompressToStreamAsync(stream, cancellationToken);

                        continue;
                    }

                    stream = await compression.DecompressToStreamAsync(await response.Content.ReadAsStreamAsync(
#if NET5_0_OR_GREATER
                    cancellationToken
#endif
                    ), cancellationToken);
                }
            }
            catch
            {
                if (stream != null)
                {
#if NETSTANDARD2_0
                    stream.Dispose();
#else
                    await stream.DisposeAsync();
#endif
                }

                throw;
            }

            response.Content.Headers.ContentEncoding.Clear();
            response.Content.Headers.ContentLength = null;
            response.Content.Dispose();

            var streamContent = new StreamContent(stream);

            streamContent.Headers.ContentLength = stream.Length;

            foreach (var header in response.Content.Headers)
            {
                streamContent.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            response.Content = streamContent;
        }

        private static bool TryGetCompressions(ICompressionProvider compressionProvider, IEnumerable<string> encodingNames, out IEnumerable<ICompression> compressions)
        {
            var compressionList = new List<ICompression>();

            compressions = compressionList;

            foreach (var encodingName in encodingNames)
            {
                var compression = compressionProvider.GetCompression(encodingName);

                if (compression == null || compression.Type == CompressionType.None)
                {
                    return false;
                }

                compressionList.Add(compression);
            }

            return true;
        }
    }
}