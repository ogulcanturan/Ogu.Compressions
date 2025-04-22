using Ogu.Compressions;
using Ogu.Compressions.Abstractions;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBrotliCompression(this IServiceCollection services, Action<BrotliCompressionOptions> opts = null)
        {
            services.AddOptions();

            if (opts != null)
            {
                services.Configure(opts);
            }

            services.AddSingleton<IBrotliCompression, BrotliCompression>();
            
            return services;
        }

        public static IServiceCollection AddDeflateCompression(this IServiceCollection services, Action<DeflateCompressionOptions> opts = null)
        {
            services.AddOptions();

            if (opts != null)
            {
                services.Configure(opts);
            }

            services.AddSingleton<IDeflateCompression, DeflateCompression>();

            return services;
        }

        public static IServiceCollection AddSnappyCompression(this IServiceCollection services, Action<SnappyCompressionOptions> opts = null)
        {
            services.AddOptions();

            if (opts != null)
            {
                services.Configure(opts);
            }

            services.AddSingleton<ISnappyCompression, SnappyCompression>();

            return services;
        }

        public static IServiceCollection AddZstdCompression(this IServiceCollection services, Action<ZstdCompressionOptions> opts = null)
        {
            services.AddOptions();

            if (opts != null)
            {
                services.Configure(opts);
            }

            services.AddSingleton<IZstdCompression, ZstdCompression>();

            return services;
        }

        public static IServiceCollection AddGzipCompression(this IServiceCollection services, Action<GzipCompressionOptions> opts = null)
        {
            services.AddOptions();

            if (opts != null)
            {
                services.Configure(opts);
            }

            services.AddSingleton<IGzipCompression, GzipCompression>();

            return services;
        }

        public static IServiceCollection AddNoneCompression(this IServiceCollection services)
        {
            services.AddSingleton<INoneCompression, NoneCompression>();

            return services;
        }


        public static IServiceCollection AddCompressionFactory(this IServiceCollection services)
        {
            services.AddOptions();

            services.AddSingleton<ICompressionFactory, CompressionFactory>();

            return services;
        }

        public static IServiceCollection AddCompressions(this IServiceCollection services, Action<CompressionFactoryOptions> opts = null)
        {
            services.AddOptions();

            if (opts != null)
            {
                var options = new CompressionFactoryOptions();
                opts.Invoke(options);

                services.Configure<BrotliCompressionOptions>(brotliOpts =>
                {
                    brotliOpts.Level = options.Level;
                    brotliOpts.BufferSize = options.BufferSize;
                });

                services.Configure<DeflateCompressionOptions>(deflateOpts =>
                {
                    deflateOpts.Level = options.Level;
                    deflateOpts.BufferSize = options.BufferSize;
                });

                services.Configure<SnappyCompressionOptions>(snappyOpts =>
                {
                    snappyOpts.Level = options.Level;
                    snappyOpts.BufferSize = options.BufferSize;
                });

                services.Configure<ZstdCompressionOptions>(zstdOpts =>
                {
                    zstdOpts.Level = options.Level;
                    zstdOpts.BufferSize = options.BufferSize;
                });

                services.Configure<GzipCompressionOptions>(gzipOpts =>
                {
                    gzipOpts.Level = options.Level;
                    gzipOpts.BufferSize = options.BufferSize;
                });
            }

            services.AddSingleton<IBrotliCompression, BrotliCompression>();
            services.AddSingleton<IDeflateCompression, DeflateCompression>();
            services.AddSingleton<ISnappyCompression, SnappyCompression>();
            services.AddSingleton<IZstdCompression, ZstdCompression>();
            services.AddSingleton<IGzipCompression, GzipCompression>();
            services.AddSingleton<INoneCompression, NoneCompression>();
            services.AddSingleton<ICompressionFactory, CompressionFactory>(sp => new CompressionFactory(
                sp.GetRequiredService<IBrotliCompression>(),
                sp.GetRequiredService<IDeflateCompression>(),
                sp.GetRequiredService<ISnappyCompression>(),
                sp.GetRequiredService<IZstdCompression>(),
                sp.GetRequiredService<IGzipCompression>(),
                sp.GetRequiredService<INoneCompression>()));

            return services;
        }
    }
}