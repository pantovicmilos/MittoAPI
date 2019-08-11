using Mitto.App2Sms.BussinesLogic.Services;
using Mitto.App2Sms.ServiceModel;
using ServiceStack.FluentValidation;

namespace Mitto.App2Sms.BussinesLogic.Validators
{
    public class GetStatisticsValidator : AbstractValidator<GetStatisticsRequest>
    {
        public GetStatisticsValidator()
        {
            RuleFor(x => x.DateFrom).NotEmpty();
            RuleFor(x => x.DateTo).NotEmpty();
        }
    }
}
