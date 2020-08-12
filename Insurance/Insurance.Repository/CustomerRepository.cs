using Insurance.Core.Models;
using System.Collections.Generic;

namespace Insurance.Repository
{
    public interface ICustomerRepository
    {
        IEnumerable<CustomerModel> Get();
    }

    public class CustomerRepository : ICustomerRepository
    {
        public IEnumerable<CustomerModel> Get()
        {
            throw new System.NotImplementedException();
        }
    }
}
