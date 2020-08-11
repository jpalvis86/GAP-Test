using Insurance.WebAPI.Models;
using System.Collections.Generic;

namespace Insurance.WebAPI.Services
{
    public class InsuranceService : IInsuranceService
    {
        public IEnumerable<InsuranceModel> GetAll()
        {
            return new List<InsuranceModel>();
        }
    }
}
