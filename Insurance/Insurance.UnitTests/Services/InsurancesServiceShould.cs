using FluentAssertions;
using Insurance.Core.Exceptions;
using Insurance.Core.Models;
using Insurance.Repository;
using Insurance.UnitTests.Helpers;
using Insurance.WebAPI.Services;
using NSubstitute;
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
        public void DeleteInsurance()
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
    }
}
