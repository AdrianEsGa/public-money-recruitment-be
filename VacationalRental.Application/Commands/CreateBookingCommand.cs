using MediatR;

public class CreateBookingCommand : IRequest<int>
{
    public int RentalId { get; set; }
    public DateTime Start { get; set; }
    public int Nights { get; set; }
}

public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, int>
{
    public Task<int> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
