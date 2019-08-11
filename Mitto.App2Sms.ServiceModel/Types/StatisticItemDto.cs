using System;
using System.Collections.Generic;
using System.Text;

namespace Mitto.App2Sms.ServiceModel.Types
{
    public class StatisticItemDto
    {
        public string Day { get; set; }
        public string Mcc { get; set; }
        public decimal PricePerSms { get; set; }
        public int Count { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
