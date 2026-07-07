using System;
using Core.Domain.Entities;
using Core.Domain.Entities.Base;

namespace  Core.Application.UseCases.CampaignBalances
{
    public class PostCampaignBalanceOutput : OutPutBase
    {
       
        public Core.Domain.Entities.Donation MapToDonation()
        {
            return new Core.Domain.Entities.Donation(
               
            );
        }
    }
}
