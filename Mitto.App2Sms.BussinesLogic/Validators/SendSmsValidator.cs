using Mitto.App2Sms.BussinesLogic.Services;
using Mitto.App2Sms.ServiceModel;
using PhoneNumbers;
using ServiceStack.FluentValidation;
using System;

namespace Mitto.App2Sms.BussinesLogic.Validators
{
    public class SendSmsValidator : AbstractValidator<SendSmsRequest>
    {
        private readonly CountryService _countryService;
        public SendSmsValidator(CountryService countryService)
        {
            _countryService = countryService;

            RuleFor(x => x.From).NotEmpty();
            RuleFor(x => x.Text).NotEmpty();
            RuleFor(x => x.To).NotEmpty();

            RuleFor(x => x.To)
                .Must(phoneNumber => ValidatePhoneNumber(phoneNumber) == true)
                .When(x => !string.IsNullOrEmpty(x.To))
                .WithMessage("Phone number not valid")
                .WithErrorCode("Invalid number");

            RuleFor(x => x.To)
                .Must(to => _countryService.CheckCountrySupport(to) == true)
                .When(x => ValidatePhoneNumber(x.To) == true)
                .WithMessage("Mcc not currently supported")
                .WithErrorCode("Unsupported Mcc");
        }

        public bool ValidatePhoneNumber(string E164phoneNumber)
        {
            bool isValid = true;
            var phoneNumberUtil = PhoneNumbers.PhoneNumberUtil.GetInstance();
            try
            {
                PhoneNumber parsed = phoneNumberUtil.Parse(E164phoneNumber, null);
            }
            catch (NumberParseException ex)
            {
                isValid = false;
            }

            return isValid;
        }
    }
}
