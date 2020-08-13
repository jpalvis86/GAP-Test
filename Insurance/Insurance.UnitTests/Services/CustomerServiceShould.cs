using FluentAssertions;
using Insurance.Core.Exceptions;
using Insurance.Core.Models;
using Insurance.Repository;
using Insurance.WebAPI.Services;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

        [Fact]
        public void UpdateCustomerInsurancesSuccessfully()
        {
            // Arrange
            var initialCustomer = new CustomerModel
            {
                Id = 1,
                Name = "Jhon Doe",
                Insurances = new List<InsuranceModel>
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
                }
            };

            // To validate state, it's better to use a custom Stub
            var initialCustomers = new List<CustomerModel> { initialCustomer };
            ICustomerRepository repository = new Stubs.CustomerRepositoryStub(initialCustomers);

            var service = new CustomerService(repository);

            var updatedCustomer = new CustomerModel
            {
                Id = 1,
                Name = "Jhon Doe",
                Insurances = new List<InsuranceModel>
                {
                    new InsuranceModel
                    {
                        Id = 2,
                        Name = "New Insurance",
                        Description = "New Insurance",
                        StartDate = DateTime.Today,
                        MonthsOfCoverage = 12,
                        CoverageRate = 0.9,
                        CoverageTypes = new List<CoverageType> { CoverageType.Fire },
                        Risk = Risk.Low,
                        Price = 599.99M,
                    }
                }
            };

            // Act
            var result = service.Update(updatedCustomer);

            // Assert
            result.Should().NotBeNull();

            // Insurance 1 should be removed and only 2 should remain
            var updatedCustomerRecord = repository.GetById(updatedCustomer.Id);
            updatedCustomerRecord.Insurances.Should().HaveCount(1);
            updatedCustomerRecord.Insurances.First().Id.Should().Be(2);
        }

        [Theory]
        [InlineData(InvalidCustomerInsurancesUpdateScenarios.RepeatedInsurance)]
        [InlineData(InvalidCustomerInsurancesUpdateScenarios.MissingInsurance)]
        public void ThrowExceptionWhileUpdatingCustomerInsurancesWhenDataIsNotValid(InvalidCustomerInsurancesUpdateScenarios scenario)
        {
            // Arrange
            var initialCustomer = new CustomerModel
            {
                Id = 1,
                Name = "Jhon Doe",
                Insurances = new List<InsuranceModel>
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
                }
            };

            // To validate state, it's better to use a custom Stub
            var initialCustomers = new List<CustomerModel> { initialCustomer };
            ICustomerRepository repository = new Stubs.CustomerRepositoryStub(initialCustomers);

            var (updatedCustomer, expectedException) = GetModelAndExceptionForScenario(scenario);
            var service = new CustomerService(repository);

            // Act
            var exception = Record.Exception(() => service.Update(updatedCustomer));

            // Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be(expectedException.Message);
        }

        public (CustomerModel updatedCustomer, Exception expectedException) GetModelAndExceptionForScenario(InvalidCustomerInsurancesUpdateScenarios scenario)
        {
            CustomerModel updatedCustomer = null;
            Exception expectedException = null;

            switch (scenario)
            {
                case InvalidCustomerInsurancesUpdateScenarios.RepeatedInsurance:
                    var insurance = new InsuranceModel
                    {
                        Id = 2,
                        Name = "New Insurance",
                        Description = "New Insurance",
                        StartDate = DateTime.Today,
                        MonthsOfCoverage = 12,
                        CoverageRate = 0.9,
                        CoverageTypes = new List<CoverageType> { CoverageType.Fire },
                        Risk = Risk.Low,
                        Price = 599.99M,
                    };

                    updatedCustomer = new CustomerModel
                    {
                        Id = 1,
                        Name = "Jhon Doe",
                        Insurances = new List<InsuranceModel> { insurance, insurance } // Duplicated
                    };

                    expectedException = new CustomerInsurancesDuplicatedException(insurance);
                    break;
                case InvalidCustomerInsurancesUpdateScenarios.MissingInsurance:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Scenario is not valid");
            }

            return (updatedCustomer, expectedException);
        }

        public enum InvalidCustomerInsurancesUpdateScenarios
        {
            RepeatedInsurance,
            MissingInsurance
        }
    }
}
