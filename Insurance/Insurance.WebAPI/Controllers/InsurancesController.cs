using Insurance.Core.Exceptions;
using Insurance.Core.Models;
using Insurance.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Insurance.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

        [HttpGet]
        [Route(":id")]
        public IActionResult GetInsurance(int id)
        {
            try
            {
                var insurance = _insuranceService.GetById(id);

                if (insurance != null)
                    return Ok(insurance);
                else
                    return NoContent();
            }
            catch (Exception ex) when (ex is InsuranceIdIsNotValidException)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Add(InsuranceModel insurance)
        {
            var newInsurance = _insuranceService.Add(insurance);
            return Created($"/insurances/{insurance.Id}", newInsurance);
        }

        [HttpPut]
        public IActionResult Update(InsuranceModel insurance)
        {
            try
            {
                var updatedInsurance = _insuranceService.Update(insurance);
                return Ok(updatedInsurance);
            }
            catch (Exception ex) when (ex is InsuranceDoesNotExistException)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult Delete(int insuranceId)
        {
            _insuranceService.Delete(insuranceId);
            return NoContent();
        }
    }
}
