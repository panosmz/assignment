using Assignment.Core.Helpers;
using Assignment.Core.Models;
using System.Text.Json;

namespace Assignment.Core.Services.Impl
{
    public class RestCountriesApiClient : IRestCountriesApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RestCountriesApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<DataOrError<IEnumerable<Country>>> GetAllCountriesAsync()
        {
            var client = _httpClientFactory.CreateClient();
            try
            {
                var response = await client.GetAsync("https://restcountries.com/v3.1/all?fields=name,capital,borders,cca3");

                if (response.IsSuccessStatusCode)
                {

                    var jsonContent = await response.Content.ReadAsStringAsync();
                    var res = JsonSerializer.Deserialize<RestCountriesApi_Country[]>(jsonContent);

                    if (res.Count() == 0)
                        return new DataOrError<IEnumerable<Country>>("Country list is empty.");

                    var retVal = res.Select(s =>
                        new Country
                        {
                            Id = s.cca3,
                            Capital = s.capital.FirstOrDefault() ?? string.Empty,
                            CommonName = s.name.common,
                            Borders = s.borders
                        }
                    );

                    return new DataOrError<IEnumerable<Country>>(retVal);
                }
                else
                {
                    return new DataOrError<IEnumerable<Country>>($"Failed to retrieve data from API. Status code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                return new DataOrError<IEnumerable<Country>>($"HttpRequestException occurred: {ex.Message}");
            }
            catch (Exception ex)
            {
                return new DataOrError<IEnumerable<Country>>($"An unexpected error occurred: {ex.Message}");
            }
        }

        #region RestCountries Api Response
        private class RestCountriesApi_Country
        {
            public RestCountriesApi_Country_Name name { get; set; }
            public string[] capital { get; set; }
            public string[] borders { get; set; }
            public string cca3 { get; set; }
        }

        private class RestCountriesApi_Country_Name
        {
            public string common { get; set; }
            public string official { get; set; }
            public object nativeName { get; set; }
        }
        #endregion


    }
}
