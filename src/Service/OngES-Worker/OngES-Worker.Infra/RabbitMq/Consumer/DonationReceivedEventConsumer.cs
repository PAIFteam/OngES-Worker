using MassTransit;
using Microsoft.Extensions.Logging;
using Core.Domain.Interfaces.CampaignBalances;
using Core.Application.UseCases.CampaignBalances;
namespace Core.Entities.RabbitMq
{
    public class DonationReceivedEventConsumer : IConsumer<DonationReceivedMessage>
    {
        private readonly IPostCampaignBalanceUseCase _postCampaignBalanceUseCase;
        private readonly ILogger<DonationReceivedEventConsumer> _logger;

        public DonationReceivedEventConsumer(
            IPostCampaignBalanceUseCase postCampaignBalanceUseCase,
            ILogger<DonationReceivedEventConsumer> logger
            )
        {
            _postCampaignBalanceUseCase = postCampaignBalanceUseCase;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<DonationReceivedMessage> context)
        {

            _logger.LogInformation("DonationReceivedEventConsumer - Iniciando consumo da mensagem de DonationReceivedMessage");
            _logger.LogInformation("......");
            _logger.LogInformation("Enviar dados para processadora de Doação");
            _logger.LogInformation("......");
            _logger.LogInformation("......");

            await Task.CompletedTask;

            PostCampaignBalanceOutput _processedInput = await _postCampaignBalanceUseCase.ExecuteAsync(
                new PostCampaignBalanceInput (context.Message.IdCampaign ,context.Message.ValueDonation));
         

            _logger.LogInformation($"Enviando dados para execução de doação, " +
                $" IdCampaign ({context.Message.IdCampaign}) e ValueDonation {context.Message.ValueDonation.ToString()}");
     
            
        }
    }
}
