using FluentAssertions;
using Insurance.Core.Exceptions;
using Insurance.Core.Models;
using Insurance.Repository;
using Insurance.UnitTests.Helpers;
using Insurance.WebAPI.Services;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Insurance.UnitTests.Services
{
    public class InsurancesServiceShould
    {
        [Fact]
        public void ReturnAllAvailableInsurances()
        {
            // Arrange            
            var insuranceList = InsuranceRecordsHelper.GetInsurances();

            var repository = Substitute.For<IInsuranceRepository>();
            repository.Get().Returns(insuranceList);

            var service = new InsuranceService(repository);

            // Act
            var insurances = service.GetAll();

            // Assert
            insurances.Should().HaveCount(insuranceList.Count());
        }

        [Fact]
        public void ReturnSingleInsuranceIfExists()
        {
            // Arrange            
            var insuranceList = InsuranceRecordsHelper.GetInsurances();
            var insuranceId = 1;

            var repository = Substitute.For<IInsuranceRepository>();
            var insuranceFromList = insuranceList.Single(i => i.Id == insuranceId);
            repository.GetById(insuranceId).Returns(insuranceFromList);

            var service = new InsuranceService(repository);

            // Act
            var insurance = service.GetById(insuranceId);

            // Assert
            insurance.Should().NotBeNull();
        }

        [Fact]
        public void ReturnNullWhenInsuranceIdDoesNotExist()
        {
            // Arrange            
            var insuranceId = 999;

            var repository = Substitute.For<IInsuranceRepository>();
            InsuranceModel nullInsurance = null;
            repository.GetById(insuranceId).Returns(nullInsurance);

            var service = new InsuranceService(repository);

            // Act
            var insurance = service.GetById(insuranceId);

            // Assert
            insurance.Should().BeNull();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-999)]
        public void ThrowAnExceptionWhenIdIsLowerThanOne(int insuranceId)
        {
            // Arrange            
            var repository = Substitute.For<IInsuranceRepository>();
            var service = new InsuranceService(repository);

            // Act
            var exception = Record.Exception(() => service.GetById(insuranceId));

            // Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<InsuranceIdIsNotValidException>();
        }

        [Fact]
        public void DeleteInsuranceSuccessfully()
        {
            // Arrange            
            var insuranceId = 1;

            var repository = Substitute.For<IInsuranceRepository>();
            repository.Delete(insuranceId);

            var service = new InsuranceService(repository);

            // Act
            var exception = Record.Exception(() => service.Delete(insuranceId));

            // Assert
            exception.Should().BeNull();
        }


        [Fact]
        public void ThrowExceptionWhileDeletingInsuranceWhenInsuranceIsUsedByCustomers()
        {
            // Arrange            
            var insuranceId = 1;

            var customers = new List<CustomerModel>()
            {
                new CustomerModel
                {
                    Id = 1,
                    Name = "Jhon Doe",
                    Insurances = new List<InsuranceModel>
                    {
                        new InsuranceModel { Id = 1 }
                    }
                }
            };

            var repository = Substitute.For<IInsuranceRepository>();
            repository.GetCustomersByInsurance(insuranceId).Returns(customers);

            var expectedException = new InsuranceIsBeingUsedByCustomersException(insuranceId: 1, customers);
            var service = new InsuranceService(repository);

            // Act
            var exception = Record.Exception(() => service.Delete(insuranceId));

            // Assert
            exception.Should().NotBeNull();
            exception.Message.Should().Be(expectedException.Message);

        }

        [Fact]
        public void AddInsuranceSuccessfullyWhenDataIsValid()
        {
            // Arrange            
            var insurance = new InsuranceModel
            {
                Name = "Test Insurance",
                Description = "Test Insurance Description",
                CoverageRate = 0.4,
                CoverageTypes = new List<CoverageType> { CoverageType.Damage },
                StartDate = DateTime.Today,
                MonthsOfCoverage = 12,
                Price = 101.99M
            };

            var addedInsurance = new InsuranceModel
            {
                Id = 1,
                Name = "Test Insurance",
                Description = "Test Insurance Description",
                CoverageRate = 0.4,
                CoverageTypes = new List<CoverageType> { CoverageType.Damage },
                StartDate = DateTime.Today,
                MonthsOfCoverage = 12,
                Price = 99.99M
            };

            var repository = Substitute.For<IInsuranceRepository>();
            repository.Add(insurance).Returns(addedInsurance);

            var service = new InsuranceService(repository);

            // Act
            var newInsurance = service.Add(insurance);

            // Assert
            newInsurance.Should().NotBeNull();
            newInsurance.Id.Should().Be(addedInsurance.Id);
        }

        [Theory]
        [InlineData(InsuranceInvalidModelScenarioEnum.StartDateIsNotValid)]
        [InlineData(InsuranceInvalidModelScenarioEnum.CoverageRateIsNotValid)]
        [InlineData(InsuranceInvalidModelScenarioEnum.CoverageRateNotValidForTheRiskProfile)]
        [InlineData(InsuranceInvalidModelScenarioEnum.MonthsPeriodIsNotValid)]
        [InlineData(InsuranceInvalidModelScenarioEnum.PriceIsNotValid)]
        public void ThrowAnExceptionWhileAddingInsuranceWhenDataIsNotValid(InsuranceInvalidModelScenarioEnum scenario)
        {
            // Arrange            
            var (invalidInsuranceModel, expectedException) = GetInvalidModelFromScenario(scenario);
            var repository = Substitute.For<IInsuranceRepository>();
            var service = new InsuranceService(repository);

            // Act
            var exception = Record.Exception(() => service.Add(invalidInsuranceModel));

            // Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType(expectedException.GetType());
        }

        [Fact]
        public void UpdateInsuranceSuccessfullyWhenDataIsValid()
        {
            // Arrange            
            var insurance = new InsuranceModel
            {
                Id = 1,
                Name = "Test Insurance",
                Description = "Test Insurance Description",
                CoverageRate = 0.4,
                CoverageTypes = new List<CoverageType> { CoverageType.Damage },
                StartDate = DateTime.Today,
                MonthsOfCoverage = 12,
                Price = 101.99M
            };

            var updatedInsurance = new InsuranceModel
            {
                Id = 1,
                Name = "Updated Test Insurance",
                Description = "Updated Test Insurance Description",
                CoverageRate = 0.3,
                CoverageTypes = new List<CoverageType> { CoverageType.Damage, CoverageType.Earthquake },
                StartDate = DateTime.Today.AddMonths(1),
                MonthsOfCoverage = 24,
                Price = 299.99M
            };

            var repository = Substitute.For<IInsuranceRepository>();
            repository.Update(insurance).Returns(updatedInsurance);

            var service = new InsuranceService(repository);

            // Act
            var returnedInsurance = service.Update(insurance);

            // Assert
            returnedInsurance.Should().NotBeNull();
            returnedInsurance.Name.Should().Be(updatedInsurance.Name);
            returnedInsurance.Description.Should().Be(updatedInsurance.Description);
            returnedInsurance.Price.Should().Be(updatedInsurance.Price);
        }

        [Theory]
        [InlineData(InsuranceInvalidModelScenarioEnum.StartDateIsNotValid)]
        [InlineData(InsuranceInvalidModelScenarioEnum.CoverageRateIsNotValid)]
        [InlineData(InsuranceInvalidModelScenarioEnum.CoverageRateNotValidForTheRiskProfile)]
        [InlineData(InsuranceInvalidModelScenarioEnum.MonthsPeriodIsNotValid)]
        [InlineData(InsuranceInvalidModelScenarioEnum.PriceIsNotValid)]
        public void ThrowAnExceptionWhileUpdatingInsuranceWhenDataIsNotValid(InsuranceInvalidModelScenarioEnum scenario)
        {
            // Arrange            
            var (invalidInsuranceModel, expectedException) = GetInvalidModelFromScenario(scenario);
            var repository = Substitute.For<IInsuranceRepository>();
            var service = new InsuranceService(repository);

            // Act
            var exception = Record.Exception(() => service.Update(invalidInsuranceModel));

            // Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType(expectedException.GetType());
        }

        [Fact]
        public void ThrowAnExceptionWhileUpdatingInsuranceWhenIdDoesNotExist()
        {
            // Arrange            
            var insurance = new InsuranceModel
            {
                Id = 1000,
                Name = "Test Insurance",
                Description = "Test Insurance Description",
                CoverageRate = 0.4,
                CoverageTypes = new List<CoverageType> { CoverageType.Damage },
                StartDate = DateTime.Today,
                MonthsOfCoverage = 12,
                Price = 101.99M
            };

            var repository = Substitute.For<IInsuranceRepository>();
            InsuranceModel updatedInsurance = null; // Entity does not exist in DB
            repository.Update(insurance).Returns(updatedInsurance);

            var service = new InsuranceService(repository);

            // Act
            var exception = Record.Exception(() => service.Update(insurance));

            // Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<InsuranceDoesNotExistException>();
        }

        /// <summary>
        /// Returns tuple of an instance of InsuranceModel and an instance of an Exception
        /// based on the scenario required for testing.
        /// 
        /// This is helpful to arrange the conditions for testing based on the specific scenario
        /// we want to validate.
        /// </summary>
        /// <param name="scenario"></param>
        /// <returns></returns>
        private (InsuranceModel model, Exception expectedException) GetInvalidModelFromScenario(InsuranceInvalidModelScenarioEnum scenario)
        {
            InsuranceModel model;
            Exception expectedException;

            switch (scenario)
            {
                case InsuranceInvalidModelScenarioEnum.StartDateIsNotValid:
                    var invalidDate = DateTime.Now.AddDays(-1); // Yesterday

                    expectedException = new InsuranceStartDateIsNotValidException(invalidDate);
                    model = new InsuranceModel { StartDate = invalidDate };

                    break;

                case InsuranceInvalidModelScenarioEnum.CoverageRateIsNotValid:
                    var invalidCoverageRate = 1.1; // Greater than 100%

                    expectedException = new InsuranceCoverageRateIsNotValidException(invalidCoverageRate);
                    model = new InsuranceModel
                    {
                        StartDate = DateTime.Today,
                        CoverageRate = invalidCoverageRate
                    };

                    break;

                case InsuranceInvalidModelScenarioEnum.MonthsPeriodIsNotValid:
                    var invalidMonthsPeriod = 0; // Lower than 1

                    expectedException = new InsuranceMonthsPeriodIsNotValidException(invalidMonthsPeriod);
                    model = new InsuranceModel
                    {
                        StartDate = DateTime.Today,
                        CoverageRate = 0.5,
                        MonthsOfCoverage = invalidMonthsPeriod
                    };

                    break;

                case InsuranceInvalidModelScenarioEnum.PriceIsNotValid:
                    var invalidPrice = 99M; // Lower than 100

                    expectedException = new InsurancePriceIsNotValidException(invalidPrice);
                    model = new InsuranceModel
                    {
                        StartDate = DateTime.Today,
                        CoverageRate = 0.5,
                        MonthsOfCoverage = 12,
                        Price = invalidPrice
                    };

                    break;

                case InsuranceInvalidModelScenarioEnum.CoverageRateNotValidForTheRiskProfile:
                    var riskProfile = Risk.High;
                    var coverageRate = 0.51; // 51% -> Should fail according to business rule

                    expectedException = new InsuranceCoverageRateForHighRiskProfileIsNotValidException(coverageRate);
                    model = new InsuranceModel
                    {
                        Risk = riskProfile,
                        StartDate = DateTime.Today,
                        CoverageRate = coverageRate,
                        MonthsOfCoverage = 12,
                        Price = 100M
                    };

                    break;

                default:
                    throw new ArgumentOutOfRangeException("Scenario is not valid");
            }

            return (model, expectedException);
        }

        public enum InsuranceInvalidModelScenarioEnum
        {
            StartDateIsNotValid,
            CoverageRateIsNotValid,
            MonthsPeriodIsNotValid,
            PriceIsNotValid,
            CoverageRateNotValidForTheRiskProfile
        }
    }


}
