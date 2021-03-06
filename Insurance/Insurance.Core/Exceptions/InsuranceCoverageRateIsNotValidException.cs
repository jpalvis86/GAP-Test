﻿using System;

namespace Insurance.Core.Exceptions
{
    public class InsuranceCoverageRateIsNotValidException : Exception
    {
        public InsuranceCoverageRateIsNotValidException(double invalidCoverageRate)
            : base($"Insurance Coverage Rate: {invalidCoverageRate * 100 }% is not valid. It must be between 1% and 100%")
        {

        }
    }
}
