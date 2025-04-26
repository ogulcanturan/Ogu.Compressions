using Ogu.Compressions;
using Ogu.Compressions.Abstractions;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the <see cref="INoneCompression"/> implementation and related dependencies into the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The service collection to register the compression service into.</param>
        /// <param name="opts">An optional delegate to configure compression options (e.g., level, buffer size).</param>
        /// <returns>The same <see cref="IServiceCollection"/> instance for chaining.</returns>
        public static IServiceCollection AddNoneCompression(this IServiceCollection services, Action<NoneCompressionOptions> opts = null)
        {
            services.AddOptions();

            if (opts != null)
            {
                services.Configure(opts);
            }

            services.AddSingleton<INoneCompression, NoneCompression>();
            services.AddSingleton<ICompression>(sp => sp.GetRequiredService<INoneCompression>());

            return services;
        }
    }
}