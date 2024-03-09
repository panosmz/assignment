using Assignment.Core.Helpers;
using Assignment.Core.Models.DTO;
namespace Assignment.Core.Services
{
    public interface ICountriesService
    {
        Task<DataOrError<IEnumerable<CountryDTO>>> GetCountriesAsync();
    }
}
