using Assignment.API.Models;
using Assignment.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Annotations;

namespace Assignment.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RequestObjController : ControllerBase
    {
        private ISecondLargestService _secondLargestService;
        private readonly ILogger<RequestObjController> _logger;

        public RequestObjController(
            ISecondLargestService secondLargestService,
            ILogger<RequestObjController> logger
            )
        {
            _secondLargestService = secondLargestService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves the second largest element from the provided array.
        /// </summary>
        [HttpPost]
        public IActionResult GetSecondLargest([FromBody] RequestObj requestObj)
        {
            if (requestObj == null || requestObj.RequestArrayObj == null || !requestObj.RequestArrayObj.Any())
            {
                return BadRequest("Request body is empty or invalid.");
            }

            var secondLargest = _secondLargestService.FindSecondLargest(requestObj.RequestArrayObj);

            if (secondLargest.IsError)
            {
                _logger.LogError(secondLargest.Error);
                return BadRequest("There is no second largest element in the array.");
            }

            return Ok(secondLargest.Data);
        }
    }
}
