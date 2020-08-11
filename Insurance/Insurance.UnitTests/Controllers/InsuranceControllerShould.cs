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
        public void ReturnOkWithCollectionOfInsurances()
        {
            // Arrange
            var insuranceList = GetInsuranceList();

            var service = Substitute.For<IInsuranceService>();
            service.GetAll().Returns(insuranceList);

            var controller = new InsuranceController(service);

            // Act
            var response = controller.GetAllInsurances();

            // Assert
            var result = response as OkObjectResult;
            result.Should().NotBeNull();

            var records = result.Value as IEnumerable<InsuranceModel>;
            records.Should().NotBeEmpty();
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
