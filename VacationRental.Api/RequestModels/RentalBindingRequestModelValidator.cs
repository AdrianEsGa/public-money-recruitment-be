using FluentValidation;
public class RentalBindingRequestModelValidator : AbstractValidator<RentalBindingRequestModel>
{
    public RentalBindingRequestModelValidator()
    {
        RuleFor(d => d.Units).GreaterThan(0);
    }
}

