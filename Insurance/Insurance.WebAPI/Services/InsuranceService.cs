using Insurance.WebAPI.Models;
using System.Collections.Generic;

namespace Insurance.WebAPI.Services
{
    public class InsuranceService : IInsuranceService
    {
        // TODO: Implement methods

        public InsuranceModel Add(InsuranceModel insurance)
        {
            return insurance;
        }

        public IEnumerable<InsuranceModel> GetAll()
        {
            return new List<InsuranceModel>();
        }
    }
}
