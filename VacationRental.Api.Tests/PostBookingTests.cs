using System.Net.Http.Json;
using Xunit;


[Collection("Integration")]
public class PostBookingTests
{
    private readonly HttpClient _client;

    public PostBookingTests()
    {
        _client = new TestServerFixture().CreateClient();
    }

    [Fact]
    public async Task GivenCompleteRequest_WhenPostBooking_ThenAGetReturnsTheCreatedBooking()
    {

        var postRentalRequest = new RentalBindingRequestModel
        {
            Units = 4
        };

        ResourceIdViewModel postRentalResult;
        using (var postRentalResponse = await _client.PostAsJsonAsync($"/api/v1/rentals", postRentalRequest))
        {
            Assert.True(postRentalResponse.IsSuccessStatusCode);
            postRentalResult = await postRentalResponse.Content.ReadAsAsync<ResourceIdViewModel>();
        }

        var postBookingRequest = new BookingBindingRequestModel
        {
            rentalId = postRentalResult.Id,
            nights = 3,
            start = new DateTime(2001, 01, 01)
        };

        ResourceIdViewModel postBookingResult;
        using (var postBookingResponse = await _client.PostAsJsonAsync($"/api/v1/bookings", postBookingRequest))
        {
            Assert.True(postBookingResponse.IsSuccessStatusCode);
            postBookingResult = await postBookingResponse.Content.ReadAsAsync<ResourceIdViewModel>();
        }

        using (var getBookingResponse = await _client.GetAsync($"/api/v1/bookings/{postBookingResult.Id}"))
        {
            Assert.True(getBookingResponse.IsSuccessStatusCode);

            var getBookingResult = await getBookingResponse.Content.ReadAsAsync<BookingViewModel>();
            Assert.Equal(postBookingRequest.rentalId, getBookingResult.RentalId);
            Assert.Equal(postBookingRequest.nights, getBookingResult.Nights);
            Assert.Equal(postBookingRequest.start, getBookingResult.Start);
        }
    }

    [Fact]
    public async Task GivenCompleteRequest_WhenPostBooking_ThenAPostReturnsErrorWhenThereIsOverbooking()
    {
        var postRentalRequest = new RentalBindingRequestModel
        {
            Units = 1
        };

        ResourceIdViewModel postRentalResult;
        using (var postRentalResponse = await _client.PostAsJsonAsync($"/api/v1/rentals", postRentalRequest))
        {
            Assert.True(postRentalResponse.IsSuccessStatusCode);
            postRentalResult = await postRentalResponse.Content.ReadAsAsync<ResourceIdViewModel>();
        }

        var postBooking1Request = new BookingBindingRequestModel
        {
            rentalId = postRentalResult.Id,
            nights = 3,
            start = new DateTime(2002, 01, 01)
        };

        using (var postBooking1Response = await _client.PostAsJsonAsync($"/api/v1/bookings", postBooking1Request))
        {
            Assert.True(postBooking1Response.IsSuccessStatusCode);
        }

        var postBooking2Request = new BookingBindingRequestModel
        {
            rentalId = postRentalResult.Id,
            nights = 1,
            start = new DateTime(2002, 01, 02)
        };

        //TODO: FIX this, we have techinical problems with Assert.ThrowsAsync<ApplicationException>(())
        using var postBooking2Response = await _client.PostAsJsonAsync($"/api/v1/bookings", postBooking2Request);
        Assert.True(!postBooking2Response.IsSuccessStatusCode);

    }

    [Fact]
    public async Task GivenCompleteRequest_WhenPostBooking_ThenAPostReturnsErrorWhenThereIsOverbookingBecausePreparationTime()
    {
        var postRentalRequest = new RentalBindingRequestModel
        {
            Units = 1,
            PreparationTimeInDays = 1        
        };

        ResourceIdViewModel postRentalResult;
        using (var postRentalResponse = await _client.PostAsJsonAsync($"/api/v1/rentals", postRentalRequest))
        {
            Assert.True(postRentalResponse.IsSuccessStatusCode);
            postRentalResult = await postRentalResponse.Content.ReadAsAsync<ResourceIdViewModel>();
        }

        var postBooking1Request = new BookingBindingRequestModel
        {
            rentalId = postRentalResult.Id,
            nights = 1,
            start = new DateTime(2002, 01, 01)
        };

        using (var postBooking1Response = await _client.PostAsJsonAsync($"/api/v1/bookings", postBooking1Request))
        {
            Assert.True(postBooking1Response.IsSuccessStatusCode);
        }

        var postBooking2Request = new BookingBindingRequestModel
        {
            rentalId = postRentalResult.Id,
            nights = 1,
            start = new DateTime(2002, 01, 02)
        };

        //TODO: FIX this, we have techinical problems with Assert.ThrowsAsync<ApplicationException>(())
        using var postBooking2Response = await _client.PostAsJsonAsync($"/api/v1/bookings", postBooking2Request);
        Assert.True(!postBooking2Response.IsSuccessStatusCode);

    }
}
