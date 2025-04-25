using Ogu.Compressions;
using Ogu.Compressions.Abstractions;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers all available compression implementations and the <see cref="ICompressionProvider"/> into the <see cref="IServiceCollection"/>.
        /// Optionally allows configuration of shared <see cref="CompressionOptions"/> for each compression type.
        /// </summary>
        /// <param name="services">The service collection to register the compression services into.</param>
        /// <param name="opts">An optional delegate to configure shared compression options (e.g., level, buffer size) for all implementations.</param>
        /// <returns>The same <see cref="IServiceCollection"/> instance for chaining.</returns>
        public static IServiceCollection AddCompressions(this IServiceCollection services, Action<CompressionOptions> opts = null)
        {
            services.AddOptions();

            if (opts != null)
            {
                services.Configure<BrotliCompressionOptions>(opts);
                services.Configure<DeflateCompressionOptions>(opts);
                services.Configure<SnappyCompressionOptions>(opts);
                services.Configure<ZstdCompressionOptions>(opts);
                services.Configure<GzipCompressionOptions>(opts);
            }

            services.AddSingleton<IBrotliCompression, BrotliCompression>();
            services.AddSingleton<ICompression>(sp => sp.GetRequiredService<IBrotliCompression>());

            services.AddSingleton<IDeflateCompression, DeflateCompression>();
            services.AddSingleton<ICompression>(sp => sp.GetRequiredService<IDeflateCompression>());

            services.AddSingleton<ISnappyCompression, SnappyCompression>();
            services.AddSingleton<ICompression>(sp => sp.GetRequiredService<ISnappyCompression>());

            services.AddSingleton<IZstdCompression, ZstdCompression>();
            services.AddSingleton<ICompression>(sp => sp.GetRequiredService<IZstdCompression>());

            services.AddSingleton<IGzipCompression, GzipCompression>();
            services.AddSingleton<ICompression>(sp => sp.GetRequiredService<IGzipCompression>());

            services.AddSingleton<INoneCompression, NoneCompression>();
            services.AddSingleton<ICompression>(sp => sp.GetRequiredService<INoneCompression>());

            services.AddSingleton<ICompressionProvider, CompressionProvider>();

            return services;
        }
    }
}