using Insurance.Core.Models;
using Insurance.Repository;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Insurance.UnitTests.Stubs
{
    public class CustomerRepositoryStub : ICustomerRepository
    {
        private IEnumerable<CustomerModel> _customers;

        public CustomerRepositoryStub(IEnumerable<CustomerModel> customers)
        {
            _customers = customers;
        }

        public IEnumerable<CustomerModel> Get() => _customers;


        public CustomerModel GetById(int id) => _customers.SingleOrDefault(c => c.Id == id);

        public void Update(CustomerModel customer)
        {
            // Keep unchanged customers
            var unchangedCustomers = _customers.Where(c => c.Id != customer.Id).ToList();

            unchangedCustomers.Add(customer);
            _customers = unchangedCustomers;
        }
    }
}
