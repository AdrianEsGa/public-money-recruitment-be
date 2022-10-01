public class BookingBindingRequestModel
{
    public int rentalId { get; set; }

    public DateTime start
    {
        get => _startIgnoreTime;
        set => _startIgnoreTime = value.Date;
    }

    private DateTime _startIgnoreTime;
    public int nights { get; set; }
}
