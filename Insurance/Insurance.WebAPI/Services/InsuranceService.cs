using Insurance.Core.Models;
using Insurance.Repository;
using System.Collections.Generic;

namespace Insurance.WebAPI.Services
{
    public class InsuranceService : IInsuranceService
    {
        private readonly IInsuranceRepository _insuranceRepository;

        public InsuranceService(IInsuranceRepository insuranceRepository)
        {
            _insuranceRepository = insuranceRepository;
        }

        public IEnumerable<InsuranceModel> GetAll()
        {
            return _insuranceRepository.Get();
        }

        public InsuranceModel GetById(int id)
        {
            return _insuranceRepository.GetById(id);
        }

        public InsuranceModel Add(InsuranceModel insurance)
        {
            return insurance;
        }

        public InsuranceModel Update(InsuranceModel insurance)
        {
            return insurance;
        }



        public void Delete(int insuranceId)
        {

        }


    }
}
