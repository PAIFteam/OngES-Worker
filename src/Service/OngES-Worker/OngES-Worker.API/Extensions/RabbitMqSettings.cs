
using Core.Domain.Entities.RabbitMQ;
using MassTransit;



namespace API.Extensions
{
    public static class RabbitMqSettings
    {
        public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitSettings = new RabbitMqConfigurationSettings();

            configuration
                .GetSection(RabbitMqConfigurationSettings.OPTION_KEY)
                .Bind(rabbitSettings);
           
            services.AddScoped(_ => rabbitSettings);
            services.AddConsumer(configuration);
           

            return services;
        }
       
    }
}
