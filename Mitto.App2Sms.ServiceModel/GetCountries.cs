using Mitto.App2Sms.ServiceModel.Types;
using ServiceStack;
using System;
using System.Collections.Generic;

namespace Mitto.App2Sms.ServiceModel
{
    [Route("/countries", "GET")]
    public class GetCountriesRequest : IReturn<GetCountriesResponse>
    {
    }

    public class GetCountriesResponse
    {
        public List<CountryDto> Countries { get; set; }
        public ResponseStatus ResponseStatus { get; set; }

    }
}
