using System;

namespace Insurance.Core.Exceptions
{
    public class CustomerInsurancesMissingException : Exception
    {
        public CustomerInsurancesMissingException(int id) : base($"Customer Insurance with Id: {id} does not exist")
        {
        }
    }
}
