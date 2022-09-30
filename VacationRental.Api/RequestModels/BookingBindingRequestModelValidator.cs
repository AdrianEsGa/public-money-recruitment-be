using FluentValidation;

public class BookingBindingRequestModelValidator : AbstractValidator<BookingBindingRequestModel>
{
    public BookingBindingRequestModelValidator()
    {
        RuleFor(d => d.Nights).GreaterThan(0);
    }
}

