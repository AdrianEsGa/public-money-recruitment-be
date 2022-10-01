using MediatR;

public class CreateRentalCommand : IRequest<ResourceId>
{
    public int Units { get; set; }
    public int PreparationTimeInDays { get; set; }
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
            Units = request.Units,
            PreparationTimeInDays = request.PreparationTimeInDays,
            UnitValues = Enumerable.Range(1, request.Units).Select(x => new Unit
            {
                Id = x,
            }).ToList()
        });

        return key;
    }
}
