using Insurance.Core.Models;
using System.Collections.Generic;

namespace Insurance.WebAPI.Services
{
    public interface ICustomerService
    {
        IEnumerable<CustomerModel> GetAll();
    }

    public class CustomerService : ICustomerService
    {
        public IEnumerable<CustomerModel> GetAll()
        {
            // TODO: Implement this
            return new List<CustomerModel>();
        }
    }
}
