using Assignment.Core.Helpers;
using Assignment.Core.Models;

namespace Assignment.Core.Services
{
    public interface IRestCountriesApiClient
    {
        Task<DataOrError<IEnumerable<Country>>> GetAllCountriesAsync();
    }
}
