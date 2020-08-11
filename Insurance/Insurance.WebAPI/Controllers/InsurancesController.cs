using Insurance.WebAPI.Models;
using Insurance.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InsurancesController : ControllerBase
    {
        private readonly IInsuranceService _insuranceService;

        public InsurancesController(IInsuranceService insuranceService)
        {
            _insuranceService = insuranceService;
        }

        [HttpGet]
        public IActionResult GetAllInsurances()
        {
            var insurances = _insuranceService.GetAll();
            return Ok(insurances);
        }

        [HttpPost]
        public IActionResult Add(InsuranceModel insurance)
        {
            var newInsurance = _insuranceService.Add(insurance);
            return Created($"/insurances/{insurance.Id}", newInsurance);
        } 
        
        [HttpPatch]
        public IActionResult UpdatePartial(InsuranceModel insurance)
        {
            var updatedInsurance = _insuranceService.Update(insurance);
            return Ok(updatedInsurance);
        }

        [HttpDelete]
        public IActionResult Delete(int insuranceId)
        {
            _insuranceService.Delete(insuranceId);
            return NoContent();
        }
    }
}
