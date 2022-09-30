using FluentValidation;

public class BookingIdRequestModelValidator : AbstractValidator<BookingIdRequestModel>
{
    public BookingIdRequestModelValidator()
    {
        RuleFor(d => d.BookingId).GreaterThan(0);
    }
}
