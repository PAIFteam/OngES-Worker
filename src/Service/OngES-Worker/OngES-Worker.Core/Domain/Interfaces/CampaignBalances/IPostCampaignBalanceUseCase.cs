using Core.Application.UseCases.CampaignBalances;

namespace  Core.Domain.Interfaces.CampaignBalances
{
    public interface IPostCampaignBalanceUseCase
    {
        public Task<PostCampaignBalanceOutput> ExecuteAsync(PostCampaignBalanceInput input);
    }
}
