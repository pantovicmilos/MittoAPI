using Mitto.App2Sms.BussinesLogic.DataAccess.Models;
using Mitto.App2Sms.BussinesLogic.Mappings;
using Mitto.App2Sms.ServiceModel;
using Mitto.App2Sms.ServiceModel.Types;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitto.App2Sms.BussinesLogic.Services
{
    public class StatisticService
    {
        private readonly IDbConnectionFactory _dbFactory;

        public StatisticService(IDbConnectionFactory dbFactory)
        {
            this._dbFactory = dbFactory;
        }

        public async Task<GetStatisticsResponse> GetStatiststics(GetStatisticsRequest dto)
        {
            string query = string.Empty;
            List<dynamic> result = new List<dynamic>();
            List<StatisticItemDto> statItems = new List<StatisticItemDto>();

            bool filterByMcc = dto.MccList?.Any() ?? false;

            string queryNoMccFilter = $@"SELECT count(s.id) as Count, date(s.created) as Day, c.mcc as Mcc, sum(c.pricePerSms) as TotalPrice, c.PricePerSms
                                        FROM app2sms.Sms s
                                        INNER JOIN app2sms.Country c on c.id = s.countryId
                                        where date(s.created) >= @dateFrom and date(s.created) <= @dateTo
                                        group by c.mcc, date(s.created)";

            string queryAllFilters = @"SELECT count(s.id) as Count, date(s.created) as Day, c.mcc as Mcc, sum(c.pricePerSms) as TotalPrice, c.PricePerSms
                                       FROM app2sms.Sms s
                                       INNER JOIN app2sms.Country c on c.id = s.countryId
                                       where date(s.created) >= @dateFrom and date(s.created) <= @dateTo
                                       and c.mcc in (@MccList)
                                       group by c.mcc, date(s.created)";

            query = filterByMcc ? queryAllFilters : queryNoMccFilter;

            using (var db = _dbFactory.OpenDbConnection())
            {
                result = await db.SelectAsync<dynamic>(query, new { dto.MccList, dto.DateFrom, dto.DateTo } );
            }

            result.ForEach(row =>
            {
                StatisticItemDto item = ConvertExtensions.ConvertToStatDto(row);
                statItems.Add(item);
            });

            return new GetStatisticsResponse()
            {
                StatisticsItems = statItems
            };
        }
    }
}
