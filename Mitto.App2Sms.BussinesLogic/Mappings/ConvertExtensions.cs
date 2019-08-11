using Mitto.App2Sms.BussinesLogic.DataAccess.Models;
using Mitto.App2Sms.ServiceModel.Types;
using System;

namespace Mitto.App2Sms.BussinesLogic.Mappings
{
    public static class ConvertExtensions
    {
        public static SentSmsDto ToDto(this Sms message)
        {
            return new SentSmsDto
            {
                State = message.State,
                From = message.From,
                To = message.To,
                MCC = message.Country.Mcc,
                Price = Math.Round(message.Country.PricePerSms, 2),
                DateTime = message.Created.ToString("s")
            };
        }

        public static StatisticItemDto ConvertToStatDto(dynamic row)
        {
            DateTime date = row.Day;
            string day = date.ToString("yyyy-MM-dd");

            StatisticItemDto dto = new StatisticItemDto
            {
                Count = (int)row.Count,
                Day = day,
                Mcc = row.Mcc,
                TotalPrice = Math.Round(row.TotalPrice, 2),
                PricePerSms = Math.Round(row.PricePerSms, 2)
            };

            return dto;
        }
    }
}
