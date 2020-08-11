using Insurance.Core.Exceptions;
using Insurance.Core.Models;
using Insurance.Repository;
using System;
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

        #region Public

        public IEnumerable<InsuranceModel> GetAll()
        {
            return _insuranceRepository.Get();
        }

        public InsuranceModel GetById(int id)
        {
            if (id <= 0)
                throw new InsuranceIdIsNotValidException(id);

            return _insuranceRepository.GetById(id);
        }

        public InsuranceModel Add(InsuranceModel insurance)
        {
            // TODO: Validate insurance data

            ValidateInsuranceStartDate(insurance.StartDate);

            return _insuranceRepository.Add(insurance);
        }

        public InsuranceModel Update(InsuranceModel insurance)
        {
            return insurance;
        }

        public void Delete(int insuranceId)
        {
            _insuranceRepository.Delete(insuranceId);

        }
        #endregion

        #region Private     

        /// <summary>
        /// Throws an exception if the insurance start date is before today
        /// </summary>
        /// <param name="insuranceStartDate"></param>
        private static void ValidateInsuranceStartDate(DateTime insuranceStartDate)
        {
            if (insuranceStartDate < DateTime.Today)
                throw new InsuranceStartDateIsNotValidException(insuranceStartDate);
        }

        #endregion

    }
}
