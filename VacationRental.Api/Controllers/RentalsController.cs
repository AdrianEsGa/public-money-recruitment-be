using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/v1/rentals")]
[ApiController]
public class RentalsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public RentalsController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpGet]
    [Route("{rentalId:int}")]
    public async Task<RentalViewModel> Get(int rentalId)
    {
        var command = new GetRentalQuery { RentalId = rentalId };
        var model = await _mediator.Send(command);
        return _mapper.Map<RentalViewModel>(model);
    }

    [HttpPost]
    public async Task<ResourceIdViewModel> Post(RentalBindingRequestModel request)
    {
        var command = _mapper.Map<CreateRentalCommand>(request);
        var model = await _mediator.Send(command);
        return _mapper.Map<ResourceIdViewModel>(model);
    }
}

