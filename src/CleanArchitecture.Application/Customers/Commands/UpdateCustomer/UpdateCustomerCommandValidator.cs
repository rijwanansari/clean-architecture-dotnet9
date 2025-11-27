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
                .Must((customer, zipCode) =>
                {
                    if (string.IsNullOrWhiteSpace(zipCode) || customer?.Address?.Country == null)
                        return false;
                    var country = customer.Address.Country.Trim().ToUpperInvariant();
                    switch (country)
                    {
                        case "US":
                        case "USA":
                        case "UNITED STATES":
                            return System.Text.RegularExpressions.Regex.IsMatch(zipCode, @"^\d{5}(-\d{4})?$");
                        case "CA":
                        case "CAN":
                        case "CANADA":
                            return System.Text.RegularExpressions.Regex.IsMatch(zipCode, @"^[A-Za-z]\d[A-Za-z][ -]?\d[A-Za-z]\d$");
                        case "GB":
                        case "UK":
                        case "UNITED KINGDOM":
                            return System.Text.RegularExpressions.Regex.IsMatch(zipCode, @"^[A-Za-z]{1,2}\d[A-Za-z\d]?\s*\d[A-Za-z]{2}$");
                        default:
                            // Allow alphanumeric postal codes, 3-12 chars, for other countries
                            return System.Text.RegularExpressions.Regex.IsMatch(zipCode, @"^[A-Za-z0-9\- ]{3,12}$");
                    }
                })
                .WithMessage("Invalid zip/postal code format for the specified country.");

            RuleFor(x => x.Address.Country)
                .NotEmpty()
                .WithMessage("Country is required")
                .MaximumLength(100)
                .WithMessage("Country cannot exceed 100 characters");
        });
    }
}
