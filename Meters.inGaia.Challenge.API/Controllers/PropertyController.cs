using System.Threading.Tasks;
using Meters.inGaia.Challenge.API.Models;
using Meters.inGaia.Challenge.API.Services.Property.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Meters.inGaia.Challenge.API.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class PropertyController : ControllerBase
    {
        private readonly ILogger<PropertyController> logger;
        private readonly IPropertyService propertyService;

        public PropertyController(
            ILogger<PropertyController> logger,
            IPropertyService propertyService)
        {
            this.logger = logger;
            this.propertyService = propertyService;
        }

        /// <summary>
        /// Get square meter price.
        /// </summary>
        /// <returns><see cref="MeterPrice"/>.</returns>
        [HttpGet("squareMeterPrice")]
        public async Task<IActionResult> GetSquareMeterPrice()
        {
            var response = await propertyService.GetSquareMeterPrice();

            if (response == null)
            {
                return StatusCode(404);
            }

            return Ok(response);
        }

        /// <summary>
        /// Get property value based on square meters provided.
        /// </summary>
        /// <remarks> Should use only meters with dot, API does not accepts comma.</remarks>
        /// <param name="meters"></param>
        /// <returns><see cref="Property"/>.</returns>
        [HttpGet("propertyValue/{meters}")]
        public async Task<IActionResult> GetPropertyValue(string meters)
        {
            var response = await propertyService.GetPropertyValue(meters);

            if (response == null)
            {
                return StatusCode(404);
            }

            if (!string.IsNullOrEmpty(response.Error))
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
