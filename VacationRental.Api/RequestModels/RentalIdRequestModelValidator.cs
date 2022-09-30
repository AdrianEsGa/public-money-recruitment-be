using FluentValidation;

namespace VacationRental.Api.RequestModels
{
    public class RentalIdRequestModelValidator : AbstractValidator<RentalIdRequestModel>
    {
        public RentalIdRequestModelValidator()
        {
            RuleFor(d => d.RentalId).GreaterThan(0);
        }
    }
}
