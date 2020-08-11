using System;

namespace Insurance.Core.Exceptions
{
    public class InsuranceMonthsPeriodIsNotValidException : Exception
    {
        public InsuranceMonthsPeriodIsNotValidException(double invalidMonthsPeriod)
            : base($"Insurance Months Period: {invalidMonthsPeriod} is not valid. It must be greater than 1")
        {

        }
    }
}
