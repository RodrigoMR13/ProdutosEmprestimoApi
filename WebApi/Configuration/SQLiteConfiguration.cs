using Application.Common.Interfaces;
using Infrastructure.Persistence;

namespace WebApi.Configuration
{
    public static class SQLiteConfiguration
    {
        public static IServiceCollection AddSQLiteConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ProdutosEmprestimoDatabase") 
                ?? throw new InvalidOperationException("Connection String 'ProdutosEmprestimoDatabase' Not Found");

            services.AddSingleton(
                new ProdutosEmprestimoDatabaseService(connectionString)
            );

            services.AddScoped<IProdutoEmprestimoRepository, ProdutoEmprestimoRepository>();

            return services;
        }
    }
}
