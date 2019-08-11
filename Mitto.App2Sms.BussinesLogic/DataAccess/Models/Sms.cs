using Mitto.App2Sms.ServiceModel.Types;
using ServiceStack.DataAnnotations;

namespace Mitto.App2Sms.BussinesLogic.DataAccess.Models
{
    public class Sms : BaseEntity
    {
        public string Text { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public State State { get; set; }
        [Reference]
        public Country Country { get; set; }
        [ForeignKey(typeof(Country))]
        public int CountryId { get; set; }

    }
}
