using Insurance.Core.Models;
using System;

namespace Insurance.Core.Exceptions
{
    public class CustomerInsurancesDuplicatedException : Exception
    {
        public CustomerInsurancesDuplicatedException(InsuranceModel duplicatedInsurance)
            : base($"Customer Insurance with Id: {duplicatedInsurance.Id} and Name: {duplicatedInsurance.Name} must not be duplicated")
        {

        }
    }
}
