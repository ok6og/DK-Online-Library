using DK_Project.Models.Requests;
using FluentValidation;

namespace DK_Project.Validators
{
    public class AddUpdateBookRequestValidator : AbstractValidator<AddUpdateBookRequest>
    {
        public AddUpdateBookRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);
            RuleFor(x=> x.AuthorId)
                .GreaterThanOrEqualTo(0).WithMessage("Input a valid AuthorId")
                .NotEmpty().WithMessage("Please input an AuthorId");
            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(0).WithMessage("Input a valid Id");
        }
    }
}
