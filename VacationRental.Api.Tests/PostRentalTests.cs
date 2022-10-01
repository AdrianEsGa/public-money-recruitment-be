﻿using System.Net.Http.Json;
using Xunit;


[Collection("Integration")]
public class PostRentalTests
{
    private readonly HttpClient _client;

    public PostRentalTests()
    {
        _client = new TestServerFixture().CreateClient();
    }

    [Fact]
    public async Task GivenCompleteRequest_WhenPostRental_ThenAGetReturnsTheCreatedRental()
    {
        var request = new RentalBindingRequestModel
        {
            Units = 25,
            PreparationTimeInDays = 1
        };

        ResourceIdViewModel postResult;
        using (var postResponse = await _client.PostAsJsonAsync($"/api/v1/rentals", request))
        {
            Assert.True(postResponse.IsSuccessStatusCode);
            postResult = await postResponse.Content.ReadAsAsync<ResourceIdViewModel>();
        }

        using (var getResponse = await _client.GetAsync($"/api/v1/rentals/{postResult.Id}"))
        {
            Assert.True(getResponse.IsSuccessStatusCode);

            var getResult = await getResponse.Content.ReadAsAsync<RentalViewModel>();
            Assert.Equal(request.Units, getResult.Units);
            Assert.Equal(request.PreparationTimeInDays, getResult.PreparationTimeInDays);
        }
    }
}
