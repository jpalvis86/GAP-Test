using Insurance.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Insurance.Core.Exceptions
{
    public class InsuranceIsBeingUsedByCustomersException : Exception
    {
        public InsuranceIsBeingUsedByCustomersException(int insuranceId, IEnumerable<CustomerModel> customers)
            : base($"The insurance with id: {insuranceId} is being used by the following customers {GetCustomersData(customers)}")
        {

        }

        private static string GetCustomersData(IEnumerable<CustomerModel> customers)
        {
            return string.Join(',', customers.Select(c => $"Id: {c.Id} - Name: {c.Name}"));
        }
    }
}
