using FluentValidation;

namespace API.Application.Contracts
{
    public class AddDroneContract
    {
        public string ModelName { get; set; } = null!;
        public string SerialNumber { get; set; } = null!;
    }

    public class AddDroneValidator : AbstractValidator<AddDroneContract>
    {
        public AddDroneValidator()
        {
            RuleFor(x => x.SerialNumber)
                .NotEmpty()
                .WithMessage("Serial Number cannot be empty")
                .MaximumLength(5)
                .WithMessage("Serial number should not be longer than 5");
            RuleFor(x => x.ModelName)
                .NotEmpty()
                .WithMessage("Model Name cannot be empty")
                .MaximumLength(50)
                .WithMessage("Name cannot be longer than 50 characters");
        }
    }
}
