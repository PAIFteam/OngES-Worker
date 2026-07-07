using MassTransit.Testing.MessageObservers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.RabbitMq
{
    public class DonationReceivedMessage
    {
        public int IdCampaign { get; set; }
        public decimal ValueDonation { get; set; }


        public DonationReceivedMessage(int idCampaign, decimal valueDonation)
        {
            IdCampaign = idCampaign;
            ValueDonation = valueDonation;
        }
    }
}
