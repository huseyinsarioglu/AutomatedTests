using FluentValidation;

namespace Automated.APISandbox.Model
{
    public class AppUserViewModelValidator : AbstractValidator<Student>
    {
        public AppUserViewModelValidator()
        {
            RuleFor(v => v.EmailAddress)
                .EmailAddress()
                .NotEmpty();

            RuleFor(v => v.Name)
                .NotEmpty()
                .MinimumLength(3);
        }
    }

    public record Student
    (
        int Id,
        string Name,
        string EmailAddress,
        DateTime BirthDate,
        string BirthPlace,
        byte[]? Image = null
    );
}
