namespace Mitto.App2Sms.BussinesLogic.DataAccess.Models
{
    public class Country : BaseEntity
    {
        public string Mcc { get; set; }
        public string Cc { get; set; }
        public string Name { get; set; }
        public decimal PricePerSms { get; set; }
    }
}
