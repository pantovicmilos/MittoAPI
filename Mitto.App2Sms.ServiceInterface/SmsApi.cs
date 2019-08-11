using ServiceStack;
using Mitto.App2Sms.ServiceModel;
using Mitto.App2Sms.BussinesLogic;
using System.Threading.Tasks;

namespace Mitto.App2Sms.ServiceInterface
{
    public class SmsApi : Service
    {
        private readonly SmsService _messagesService;
        public SmsApi(SmsService messagesService)
        {
            this._messagesService = messagesService;
        }
        public Task<SendSmsResponse> Any(SendSmsRequest request)
        {
            return this._messagesService.SendSms(request);
        }

        public Task<GetSentSmsResponse> Any(GetSentSmsRequest request)
        {
            return this._messagesService.GetPagedSmsListAsync(request);
        }
    }
}
