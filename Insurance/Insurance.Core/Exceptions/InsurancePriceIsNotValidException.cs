using System;

namespace Insurance.Core.Exceptions
{
    public class InsurancePriceIsNotValidException : Exception
    {
        public InsurancePriceIsNotValidException(decimal invalidPrice)
            : base($"Insurance Price: {invalidPrice} is not valid. It must be higher than 100")
        {

        }
    }
}
