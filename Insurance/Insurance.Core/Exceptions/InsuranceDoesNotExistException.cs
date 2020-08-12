using System;

namespace Insurance.Core.Exceptions
{
    public class InsuranceDoesNotExistException : Exception
    {
        public InsuranceDoesNotExistException(int id)
            : base($"Insurance with id: {id} does not exist.")
        {

        }
    }
}
