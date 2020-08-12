using Insurance.WebAPI.Services;
using Insurance.WebAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
