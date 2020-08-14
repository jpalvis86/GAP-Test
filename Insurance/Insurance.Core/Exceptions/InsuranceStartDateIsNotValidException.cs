using System;

namespace Insurance.Core.Exceptions
{
    public class InsuranceStartDateIsNotValidException : Exception
    {
        public InsuranceStartDateIsNotValidException(DateTime startDate)
            : base($"Insurance Start Date: {startDate.ToString("dd/MM/yyyy")} is not valid. It must be in the future")
        {

        }
    }
}
