using Microsoft.EntityFrameworkCore;

namespace ProvaConceitoSignalR.Infra
{
    public static class InfraService
    {

        public static IServiceCollection AddAllServices(this IServiceCollection services,
                                                               IConfiguration configuration)
        {
            string? connectionString = configuration["ConnectionStrings:DefaultConnection"];

            services.AddDbContext<Contexto>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRequisicaoRepository, RequisicaoRepository>();
            services.AddScoped<IRequisicaoService, RequisicaoService>();

            return services;
        }
    }
}
