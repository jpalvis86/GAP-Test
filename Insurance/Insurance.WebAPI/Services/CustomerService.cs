using Insurance.Core.Exceptions;
using Insurance.Core.Models;
using Insurance.Repository;
using Insurance.WebAPI.Models;
using System.Collections.Generic;
using System.Linq;

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
        private readonly IInsuranceRepository _insuranceRepository;

        public CustomerService(ICustomerRepository customerRepository
                            , IInsuranceRepository insuranceRepository)
        {
            _customerRepository = customerRepository;
            _insuranceRepository = insuranceRepository;
        }

        public IEnumerable<CustomerModel> GetAll() => _customerRepository.Get();

        public CustomerModel GetById(int id) => _customerRepository.GetById(id);

        public CustomerModel Update(CustomerModel customer)
        {
            ValidateCustomerInsurances(customer);

            _customerRepository.Update(customer);

            return customer;
        }

        private void ValidateCustomerInsurances(CustomerModel customer)
        {
            ValidateDuplicatedInsurances(customer);
            ValidateMissingInsurances(customer);
        }

        private void ValidateMissingInsurances(CustomerModel customer)
        {
            foreach (var insurance in customer.Insurances)
            {
                if (_insuranceRepository.GetById(insurance.Id) is null)
                    throw new CustomerInsurancesMissingException(insurance.Id);
            }
        }

        private static void ValidateDuplicatedInsurances(CustomerModel customer)
        {
            var groupedCustomerInsurances = from i in customer.Insurances
                                            group i by i.Id into groupedInsurances
                                            select new
                                            {
                                                groupedInsurances.Key,
                                                Count = groupedInsurances.Count()
                                            };

            var duplicated = groupedCustomerInsurances.FirstOrDefault(c => c.Count > 1);
            if (duplicated != null)
            {
                var insurance = customer.Insurances.First(i => i.Id == duplicated.Key);
                throw new CustomerInsurancesDuplicatedException(insurance);
            }
        }
    }
}
