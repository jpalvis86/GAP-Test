﻿using FluentAssertions;
using Insurance.Core.Models;
using Insurance.WebAPI.Controllers;
using Insurance.WebAPI.Services;
using Insurance.WebAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.Core.DependencyInjection;
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

            var records = result.Value as CustomerModel;
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

        [Fact]
        public void ReturnOkWhenUpdatingCustomerInsurances()
        {
            // Arrange
            var insurances = new List<InsuranceModel>
            {
                new InsuranceModel
                {
                    Id = 1,
                    Name = "Test",
                    Description = "Test Insurance",
                    StartDate = DateTime.Today,
                    MonthsOfCoverage = 24,
                    CoverageRate = 0.5,
                    CoverageTypes = new List<CoverageType> { CoverageType.Earthquake, CoverageType.Robbery },
                    Risk = Risk.Low,
                    Price = 999.99M,
                } 
            };

            var customer = new CustomerModel
            {
                Id = 1,
                Name = "Jhon Doe",
                Insurances = insurances
            };

            var service = Substitute.For<ICustomerService>();
            service.Update(customer).Returns(customer);

            var controller = new CustomersController(service);

            // Act
            var response = controller.Update(customer);

            // Assert
            var result = response as OkObjectResult;
            result.Should().NotBeNull();

            var records = result.Value as CustomerModel;
            records.Should().NotBeNull();
        }
    }
}
