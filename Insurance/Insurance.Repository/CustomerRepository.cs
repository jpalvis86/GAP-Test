using Insurance.Core.Models;
using System.Collections.Generic;

namespace Insurance.Repository
{
    public interface ICustomerRepository
    {
        IEnumerable<CustomerModel> Get();
        CustomerModel GetById(int id);
    }

    public class CustomerRepository : ICustomerRepository
    {
        public IEnumerable<CustomerModel> Get()
        {
            throw new System.NotImplementedException();
        }

        public CustomerModel GetById(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
