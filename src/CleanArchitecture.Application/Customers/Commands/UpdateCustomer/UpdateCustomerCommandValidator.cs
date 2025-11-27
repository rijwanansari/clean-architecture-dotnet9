using FluentValidation;

namespace CleanArchitecture.Application.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Customer ID is required");

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("First name is required")
            .MaximumLength(100)
            .WithMessage("First name cannot exceed 100 characters");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Last name is required")
            .MaximumLength(100)
            .WithMessage("Last name cannot exceed 100 characters");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("Phone number is required")
            .Matches(@"^[\d\s\-\+\(\)]+$")
            .WithMessage("Invalid phone number format")
            .MaximumLength(20)
            .WithMessage("Phone number cannot exceed 20 characters");

        RuleFor(x => x.Address)
            .NotNull()
            .WithMessage("Address is required");

        When(x => x.Address != null, () =>
        {
            RuleFor(x => x.Address.Street)
                .NotEmpty()
                .WithMessage("Street address is required")
                .MaximumLength(200)
                .WithMessage("Street address cannot exceed 200 characters");

            RuleFor(x => x.Address.City)
                .NotEmpty()
                .WithMessage("City is required")
                .MaximumLength(100)
                .WithMessage("City cannot exceed 100 characters");

            RuleFor(x => x.Address.State)
                .NotEmpty()
                .WithMessage("State is required")
                .MaximumLength(100)
                .WithMessage("State cannot exceed 100 characters");

            RuleFor(x => x.Address.ZipCode)
                .NotEmpty()
                .WithMessage("Zip code is required")
                .Matches(@"^\d{5}(-\d{4})?$")
                .WithMessage("Invalid zip code format");

            RuleFor(x => x.Address.Country)
                .NotEmpty()
                .WithMessage("Country is required")
                .MaximumLength(100)
                .WithMessage("Country cannot exceed 100 characters");
        });
    }
}
