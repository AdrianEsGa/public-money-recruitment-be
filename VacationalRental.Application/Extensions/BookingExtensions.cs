public static class BookingExtensions
{
    public static bool IsBooked(this Booking booking, int rentalId, DateTime date)
    {
        return booking.RentalId == rentalId
                        && booking.Start <= date && booking.Start.AddDays(booking.Nights) > date;
    }

    public static bool IsInPreparationTime(this Booking booking, int rentalId, int preparationTime, DateTime date)
    {
        return booking.RentalId == rentalId
        && date >= booking.Start.AddDays(booking.Nights) && date < booking.Start.AddDays(booking.Nights + preparationTime);
    }
}
