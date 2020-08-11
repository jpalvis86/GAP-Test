using System;

namespace Insurance.Core.Exceptions
{
    public class InsuranceCoverageRateForHighRiskProfileIsNotValidException : Exception
    {
        public InsuranceCoverageRateForHighRiskProfileIsNotValidException(double invalidCoverageRate)
            : base($"Insurance Coverage Rate: {invalidCoverageRate} is not valid. It must not exceed 50% for a High Risk Profile")
        {

        }
    }
}
