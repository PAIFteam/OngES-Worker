using Core.Application.UseCases.CampaignBalances.PostCampaignBalance;
using Core.Domain.Interfaces.CampaignBalances;
using Infra.Data.Repositories.CampaignBalances;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infra.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection services)
        {
            //Registro do MediaR
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(
                    Assembly.GetExecutingAssembly(),
                    Assembly.GetAssembly(typeof(PostCampaignBalanceUseCase))!
                    );
            });

            //Registro dos Repositorios
            services.AddScoped<IPostCampaignBalanceRepository, PostCampaignBalanceRepository>();    
            
            //Registro dos UseCases
            services.AddScoped<PostCampaignBalanceUseCase>();

            return services;
        }
    }
}
