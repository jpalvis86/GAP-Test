using Insurance.WebAPI.Models;
using System.Collections.Generic;

namespace Insurance.WebAPI.Services
{
    public interface IInsuranceService
    {
        IEnumerable<InsuranceModel> GetAll();
    }
}
