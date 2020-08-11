using Insurance.Core.Models;
using System.Collections.Generic;

namespace Insurance.Repository
{
    public interface IInsuranceRepository
    {
        IEnumerable<InsuranceModel> Get();
        InsuranceModel GetById(int id);
    }
}
