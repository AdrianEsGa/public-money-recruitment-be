using FluentValidation;

public class GetCalendarRequestModelValidator : AbstractValidator<GetCalendarRequestModel>
{
    public GetCalendarRequestModelValidator()
    {
        RuleFor(d => d.rentalId).GreaterThan(0);
    }
}
