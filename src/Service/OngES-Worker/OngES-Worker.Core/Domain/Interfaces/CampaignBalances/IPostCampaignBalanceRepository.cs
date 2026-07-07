using Core.Domain.Entities;

namespace  Core.Domain.Interfaces.CampaignBalances
{
    public interface IPostCampaignBalanceRepository
    {
        Task<int> PostCampaignBalanceAsync(Core.Domain.Entities.Donation donation);
        

    }
}
