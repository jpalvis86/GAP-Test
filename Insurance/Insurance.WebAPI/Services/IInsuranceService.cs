using Insurance.Core.Models;
using System.Collections.Generic;

namespace Insurance.WebAPI.Services
{
    public interface IInsuranceService
    {
        IEnumerable<InsuranceModel> GetAll();
        InsuranceModel GetById(int id);
        InsuranceModel Add(InsuranceModel insurance); 
        InsuranceModel Update(InsuranceModel insurance);         
        void Delete(int insuranceId);
    }
}
