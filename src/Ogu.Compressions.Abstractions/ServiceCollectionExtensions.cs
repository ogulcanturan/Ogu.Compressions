using Ogu.Compressions.Abstractions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the default <see cref="ICompressionProvider"/> implementation and related dependencies into the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The service collection to add the compression provider to.</param>
        /// <returns>The same <see cref="IServiceCollection"/> instance for chaining.</returns>
        public static IServiceCollection AddCompressionProvider(this IServiceCollection services)
        {
            services.AddSingleton<ICompressionProvider, CompressionProvider>();

            return services;
        }
    }
}