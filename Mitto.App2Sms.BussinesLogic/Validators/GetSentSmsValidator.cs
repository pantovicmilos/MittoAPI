using Mitto.App2Sms.ServiceModel;
using ServiceStack.FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mitto.App2Sms.BussinesLogic.Validators
{
    public class GetSentSmsValidator : AbstractValidator<GetSentSmsRequest>
    {
        public GetSentSmsValidator()
        {
            RuleFor(x => x.DateTimeFrom).NotEmpty();
            RuleFor(x => x.DateTimeTo).NotEmpty();
        }
    }
}
