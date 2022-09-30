using MediatR;
using VacationRental.Domain.Models;

public class CreateRentalCommand : IRequest<ResourceId>
{
    public int Units { get; set; }
}

public class CreateRentalCommandHandler : IRequestHandler<CreateRentalCommand, ResourceId>
{
    private readonly IDictionary<int, Rental> _rentals;

    public CreateRentalCommandHandler(IDictionary<int, Rental> rentals, IDictionary<int, Booking> bookings)
    {
        _rentals = rentals;
    }

    public async Task<ResourceId> Handle(CreateRentalCommand request, CancellationToken cancellationToken)
    {
        var key = new ResourceId { Id = _rentals.Keys.Count + 1 };

        _rentals.Add(key.Id, new Rental
        {
            Id = key.Id,
            Units = request.Units
        });

        return key;
    }
}
