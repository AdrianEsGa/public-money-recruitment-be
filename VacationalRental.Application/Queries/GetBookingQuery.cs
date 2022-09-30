using MediatR;
using VacationRental.Domain.Models;

public class GetBookingQuery : IRequest<Booking>
{
    public int BookingId { get; set; }
}

public class GetBookingQueryHandler : IRequestHandler<GetBookingQuery, Booking>
{
    public Task<Booking> Handle(GetBookingQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
