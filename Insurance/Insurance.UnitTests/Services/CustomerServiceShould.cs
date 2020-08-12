using FluentAssertions;
using Insurance.Core.Models;
using Insurance.Repository;
using Insurance.WebAPI.Services;
using NSubstitute;
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

    }
}
