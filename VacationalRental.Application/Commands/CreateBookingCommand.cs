using MediatR;
using System.ComponentModel.DataAnnotations;

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

        var rental = _rentals[request.RentalId];

        var preparationTime = rental.PreparationTimeInDays;

        var busyUnits = new List<Unit>();

        for (var i = 0; i < request.Nights; i++)
        {
            var count = 0;
            foreach (var booking in _bookings.Values)
            {
                if (booking.RentalId == request.RentalId
                && (booking.Start <= request.Start.Date && booking.Start.AddDays(booking.Nights + preparationTime) > request.Start.Date)
                || (booking.Start < request.Start.AddDays(request.Nights + preparationTime) && booking.Start.AddDays(booking.Nights + preparationTime) >= request.Start.AddDays(request.Nights + preparationTime))
                || (booking.Start > request.Start && booking.Start.AddDays(booking.Nights + preparationTime ) < request.Start.AddDays(request.Nights + preparationTime)))
                {
                    if(!busyUnits.Contains(booking.Unit)) 
                        busyUnits.Add(booking.Unit);

                    count++;
                }
            }

            if (count >= rental.Units)
                throw new ApplicationException("Not available");
        }

        var availableUnits = rental.UnitValues.Where(p => busyUnits.All(p2 => p2.Id != p.Id));

        var key = new ResourceId { Id = _bookings.Keys.Count + 1 };

        _bookings.Add(key.Id, new Booking
        {
            Id = key.Id,
            Nights = request.Nights,
            RentalId = request.RentalId,
            Start = request.Start.Date,
            Unit = availableUnits.First()
        });

        return key;
    }
}
