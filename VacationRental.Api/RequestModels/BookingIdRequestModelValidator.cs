using FluentValidation;

namespace VacationRental.Api.RequestModels
{
    public class BookingIdRequestModelValidator : AbstractValidator<BookingIdRequestModel>
    {
        public BookingIdRequestModelValidator()
        {
            RuleFor(d => d.BookingId).GreaterThan(0);
        }
    }
}
