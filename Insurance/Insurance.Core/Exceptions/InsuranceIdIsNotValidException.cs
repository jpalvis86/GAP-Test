using System;

namespace Insurance.Core.Exceptions
{
    public class InsuranceIdIsNotValidException : Exception
    {
        public InsuranceIdIsNotValidException(int insuranceId)
            : base($"Insurance Id: {insuranceId} must be greater than 0")
        {
        }
    }
}
