using Insurance.Core.Models;
using System.Collections.Generic;

namespace Insurance.Repository
{
    public interface IInsuranceRepository
    {
        IEnumerable<InsuranceModel> Get();
        InsuranceModel GetById(int id);
        void Delete(int id);
        InsuranceModel Add(InsuranceModel insurance);
        InsuranceModel Update(InsuranceModel insurance);
    }
}
