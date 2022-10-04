using DK_Project.Models.Requests;
using FluentValidation;

namespace DK_Project.Validators
{
    public class AddUpdateAuthorRequestValidator : AbstractValidator<AddUpdateAuthorRequest>
    {
        public AddUpdateAuthorRequestValidator()
        {
            RuleFor(x => x.Age)
                .GreaterThan(0).WithMessage("YOu are to old bruh")
                .LessThanOrEqualTo(120).WithMessage("You are too young budddddddddddddddy");
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);
            When(x => !string.IsNullOrEmpty(x.Nickname), () =>
            {
                RuleFor<string>(x => x.Nickname)
                 .MinimumLength(2)
                 .MaximumLength(50);
            });
            RuleFor(x => x.DateOfBirth)
                .GreaterThan(DateTime.MinValue)
                .LessThan(DateTime.MaxValue);
        }
    }
}
