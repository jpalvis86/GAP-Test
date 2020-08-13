using Insurance.Core.Models;
using Insurance.WebAPI.Models;
using Insurance.WebAPI.Services;
using Insurance.WebAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Insurance.WebAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            var customers = _customerService.GetAll();

            IEnumerable<CustomerViewModel> customersViewModel;

            if (customers is null || !customers.Any())
                return NoContent();

            customersViewModel = customers.Select(c => new CustomerViewModel
            {
                Id = c.Id,
                Name = c.Name
            });

            return Ok(customersViewModel);
        }

        [HttpGet]
        [Route(":id")]
        public IActionResult GetCustomer(int id)
        {
            var customer = _customerService.GetById(id);

            if (customer is null)
                return NoContent();

            return Ok(customer);
        }

        [HttpPost]
        public IActionResult Update(CustomerRequestModel customerParam)
        {
            var customerInsurances = new List<InsuranceModel>();

            if (customerParam.Insurances != null && customerParam.Insurances.Any())
            {
                customerInsurances = customerParam.Insurances.Select(i => new InsuranceModel
                {
                    Id = i
                }).ToList();
            }

            var customer = new CustomerModel
            {
                Id = customerParam.Id,
                Name = customerParam.Name,
                Insurances = customerInsurances
            };

            _customerService.Update(customer);

            return Ok(customerParam);
        }

    }
}
