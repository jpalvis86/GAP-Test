using FluentAssertions;
using Insurance.Core.Models;
using Insurance.Repository;
using Insurance.WebAPI.Services;
using NSubstitute;
using System;
using System.Collections.Generic;
using Xunit;

namespace Insurance.UnitTests.Services
{
    public class CustomerServiceShould
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
            var repository = Substitute.For<ICustomerRepository>();
            repository.Get().Returns(customers);

            var service = new CustomerService(repository);

            // Act
            var result = service.GetAll();

            // Assert
            result.Should().HaveCount(customers.Count);
        }

        [Fact]
        public void ReturnSingleCustomer()
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

            var repository = Substitute.For<ICustomerRepository>();
            repository.GetById(customer.Id).Returns(customer);

            var service = new CustomerService(repository);

            // Act
            var result = service.GetById(customer.Id);

            // Assert
            result.Should().NotBeNull();
        }

    }
}
