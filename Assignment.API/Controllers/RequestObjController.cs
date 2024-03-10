using Assignment.API.Models;
using Assignment.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Annotations;

namespace Assignment.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
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
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /RequestObj
        ///     {
        ///        "requestArrayObj": [5, 10, 15, 20]
        ///     }
        ///
        /// </remarks>
        /// <param name="requestObj">The request object containing an array of integers.</param>
        /// <returns>The second largest element in the array.</returns>
        [HttpPost]
        [SwaggerResponse(200, "The second largest element in the array.", typeof(int))]
        [SwaggerResponse(400, "If the request body is empty, invalid, or there is no second largest element in the array.")]
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
