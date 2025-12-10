using FluentValidation;
using HelpDeskPro.Consts;

namespace HelpDeskPro.Dtos.Auth.Validators
{
    public class LoginRequestDtoValidator : AbstractValidator<LoginRequestDto>
    {
        public LoginRequestDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithErrorCode("Email_Required")
                .EmailAddress().WithErrorCode("Email_Invalid");

            RuleFor(x => x.Password)
                .NotEmpty().WithErrorCode("Password_Required")
                .MinimumLength(6).WithErrorCode("Password_MinLength");

            RuleFor(x => x.LanguageCode)
                .Must(BeValidLanguageCode)
                .When(x => !string.IsNullOrWhiteSpace(x.LanguageCode))
                .WithErrorCode("LanguageCode_Invalid");
        }

        private bool BeValidLanguageCode(string? code)
        {
            return code != null && Languages.IsLanguageSupported(code);
        }
    }
}
