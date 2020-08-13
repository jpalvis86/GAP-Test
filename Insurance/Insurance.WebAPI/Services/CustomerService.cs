using Insurance.Core.Models;
using Insurance.Repository;
using System.Collections.Generic;

namespace Insurance.WebAPI.Services
{
    public interface ICustomerService
    {
        IEnumerable<CustomerModel> GetAll();
        CustomerModel GetById(int id);
        CustomerModel Update(CustomerModel customer);
    }

    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public IEnumerable<CustomerModel> GetAll() => _customerRepository.Get();

        public CustomerModel GetById(int id) => _customerRepository.GetById(id);

        public CustomerModel Update(CustomerModel customer)
        {
            // TODO: Validate object
            _customerRepository.Update(customer);

            return customer;
        }
    }
}
