using Ogu.Compressions;
using Ogu.Compressions.Abstractions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
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
    }
}