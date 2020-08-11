﻿using Insurance.WebAPI.Models;
using Insurance.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InsuranceController : ControllerBase
    {
        private readonly IInsuranceService _insuranceService;

        public InsuranceController(IInsuranceService insuranceService)
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
    }
}
