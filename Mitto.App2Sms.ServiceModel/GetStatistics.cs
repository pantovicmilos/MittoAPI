using Mitto.App2Sms.ServiceModel.Types;
using ServiceStack;
using System;
using System.Collections.Generic;

namespace Mitto.App2Sms.ServiceModel
{
    [Route("/statistics", "GET")]
    public class GetStatisticsRequest : IReturn<GetStatisticsResponse>
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public List<string> MccList { get; set; }
    }

    public class GetStatisticsResponse
    {
        public List<StatisticItemDto> StatisticsItems { get; set; }
        public ResponseStatus ResponseStatus { get; set; }

    }
}
