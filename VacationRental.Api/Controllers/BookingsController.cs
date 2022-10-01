using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/v1/bookings")]
[ApiController]
public class BookingsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public BookingsController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpGet]
    [Route("{bookingId:int}")]
    public async Task<BookingViewModel> Get(int bookingId)
    {
        var command = new GetBookingQuery { BookingId = bookingId };
        var model = await _mediator.Send(command);
        return _mapper.Map<BookingViewModel>(model);
    }

    [HttpPost]
    public async Task<ResourceIdViewModel> Post(BookingBindingRequestModel request)
    {
        var command = _mapper.Map<CreateBookingCommand>(request);
        var model = await _mediator.Send(command);
        return _mapper.Map<ResourceIdViewModel>(model);
    }
}
