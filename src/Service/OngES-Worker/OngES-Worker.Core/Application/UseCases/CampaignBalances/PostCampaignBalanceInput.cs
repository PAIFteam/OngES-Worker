using System;
using Core.Domain.Entities;

namespace  Core.Application.UseCases.CampaignBalances
{
    public class PostCampaignBalanceInput
    {
        public int IdCampaign { get; set; }
       
        public decimal Value { get; set; }
        public PostCampaignBalanceInput(int idCampaign, decimal value)
        {
            IdCampaign = idCampaign;
            Value = value;
        }
        public Core.Domain.Entities.Donation MapToDonation()
        {
            return new Core.Domain.Entities.Donation(
                IdCampaign,
                Value
            );
        }
    }
}
