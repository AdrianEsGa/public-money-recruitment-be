using MediatR;
using VacationRental.Domain.Models;

public class CreateBookingCommand : IRequest<ResourceId>
{
    public int RentalId { get; set; }
    public DateTime Start { get; set; }
    public int Nights { get; set; }
}

public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, ResourceId>
{
    private readonly IDictionary<int, Rental> _rentals;
    private readonly IDictionary<int, Booking> _bookings;

    public CreateBookingCommandHandler(IDictionary<int, Rental> rentals, IDictionary<int, Booking> bookings)
    {
        _rentals = rentals;
        _bookings = bookings;
    }

    public async Task<ResourceId> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        if (!_rentals.ContainsKey(request.RentalId))
            throw new ApplicationException("Rental not found");

        for (var i = 0; i < request.Nights; i++)
        {
            var count = 0;
            foreach (var booking in _bookings.Values)
            {
                if (booking.RentalId == request.RentalId
                && (booking.Start <= request.Start.Date && booking.Start.AddDays(booking.Nights) > request.Start.Date)
                || (booking.Start < request.Start.AddDays(request.Nights) && booking.Start.AddDays(booking.Nights) >= request.Start.AddDays(request.Nights))
                || (booking.Start > request.Start && booking.Start.AddDays(booking.Nights) < request.Start.AddDays(request.Nights)))
                {
                    count++;
                }
            }
            if (count >= _rentals[request.RentalId].Units)
                throw new ApplicationException("Not available");
        }

        var key = new ResourceId { Id = _bookings.Keys.Count + 1 };

        _bookings.Add(key.Id, new Booking
        {
            Id = key.Id,
            Nights = request.Nights,
            RentalId = request.RentalId,
            Start = request.Start.Date
        });

        return key;
    }
}
