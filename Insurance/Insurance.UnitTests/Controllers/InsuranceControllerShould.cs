using FluentAssertions;
using Insurance.WebAPI.Controllers;
using Insurance.WebAPI.Models;
using Insurance.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System;
using System.Collections.Generic;
using Xunit;

namespace Insurance.UnitTests.Controllers
{
    public class InsuranceControllerShould
    {
        [Fact]
        public void ReturnOkWhenRetrievingExistingInsurances()
        {
            // Arrange
            var insuranceList = GetInsuranceList();

            var service = Substitute.For<IInsuranceService>();
            service.GetAll().Returns(insuranceList);

            var controller = new InsurancesController(service);

            // Act
            var response = controller.GetAllInsurances();

            // Assert
            var result = response as OkObjectResult;
            result.Should().NotBeNull();

            var records = result.Value as IEnumerable<InsuranceModel>;
            records.Should().NotBeEmpty();
        }

        [Fact]
        public void ReturnCreatedWhenAddingNewInsurance()
        {
            // Arrange
            var newInsurance = new InsuranceModel
            {
                Name = "New Insurance",
                Description = "New Insurance Description",
                CoverageRate = 0.9,
                CoverageTypes = new List<CoverageType> { CoverageType.Damage },
                Risk = Risk.Low,
                StartDate = DateTime.Today.AddDays(1),
                MonthsOfCoverage = 36,
                Price = 1199.99M
            };

            var service = Substitute.For<IInsuranceService>();
            service.Add(newInsurance).Returns(newInsurance);

            var controller = new InsurancesController(service);

            // Act
            var response = controller.Add(newInsurance);

            // Assert
            var result = response as CreatedResult;
            result.Should().NotBeNull();

            var record = result.Value as InsuranceModel;
            record.Should().NotBeNull();
        }

        [Fact]
        public void ReturnNoContentWhenDeletingInsurance()
        {
            // Arrange
            var service = Substitute.For<IInsuranceService>();
            var controller = new InsurancesController(service);

            // Act
            var response = controller.Delete(insuranceId: 1);

            // Assert
            var result = response as NoContentResult;
            result.Should().NotBeNull();
        }

        [Fact]
        public void ReturnOkWhenUpdatingAnExistingInsurance()
        {
            // Arrange
            var updatedInsurance = new InsuranceModel
            {
                Name = "Updated Insurance",
                Description = "Updated Insurance Description",
                Price = 99.99M
            };

            var service = Substitute.For<IInsuranceService>();
            service.Update(updatedInsurance).Returns(updatedInsurance);

            var controller = new InsurancesController(service);

            // Act
            var response = controller.UpdatePartial(updatedInsurance);

            // Assert
            var result = response as OkObjectResult;
            result.Should().NotBeNull();

            var record = result.Value as InsuranceModel;
            record.Should().NotBeNull();
        }


        private static IEnumerable<InsuranceModel> GetInsuranceList()
        {
            return new List<InsuranceModel>
            {
                new InsuranceModel
                {
                    Id = 1,
                    Name ="Test",
                    Description ="Test Insurance",
                    StartDate = DateTime.Today,
                    MonthsOfCoverage = 24,
                    CoverageRate = 0.5,
                    CoverageTypes = new List<CoverageType>{ CoverageType.Earthquake, CoverageType.Robbery },
                    Risk = Risk.Low,
                    Price = 999.99M,
                },
                new InsuranceModel
                {
                    Id = 2,
                    Name ="Test 2",
                    Description ="Test Insurance 2",
                    StartDate = DateTime.Today.AddMonths(1),
                    MonthsOfCoverage = 12,
                    CoverageRate = 0.25,
                    CoverageTypes = new List<CoverageType>{ CoverageType.Earthquake, CoverageType.Robbery, CoverageType.Fire, CoverageType.Damage },
                    Risk = Risk.High,
                    Price = 14999.99M,
                }
            };
        }
    }
}
