using FluentAssertions;
using Insurance.Core.Models;
using Insurance.WebAPI.Controllers;
using Insurance.WebAPI.Services;
using Insurance.WebAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Insurance.UnitTests.Controllers
{
    public class CustomersControllerShould
    {
        [Fact]
        public void ReturnAllCustomers()
        {
            // Arrange
            var customers = new List<CustomerModel>
            {
                new CustomerModel{ Id = 1, Name = "Jhon Doe"},
                new CustomerModel{ Id = 2, Name = "Michael Jackson"},
                new CustomerModel{ Id = 3, Name = "Steve Irwin"}
            };
            var service = Substitute.For<ICustomerService>();
            service.GetAll().Returns(customers);

            var controller = new CustomersController(service);

            // Act
            var response = controller.GetAllCustomers();

            // Assert
            var result = response as OkObjectResult;
            result.Should().NotBeNull();

            var records = result.Value as IEnumerable<CustomerViewModel>;
            records.Should().HaveCount(customers.Count);
        }
    }
}
