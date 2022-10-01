using FluentValidation;

public class BookingBindingRequestModelValidator : AbstractValidator<BookingBindingRequestModel>
{
    public BookingBindingRequestModelValidator()
    {
        RuleFor(d => d.nights).GreaterThan(0);
    }
}

