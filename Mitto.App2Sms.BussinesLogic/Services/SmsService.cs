using Mitto.App2Sms.BussinesLogic.DataAccess.Models;
using Mitto.App2Sms.BussinesLogic.Mappings;
using Mitto.App2Sms.BussinesLogic.Services;
using Mitto.App2Sms.ServiceModel;
using Mitto.App2Sms.ServiceModel.Types;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Collections.Generic;
using ServiceStack;
using System.Threading.Tasks;
using System;
using System.Net;
using ServiceStack.Text;

namespace Mitto.App2Sms.BussinesLogic
{
    public class SmsService
    {
        private readonly IDbConnectionFactory _dbFactory;
        private readonly CountryService _countryService;

        public SmsService(IDbConnectionFactory dbFactory, CountryService countryService)
        {
            this._dbFactory = dbFactory;
            this._countryService = countryService;
        }

        public async Task<GetSentSmsResponse> GetPagedSmsListAsync(GetSentSmsRequest dto)
        {
            List<Sms> messages = new List<Sms>();
            int totalCount = 0;

            using (var db = _dbFactory.OpenDbConnection())
            {
                SqlExpression<Sms> countQuery = db.From<Sms>()
                             .Join<Country>()
                             .Where<Sms>(x => x.Created >= dto.DateTimeFrom 
                                           && x.Created <= dto.DateTimeTo)
                             .Select(Sql.Count("*"));

                totalCount = await db.ScalarAsync<int>(countQuery);

                SqlExpression<Sms> pagedQuery = db.From<Sms>()
                             .Join<Country>()
                             .Where<Sms>(x => x.Created >= dto.DateTimeFrom
                                           && x.Created <= dto.DateTimeTo)
                             .Skip(dto.Skip)
                             .Take(dto.Take);

                messages = await db.LoadSelectAsync<Sms>(pagedQuery);
            }

            return new GetSentSmsResponse
            {
                Items = messages.ConvertAll(m => m.ToDto()),
                TotalCount = totalCount
            };
        }

        public async Task<SendSmsResponse> SendSms(SendSmsRequest dto)
        {
            State state = DispatchSms(dto);
            int countryId = _countryService.FindRecipientCountry(dto.To).Id;

            Sms message = new Sms
            {
                CountryId = countryId,
                State = state
            }
            .PopulateWith(dto);
                     
            using (var db = _dbFactory.OpenDbConnection())
            {
                await db.SaveAsync<Sms>(message);
            }

            return new SendSmsResponse()
            {
                State = state
            };
        }

        public State DispatchSms(SendSmsRequest message)
        {
            Console.WriteLine("sms message was sent successfully");
            message.PrintDump();

            return State.Success;
        }


    }
}
