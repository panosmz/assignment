using Assignment.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountriesController : ControllerBase
    {
        private ICountriesService _countriesService;
        private readonly ILogger<CountriesController> _logger;

        public CountriesController(
            ICountriesService countriesService,
            ILogger<CountriesController> logger
            )
        {
            _countriesService = countriesService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all countries from restcountries.com
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {
            var res = await _countriesService.GetCountriesAsync();

            if (res.IsError)
            {
                _logger.LogError(res.Error);
                return StatusCode(500);
            }

            return Ok(res.Data);
        }
    }
}
