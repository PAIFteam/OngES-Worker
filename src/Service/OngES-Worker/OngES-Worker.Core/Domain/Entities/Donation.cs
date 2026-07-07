using System;

namespace  Core.Domain.Entities
{
    public class Donation
    {
        public int IdCampaign { get; set; }
        public decimal Value { get; set; }

        public Donation()
        {
        }

   

        public Donation(int idCampaign,  decimal value)
        {
            IdCampaign = idCampaign;
    
            Value = value;
        }
    }
}
