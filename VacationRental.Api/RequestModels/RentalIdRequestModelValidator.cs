using FluentValidation;

public class RentalIdRequestModelValidator : AbstractValidator<RentalIdRequestModel>
{
    public RentalIdRequestModelValidator()
    {
        RuleFor(d => d.RentalId).GreaterThan(0);
    }
}
