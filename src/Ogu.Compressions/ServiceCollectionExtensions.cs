using Ogu.Compressions;
using Ogu.Compressions.Abstractions;
using Ogu.Compressions.Brotli.Native;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers all available compression implementations and the <see cref="ICompressionProvider"/> into the <see cref="IServiceCollection"/>.
        /// Allows optional configuration via <see cref="CompressionRegistrationOptions"/>, including whether to use native brotli and shared compression options
        /// which affect all compressions (brotli, deflate, snappy, zstandard, gzip, none).
        /// </summary>
        /// <param name="services">The service collection to register the compression services into.</param>
        /// <param name="opts">
        /// An optional delegate to specify whether to use native brotli instead of managed and to
        /// configure shared compression options (e.g., level, buffer size) for all implementations.
        /// </param>
        /// <returns>The same <see cref="IServiceCollection"/> instance for chaining.</returns>
        public static IServiceCollection AddCompressions(this IServiceCollection services, Action<CompressionRegistrationOptions> opts = null)
        {
            services.AddOptions();

            var options = new CompressionRegistrationOptions();
            opts?.Invoke(options);

            if (options.CompressionOptions != null)
            {
                _ = options.UseNativeBrotli
                    ? services.Configure<NativeBrotliCompressionOptions>(options.CompressionOptions)
                    : services.Configure<BrotliCompressionOptions>(options.CompressionOptions);
                services.Configure<DeflateCompressionOptions>(options.CompressionOptions);
                services.Configure<SnappyCompressionOptions>(options.CompressionOptions);
                services.Configure<ZstdCompressionOptions>(options.CompressionOptions);
                services.Configure<GzipCompressionOptions>(options.CompressionOptions);
                services.Configure<NoneCompressionOptions>(options.CompressionOptions);
            }

            services.Add(options.UseNativeBrotli
                ? ServiceDescriptor.Singleton<IBrotliCompression, NativeBrotliCompression>()
                : ServiceDescriptor.Singleton<IBrotliCompression, BrotliCompression>());
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