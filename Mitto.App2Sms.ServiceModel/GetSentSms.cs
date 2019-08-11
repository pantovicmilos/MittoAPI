using System;
using System.Collections.Generic;
using Mitto.App2Sms.ServiceModel.Types;
using ServiceStack;

namespace Mitto.App2Sms.ServiceModel
{
    [Route("/sms/sent", "GET")]
    public class GetSentSmsRequest : IReturn<GetSentSmsResponse>
    {
        public DateTime DateTimeFrom { get; set; }
        public DateTime DateTimeTo { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }

    public class GetSentSmsResponse
    {
        public List<SentSmsDto> Items { get; set; }
        public int TotalCount { get; set; }
        public ResponseStatus ResponseStatus { get; set; }

    }
}
