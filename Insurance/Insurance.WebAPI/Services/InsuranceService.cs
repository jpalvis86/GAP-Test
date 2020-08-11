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
            ValidateInsuranceCoverageRate(insurance.CoverageRate);
            ValidateInsuranceMonthsPeriod(insurance.MonthsOfCoverage);


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

        /// <summary>
        /// Throws an exception if the insurance coverage rate is below 1% or higher than 100%
        /// </summary>
        /// <param name="insuranceCoverageRate"></param>
        private static void ValidateInsuranceCoverageRate(double insuranceCoverageRate)
        {
            if (insuranceCoverageRate < 0.01 || insuranceCoverageRate > 1)
                throw new InsuranceCoverageRateIsNotValidException(insuranceCoverageRate);
        }

        /// <summary>
        /// Throws an exception if the insurance months period rate is below 1
        /// </summary>
        /// <param name="insuranceMonthsPeriod"></param>
        private static void ValidateInsuranceMonthsPeriod(double insuranceMonthsPeriod)
        {
            if (insuranceMonthsPeriod < 1)
                throw new InsuranceMonthsPeriodIsNotValidException(insuranceMonthsPeriod);
        }

        #endregion

    }
}
