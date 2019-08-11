using Mitto.App2Sms.BussinesLogic.Services;
using Mitto.App2Sms.ServiceModel;
using ServiceStack;
using System.Threading.Tasks;

namespace Mitto.App2Sms.ServiceInterface
{
    public class CountriesApi : Service
    {
        private readonly CountryService _countryService;
        public CountriesApi(CountryService countryService)
        {
            this._countryService = countryService;
        }
        
        public GetCountriesResponse Any(GetCountriesRequest request)
        {
            return this._countryService.GetCountriesDto();
        }
    }
}
