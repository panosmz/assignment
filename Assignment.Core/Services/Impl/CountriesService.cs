using Assignment.Core.Helpers;
using Assignment.Core.Models;
using Assignment.Core.Models.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Assignment.Core.Services.Impl
{
    public class CountriesService : ICountriesService
    {
        private readonly AssignmentContext _dbContext;
        private readonly IRestCountriesApiClient _restCountriesApiClient;
        private readonly ILogger<CountriesService> _logger;
        private readonly IMemoryCache _cache;
        private readonly IMapper _mapper;

        private const string CacheKey = "COUNTRIES_ALL";

        public CountriesService(
            AssignmentContext dbContext,
            IRestCountriesApiClient restCountriesApiClient,
            IMemoryCache memoryCache,
            ILogger<CountriesService> logger,
            IMapper mapper
            )
        {
            _dbContext = dbContext;
            _restCountriesApiClient = restCountriesApiClient;
            _cache = memoryCache;
            _logger = logger;
            _mapper = mapper;
        }


        public async Task<DataOrError<IEnumerable<CountryDTO>>> GetCountriesAsync()
        {
            try
            {
                IEnumerable<CountryDTO> countriesDto;
                if (!_cache.TryGetValue(CacheKey, out countriesDto))
                {
                    IEnumerable<Country> countries = await FetchCountriesAsync();
                    countriesDto = _mapper.Map<IEnumerable<CountryDTO>>(countries);
                    _cache.Set(CacheKey, countriesDto);
                }

                return new DataOrError<IEnumerable<CountryDTO>>(countriesDto);
            }
            catch (Exception ex)
            {
                return new DataOrError<IEnumerable<CountryDTO>>(ex.Message);
            }

        }

        private async Task<IEnumerable<Country>> FetchCountriesAsync()
        {
            var dbResults = await _dbContext.Countries.ToListAsync();

            if (dbResults != null && dbResults.Count > 0)
            {
                return dbResults;
            }

            var apiResponse = await _restCountriesApiClient.GetAllCountriesAsync();

            if (apiResponse.IsError)
            {
                throw new Exception(apiResponse.Error);
            }

            await _dbContext.Countries.AddRangeAsync(apiResponse.Data);
            await _dbContext.SaveChangesAsync();

            return apiResponse.Data;
        }
    }
}
