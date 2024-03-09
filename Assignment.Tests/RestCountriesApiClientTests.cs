using Assignment.Core.Services;
using Assignment.Core.Services.Impl;
using Moq;
using RichardSzalay.MockHttp;
using System.Net;

namespace Assignment.Tests
{
    public class RestCountriesApiClientTests
    {
        private readonly IRestCountriesApiClient _restCountriesApiClientMock;
        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock = new();
        private readonly MockHttpMessageHandler _httpClientMessageHandlerMock = new MockHttpMessageHandler();

        public RestCountriesApiClientTests()
        {
            _restCountriesApiClientMock = new RestCountriesApiClient(_httpClientFactoryMock.Object);
        }

        private const string API_ENDPOINT = "https://restcountries.com/v3.1/all?fields=name,capital,borders,cca3";

        [Fact]
        public async Task GetAllCountriesAsync_Success()
        {
            // Arrange
            _httpClientMessageHandlerMock.When(API_ENDPOINT)
                    .Respond(HttpStatusCode.OK, "application/json", @"
            [
                {
                    ""name"": {
                        ""common"": ""Country1"",
                        ""official"": ""Official Name1"",
                        ""nativeName"": null
                    },
                    ""capital"": [""Capital1""],
                    ""borders"": [""Border1"", ""Border2""],
                    ""cca3"": ""ABC""
                },
                {
                    ""name"": {
                        ""common"": ""Country2"",
                        ""official"": ""Official Name2"",
                        ""nativeName"": null
                    },
                    ""capital"": [""Capital2""],
                    ""borders"": [""Border3"", ""Border4""],
                    ""cca3"": ""DEF""
                }
            ]");

            _httpClientFactoryMock.Setup(s => s.CreateClient(It.IsAny<string>())).Returns(new HttpClient(_httpClientMessageHandlerMock));

            // Act
            var result = await _restCountriesApiClientMock.GetAllCountriesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsData);
            Assert.NotNull(result.Data);

            var countries = result.Data;
            Assert.Collection(countries,
                country =>
                {
                    Assert.Equal("Country1", country.CommonName);
                    Assert.Equal("Capital1", country.Capital);
                    Assert.Collection(country.Borders,
                        border => Assert.Equal("Border1", border),
                        border => Assert.Equal("Border2", border));
                    Assert.Equal("ABC", country.Id);
                },
                country =>
                {
                    Assert.Equal("Country2", country.CommonName);
                    Assert.Equal("Capital2", country.Capital);
                    Assert.Collection(country.Borders,
                        border => Assert.Equal("Border3", border),
                        border => Assert.Equal("Border4", border));
                    Assert.Equal("DEF", country.Id);
                });
        }

        [Fact]
        public async Task GetAllCountriesAsync_NotFound()
        {
            // Arrange
            _httpClientMessageHandlerMock.When(API_ENDPOINT)
                .Respond(HttpStatusCode.NotFound);

            _httpClientFactoryMock.Setup(s => s.CreateClient(It.IsAny<string>())).Returns(new HttpClient(_httpClientMessageHandlerMock));

            // Act
            var result = await _restCountriesApiClientMock.GetAllCountriesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsError);
            Assert.Equal("Failed to retrieve data from API. Status code: NotFound", result.Error);
        }

        [Fact]
        public async Task GetAllCountriesAsync_EmptyResponse()
        {
            // Arrange
            _httpClientMessageHandlerMock.When(API_ENDPOINT)
                .Respond(HttpStatusCode.OK, "application/json", "[]");

            _httpClientFactoryMock.Setup(s => s.CreateClient(It.IsAny<string>())).Returns(new HttpClient(_httpClientMessageHandlerMock));

            // Act
            var result = await _restCountriesApiClientMock.GetAllCountriesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsError);
            Assert.Equal("Country list is empty.", result.Error);
        }

        [Fact]
        public async Task GetAllCountriesAsync_InvalidJson()
        {
            // Arrange
            _httpClientMessageHandlerMock.When(API_ENDPOINT)
                .Respond(HttpStatusCode.OK, "application/json", "{Invalid JSON}");

            _httpClientFactoryMock.Setup(s => s.CreateClient(It.IsAny<string>())).Returns(new HttpClient(_httpClientMessageHandlerMock));

            // Act
            var result = await _restCountriesApiClientMock.GetAllCountriesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsError);
            Assert.StartsWith("An unexpected error occurred:", result.Error);
        }

        [Fact]
        public async Task GetAllCountriesAsync_HttpClientException()
        {
            // Arrange
            _httpClientMessageHandlerMock.When(API_ENDPOINT)
                .Throw(new HttpRequestException());
            _httpClientFactoryMock.Setup(s => s.CreateClient(It.IsAny<string>())).Returns(new HttpClient(_httpClientMessageHandlerMock));

            // Act
            var result = await _restCountriesApiClientMock.GetAllCountriesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsError);
            Assert.StartsWith("HttpRequestException occurred:", result.Error);
        }

        [Fact]
        public async Task GetAllCountriesAsync_DeserializationError()
        {
            // Arrange
            _httpClientMessageHandlerMock.When(API_ENDPOINT)
                .Respond(HttpStatusCode.OK, "application/json", "[{Invalid JSON}]");

            _httpClientFactoryMock.Setup(s => s.CreateClient(It.IsAny<string>())).Returns(new HttpClient(_httpClientMessageHandlerMock));

            // Act
            var result = await _restCountriesApiClientMock.GetAllCountriesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsError);
            Assert.StartsWith("An unexpected error occurred:", result.Error);
        }
    }
}
