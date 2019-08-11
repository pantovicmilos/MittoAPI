using ServiceStack;
using Mitto.App2Sms.ServiceModel;
using Mitto.App2Sms.BussinesLogic.Services;
using System.Threading.Tasks;

namespace Mitto.App2Sms.ServiceInterface
{
    public class StatisticsApi : Service
    {
        private readonly StatisticService _statisticService;
        public StatisticsApi(StatisticService statisticService)
        {
            this._statisticService = statisticService;
        }
        public Task<GetStatisticsResponse> Any(GetStatisticsRequest request)
        {
            return this._statisticService.GetStatiststics(request);
        }
    }
}
