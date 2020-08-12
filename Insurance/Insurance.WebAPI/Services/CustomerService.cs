using Insurance.Core.Models;
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
        public IEnumerable<CustomerModel> GetAll()
        {
            // TODO: Implement this
            return new List<CustomerModel>();
        }

        public CustomerModel GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public CustomerModel Update(CustomerModel customer)
        {
            throw new System.NotImplementedException();
        }
    }
}
