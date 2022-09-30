using FluentValidation;

namespace VacationRental.Api.RequestModels
{
    public class BookingBindingRequestModelValidator : AbstractValidator<BookingBindingRequestModel>
    {
        public BookingBindingRequestModelValidator()
        {
            RuleFor(d => d.Nights).GreaterThan(0);
        }
    }
}
