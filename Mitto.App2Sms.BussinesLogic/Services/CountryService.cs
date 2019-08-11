using Mitto.App2Sms.BussinesLogic.DataAccess.Models;
using Mitto.App2Sms.ServiceModel;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Collections.Generic;
using System.Linq;
using ServiceStack;
using Mitto.App2Sms.ServiceModel.Types;
using System.Threading.Tasks;

namespace Mitto.App2Sms.BussinesLogic.Services
{
    public class CountryService
    {
        private readonly IDbConnectionFactory _dbFactory;
        public List<Country> countriesCache; 

        public CountryService(IDbConnectionFactory dbFactory)
        {
            this._dbFactory = dbFactory;
            countriesCache = GetAllCountries();
        }


        public List<Country> GetAllCountries()
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                return db.Select<Country>();
            }
        }

        public GetCountriesResponse GetCountriesDto()
        {
            return new GetCountriesResponse()
            {
                Countries = countriesCache.ConvertAll(c => c.ConvertTo<CountryDto>())
            };
        }

        public Country FindRecipientCountry(string phoneNumber)
        {
            phoneNumber = phoneNumber.Replace("+", "").Replace("-", "").Replace(" ", "");
            string cc = phoneNumber.Substring(0, 2);
            string mcc = phoneNumber.Substring(2, 3);

            Country country = countriesCache.FirstOrDefault(c => c.Mcc == mcc && c.Cc == cc);

            return country;
        }

        public bool CheckCountrySupport(string phoneNumber)
        {
            Country country = FindRecipientCountry(phoneNumber);

            if (country != null)
            {
                return true;
            }
            return false;
        }
    }
}
