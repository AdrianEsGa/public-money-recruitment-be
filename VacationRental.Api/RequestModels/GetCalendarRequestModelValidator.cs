using FluentValidation;

namespace VacationRental.Api.RequestModels
{
    public class GetCalendarRequestModelValidator : AbstractValidator<GetCalendarRequestModel>
    {
        public GetCalendarRequestModelValidator()
        {
            RuleFor(d => d.RentalId).GreaterThan(0);
        }
    }
}
