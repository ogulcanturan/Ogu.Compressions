using Ogu.Compressions;
using Ogu.Compressions.Abstractions;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the <see cref="IBrotliCompression"/> implementation and related dependencies into the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The service collection to register the compression service into.</param>
        /// <param name="opts">An optional delegate to configure compression options (e.g., level, buffer size).</param>
        /// <returns>The same <see cref="IServiceCollection"/> instance for chaining.</returns>
        public static IServiceCollection AddBrotliCompression(this IServiceCollection services, Action<BrotliCompressionOptions> opts = null)
        {
            services.AddOptions();

            if (opts != null)
            {
                services.Configure(opts);
            }

            services.AddSingleton<IBrotliCompression, BrotliCompression>();
            services.AddSingleton<ICompression>(sp => sp.GetRequiredService<IBrotliCompression>());

            return services;
        }

        /// <summary>
        /// Registers the <see cref="IDeflateCompression"/> implementation and related dependencies into the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The service collection to register the compression service into.</param>
        /// <param name="opts">An optional delegate to configure compression options (e.g., level, buffer size).</param>
        /// <returns>The same <see cref="IServiceCollection"/> instance for chaining.</returns>
        public static IServiceCollection AddDeflateCompression(this IServiceCollection services, Action<DeflateCompressionOptions> opts = null)
        {
            services.AddOptions();

            if (opts != null)
            {
                services.Configure(opts);
            }

            services.AddSingleton<IDeflateCompression, DeflateCompression>();
            services.AddSingleton<ICompression>(sp => sp.GetRequiredService<IDeflateCompression>());

            return services;
        }

        /// <summary>
        /// Registers the <see cref="ISnappyCompression"/> implementation and related dependencies into the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The service collection to register the compression service into.</param>
        /// <param name="opts">An optional delegate to configure compression options (e.g., level, buffer size).</param>
        /// <returns>The same <see cref="IServiceCollection"/> instance for chaining.</returns>
        public static IServiceCollection AddSnappyCompression(this IServiceCollection services, Action<SnappyCompressionOptions> opts = null)
        {
            services.AddOptions();

            if (opts != null)
            {
                services.Configure(opts);
            }

            services.AddSingleton<ISnappyCompression, SnappyCompression>();
            services.AddSingleton<ICompression>(sp => sp.GetRequiredService<ISnappyCompression>());

            return services;
        }

        /// <summary>
        /// Registers the <see cref="IZstdCompression"/> implementation and related dependencies into the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The service collection to register the compression service into.</param>
        /// <param name="opts">An optional delegate to configure compression options (e.g., level, buffer size).</param>
        /// <returns>The same <see cref="IServiceCollection"/> instance for chaining.</returns>
        public static IServiceCollection AddZstdCompression(this IServiceCollection services, Action<ZstdCompressionOptions> opts = null)
        {
            services.AddOptions();

            if (opts != null)
            {
                services.Configure(opts);
            }

            services.AddSingleton<IZstdCompression, ZstdCompression>();
            services.AddSingleton<ICompression>(sp => sp.GetRequiredService<IZstdCompression>());

            return services;
        }

        /// <summary>
        /// Registers the <see cref="IGzipCompression"/> implementation and related dependencies into the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The service collection to register the compression service into.</param>
        /// <param name="opts">An optional delegate to configure compression options (e.g., level, buffer size).</param>
        /// <returns>The same <see cref="IServiceCollection"/> instance for chaining.</returns>
        public static IServiceCollection AddGzipCompression(this IServiceCollection services, Action<GzipCompressionOptions> opts = null)
        {
            services.AddOptions();

            if (opts != null)
            {
                services.Configure(opts);
            }

            services.AddSingleton<IGzipCompression, GzipCompression>();
            services.AddSingleton<ICompression>(sp => sp.GetRequiredService<IGzipCompression>());

            return services;
        }

        /// <summary>
        /// Registers the <see cref="INoneCompression"/> implementation and related dependencies into the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The service collection to register the compression service into.</param>
        /// <returns>The same <see cref="IServiceCollection"/> instance for chaining.</returns>
        public static IServiceCollection AddNoneCompression(this IServiceCollection services)
        {
            services.AddSingleton<INoneCompression, NoneCompression>();
            services.AddSingleton<ICompression>(sp => sp.GetRequiredService<INoneCompression>());

            return services;
        }

        /// <summary>
        /// Registers the default <see cref="ICompressionProvider"/> implementation and related dependencies into the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The service collection to add the compression provider to.</param>
        /// <returns>The same <see cref="IServiceCollection"/> instance for chaining.</returns>
        public static IServiceCollection AddCompressionProvider(this IServiceCollection services)
        {
            services.AddOptions();

            services.AddSingleton<ICompressionProvider, CompressionProvider>();

            return services;
        }

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