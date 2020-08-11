using FluentAssertions;
using Insurance.WebAPI.Controllers;
using System;
using Xunit;

namespace Insurance.UnitTests.Controllers
{
    public class InsuranceControllerShould
    {
        [Fact]
        public void ReturnCollectionOfInsurances()
        {
            // Arrange
            var controller = new InsuranceController();

            // Act
            var result = controller.Get();

            // Assert
            result.Should().NotBeNull();
        }
    }
}
