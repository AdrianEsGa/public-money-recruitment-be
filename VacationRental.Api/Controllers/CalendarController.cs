using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/v1/calendar")]
[ApiController]
public class CalendarController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public CalendarController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<CalendarViewModel> Get([FromQuery] GetCalendarRequestModel request)
    {
        var command = _mapper.Map<GetCalendarQuery>(request);
        var model = await _mediator.Send(command);
        return _mapper.Map<CalendarViewModel>(model);
    }
}
