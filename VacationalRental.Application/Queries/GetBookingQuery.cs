using MediatR;

public class GetBookingQuery : IRequest<Booking>
{
    public int BookingId { get; set; }
}

public class GetBookingQueryHandler : IRequestHandler<GetBookingQuery, Booking>
{
    private readonly IDictionary<int, Booking> _bookings;

    public GetBookingQueryHandler(IDictionary<int, Booking> bookings)
    {
        _bookings = bookings;
    }

    public async Task<Booking> Handle(GetBookingQuery request, CancellationToken cancellationToken)
    {
        if (!_bookings.ContainsKey(request.BookingId))
            throw new ApplicationException("Booking not found");

        return _bookings[request.BookingId];
    }
}
