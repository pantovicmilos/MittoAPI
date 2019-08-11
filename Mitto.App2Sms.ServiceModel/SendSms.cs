using Mitto.App2Sms.ServiceModel.Types;
using ServiceStack;

namespace Mitto.App2Sms.ServiceModel
{
    [Route("/sms/send", "GET")]
    public class SendSmsRequest : IReturn<SendSmsResponse>
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Text { get; set; }
    }

    public class SendSmsResponse
    {
        public State State { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }
}
