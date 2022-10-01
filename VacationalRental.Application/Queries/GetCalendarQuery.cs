using MediatR;

public class GetCalendarQuery : IRequest<Calendar>
{
    public int RentalId { get; set; }
    public DateTime Start { get; set; }
    public int Nights { get; set; }

    public class GetCalendarQueryHandler : IRequestHandler<GetCalendarQuery, Calendar>
    {
        private readonly IDictionary<int, Rental> _rentals;
        private readonly IDictionary<int, Booking> _bookings;

        public GetCalendarQueryHandler(IDictionary<int, Rental> rentals, IDictionary<int, Booking> bookings)
        {
            _rentals = rentals;
            _bookings = bookings;
        }

        public async Task<Calendar> Handle(GetCalendarQuery request, CancellationToken cancellationToken)
        {
            if (!_rentals.ContainsKey(request.RentalId))
                throw new ApplicationException("Rental not found");

            var result = new Calendar
            {
                RentalId = request.RentalId,
                Dates = new List<CalendarDate>()
            };

            var preparationTime = _rentals[request.RentalId].PreparationTimeInDays;

            for (var i = 0; i < request.Nights; i++)
            {
                var date = new CalendarDate
                {
                    Date = request.Start.Date.AddDays(i),
                    Bookings = new List<CalendarBooking>(),
                    PreparationTimes = new List<PreparationTime>()
                };

                foreach (var booking in _bookings.Values)
                {
                    if (booking.IsBooked(request.RentalId, date.Date))
                    {
                        date.Bookings.Add(new CalendarBooking { Id = booking.Id, Unit = booking.Unit.Id });
                    }

                    if (booking.IsInPreparationTime(request.RentalId, preparationTime, date.Date))
                    {
                        date.PreparationTimes.Add(new PreparationTime { Unit = booking.Unit.Id });
                    }
                }

                result.Dates.Add(date);
            }

            return result;
        }
    }
}