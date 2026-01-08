namespace WebApi.IntegrationTests.QueryTests;

[Collection(ApplicationTestCollection.Name)]
public class QueryTests : IDisposable
{
    private readonly ApplicationTestFixture _applicationTestFixture;
    private readonly HttpClient _httpClient;

    public QueryTests(ApplicationTestFixture applicationTestFixture)
    {
        _applicationTestFixture = applicationTestFixture;
        _httpClient = _applicationTestFixture.WebApiApplicationFactory.Server.CreateClient();
    }

    [Fact]
    public async Task GetServiceOffersPage_ShouldReturnServiceOffersPage()
    {
        //Arrange

        //Act

        //Assert
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}
