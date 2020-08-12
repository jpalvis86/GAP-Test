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
        public void ReturnOkWhenRetrievingCustomers()
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

        [Fact]
        public void ReturnNoContentWhenThereAreNoCustomers()
        {
            // Arrange
            var customers = new List<CustomerModel>();
            var service = Substitute.For<ICustomerService>();
            service.GetAll().Returns(customers);

            var controller = new CustomersController(service);

            // Act
            var response = controller.GetAllCustomers();

            // Assert
            var result = response as NoContentResult;
            result.Should().NotBeNull();
        }

        [Fact]
        public void ReturnOkWhenRetrievingSingleCustomer()
        {
            // Arrange
            var customer = new CustomerModel { Id = 1, Name = "Jhon Doe" };

            var service = Substitute.For<ICustomerService>();
            service.GetById(customer.Id).Returns(customer);

            var controller = new CustomersController(service);

            // Act
            var response = controller.GetCustomer(customer.Id);

            // Assert
            var result = response as OkObjectResult;
            result.Should().NotBeNull();

            var records = result.Value as CustomerViewModel;
            records.Should().NotBeNull();
        }

        [Fact]
        public void ReturnNoContentWhenRetrievingMissingCustomer()
        {
            // Arrange
            CustomerModel customer = null;

            var service = Substitute.For<ICustomerService>();
            service.GetById(Arg.Any<int>()).Returns(customer);

            var controller = new CustomersController(service);

            // Act
            var response = controller.GetCustomer(Arg.Any<int>());

            // Assert
            var result = response as NoContentResult;
            result.Should().NotBeNull();
        }
    }
}
