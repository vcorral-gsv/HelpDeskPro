using FluentValidation;
using HelpDeskPro.Consts;

namespace HelpDeskPro.Dtos.Auth.Validators
{
    public class RegisterUserRequestDtoValidator : AbstractValidator<RegisterUserRequestDto>
    {
        public RegisterUserRequestDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithErrorCode("Email_Required")
                .EmailAddress().WithErrorCode("Email_Invalid");

            RuleFor(x => x.Password)
                .NotEmpty().WithErrorCode("Password_Required")
                .MinimumLength(8).WithErrorCode("Password_MinLength");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithErrorCode("FirstName_Required")
                .MaximumLength(100).WithErrorCode("FirstName_MaxLength");

            RuleFor(x => x.LastName)
                .NotEmpty().WithErrorCode("LastName_Required")
                .MaximumLength(100).WithErrorCode("LastName_MaxLength");

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
