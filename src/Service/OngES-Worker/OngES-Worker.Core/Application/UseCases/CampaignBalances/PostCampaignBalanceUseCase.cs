
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Core.Domain.Interfaces.CampaignBalances;


namespace  Core.Application.UseCases.CampaignBalances.PostCampaignBalance

{
    public class PostCampaignBalanceUseCase : IPostCampaignBalanceUseCase       
    {
        private readonly IPostCampaignBalanceRepository _postCampaignBalanceRepository;
        private readonly ILogger<PostCampaignBalanceUseCase> _logger;
        private readonly IConfiguration _configuration;

        public PostCampaignBalanceUseCase(
            IPostCampaignBalanceRepository postCampaignBalanceRepository,
            ILogger<PostCampaignBalanceUseCase> logger,
            IConfiguration configuration
        )
        { 
            _postCampaignBalanceRepository = postCampaignBalanceRepository;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<PostCampaignBalanceOutput> ExecuteAsync(PostCampaignBalanceInput input)
        {
            _logger.LogInformation("Starting PutDonationUseCase.ExecuteAsync");

            try
            {
                
                Core.Domain.Entities.Donation donation = input.MapToDonation();
                int idDonation = await _postCampaignBalanceRepository.PostCampaignBalanceAsync(donation);

                PostCampaignBalanceOutput outPut = new PostCampaignBalanceOutput
                {
                   
                    Result = true,
                    Message = "Valor doado da campanha atualizada com sucesso!",
                    Exception = null
                };

                return outPut;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao registrar campanha");
                return new PostCampaignBalanceOutput
                {
                    Result = false,
                    Message = "Ocorreu um erro de Runtime Interno",
                    Exception = ex
                };
            }
        }

        
    }
}
