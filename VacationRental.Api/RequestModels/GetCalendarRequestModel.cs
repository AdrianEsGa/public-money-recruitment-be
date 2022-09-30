using Microsoft.AspNetCore.Mvc;

public class GetCalendarRequestModel
{
    [FromQuery]
    public int RentalId { get; set; }
    [FromQuery]
    public DateTime Start { get; set; }
    [FromQuery]
    public int Nights { get; set; }
}