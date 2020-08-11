using FluentAssertions;
using Insurance.Core.Models;
using Insurance.UnitTests.Helpers;
using Insurance.WebAPI.Controllers;
using Insurance.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Insurance.UnitTests.Controllers
{
    /// <summary>
    /// Unit tests for the Insurances Controller.
    /// Here we validate the return codes more than the actual data
    /// </summary>
    public class InsurancesControllerShould
    {
        [Fact]
        public void ReturnOkWhenRetrievingExistingInsurances()
        {
            // Arrange
            var insuranceList = InsuranceRecordsHelper.GetInsurances();

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
        public void ReturnOkWhenRetrievingSingleInsurance()
        {
            // Arrange
            var insuranceList = InsuranceRecordsHelper.GetInsurances();

            var service = Substitute.For<IInsuranceService>();
            service.GetById(Arg.Any<int>()).Returns(insuranceList.First());

            var controller = new InsurancesController(service);

            // Act
            var response = controller.GetInsurance(id: 1);

            // Assert
            var result = response as OkObjectResult;
            result.Should().NotBeNull();

            var records = result.Value as InsuranceModel;
            records.Should().NotBeNull();
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

    }
}
