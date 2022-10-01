using MediatR;

public class GetRentalQuery : IRequest<Rental>
{
    public int RentalId { get; set; }
}

public class GetRentalQueryHandler : IRequestHandler<GetRentalQuery, Rental>
{
    private readonly IDictionary<int, Rental> _rentals;

    public GetRentalQueryHandler(IDictionary<int, Rental> rentals, IDictionary<int, Booking> bookings)
    {
        _rentals = rentals;
    }

    public async Task<Rental> Handle(GetRentalQuery request, CancellationToken cancellationToken)
    {
        if (!_rentals.ContainsKey(request.RentalId))
            throw new ApplicationException("Rental not found");

        return _rentals[request.RentalId];
    }
}
