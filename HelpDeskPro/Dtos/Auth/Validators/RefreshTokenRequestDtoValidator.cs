using FluentValidation;

namespace HelpDeskPro.Dtos.Auth.Validators
{
    public class RefreshTokenRequestDtoValidator : AbstractValidator<RefreshTokenRequestDto>
    {
        public RefreshTokenRequestDtoValidator()
        {
            RuleFor(x => x.RefreshToken)
                .NotEmpty().WithErrorCode("RefreshToken_Required");
        }
    }
}
