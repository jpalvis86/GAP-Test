using Microsoft.AspNetCore.Mvc;

namespace Insurance.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InsuranceController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Works");
        }
    }
}
