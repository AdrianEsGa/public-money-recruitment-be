using MediatR;
using VacationRental.Domain.Models;

public class GetCalendarQuery : IRequest<Calendar>
{
    public int RentalId { get; set; }
    public DateTime Start { get; set; }
    public int Nights { get; set; }

    public class GetCalendarQueryHandler : IRequestHandler<GetCalendarQuery, Calendar>
    {
        public Task<Calendar> Handle(GetCalendarQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

}