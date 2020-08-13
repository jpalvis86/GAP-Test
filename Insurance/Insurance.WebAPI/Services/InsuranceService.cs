using Insurance.Core.Exceptions;
using Insurance.Core.Models;
using Insurance.Repository;
using Microsoft.EntityFrameworkCore.Internal;
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
            ValidateInsuranceData(insurance);

            return _insuranceRepository.Add(insurance);
        }

        public InsuranceModel Update(InsuranceModel insurance)
        {
            ValidateInsuranceData(insurance);

            var updatedInsurance = _insuranceRepository.Update(insurance);
            if (updatedInsurance is null)
                throw new InsuranceDoesNotExistException(insurance.Id);

            return updatedInsurance;
        }

        public void Delete(int insuranceId)
        {
            var customersByInsurance = _insuranceRepository.GetCustomersByInsurance(insuranceId);

            if (customersByInsurance.Any())
                throw new InsuranceIsBeingUsedByCustomersException(insuranceId, customersByInsurance);

            _insuranceRepository.Delete(insuranceId);
        }

        #endregion

        #region Private     

        private static void ValidateInsuranceData(InsuranceModel insurance)
        {
            ValidateInsuranceStartDate(insurance.StartDate);
            ValidateInsuranceCoverageRate(insurance.CoverageRate);
            ValidateInsuranceCoverageRateForHighRisk(insurance.CoverageRate, insurance.Risk);
            ValidateInsuranceMonthsPeriod(insurance.MonthsOfCoverage);
            ValidateInsurancePrice(insurance.Price);
        }

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
        /// Throws an exception if the insurance coverage rate is higher than 50% for a High Risk profile
        /// </summary>
        /// <param name="insuranceCoverageRate"></param>
        /// <param name="riskProfile"></param>
        private static void ValidateInsuranceCoverageRateForHighRisk(double insuranceCoverageRate, Risk riskProfile)
        {
            if (riskProfile == Risk.High && insuranceCoverageRate > 0.5)
                throw new InsuranceCoverageRateForHighRiskProfileIsNotValidException(insuranceCoverageRate);
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

        /// <summary>
        /// Throws an exception if the insurance price is below 100
        /// </summary>
        /// <param name="insurancePrice"></param>
        private static void ValidateInsurancePrice(decimal insurancePrice)
        {
            if (insurancePrice < 100M)
                throw new InsurancePriceIsNotValidException(insurancePrice);
        }

        #endregion

    }
}
