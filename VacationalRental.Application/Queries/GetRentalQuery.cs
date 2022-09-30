using MediatR;
using VacationRental.Domain.Models;

public class GetRentalQuery : IRequest<Rental>
{
    public int RentalId { get; set; }
}

public class GetRentalQueryHandler : IRequestHandler<GetRentalQuery, Rental>
{
    public Task<Rental> Handle(GetRentalQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
