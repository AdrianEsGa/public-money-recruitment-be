using MediatR;

public class CreateRentalCommand : IRequest<int>
{
    public int Units { get; set; }
}

public class CreateRentalCommandHandler : IRequestHandler<CreateRentalCommand, int>
{
    public Task<int> Handle(CreateRentalCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
