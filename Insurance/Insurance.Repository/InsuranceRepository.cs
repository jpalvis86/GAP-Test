﻿using Insurance.Core.Models;
using Insurance.Repository.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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

    public class InsuranceRepository : IInsuranceRepository
    {
        private readonly InsuranceDbContext _context;

        public InsuranceRepository(InsuranceDbContext context)
        {
            _context = context;
        }

        #region Public

        public InsuranceModel Add(InsuranceModel insurance)
        {
            var insuranceEntity = AddInsuranceRecord(insurance);

            SaveInsuranceCoverageRecords(insurance, insuranceEntity);

            insurance.Id = insuranceEntity.Id;
            return insurance;
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<InsuranceModel> Get()
        {
            IEnumerable<InsuranceModel> records = new List<InsuranceModel>();

            if (_context.Insurances.Any())
            {
                records = _context.Insurances.Select(i => new InsuranceModel
                {
                    Id = i.Id,
                    Name = i.Name,
                    Description = i.Description,
                    CoverageRate = i.CoverageRate,
                    StartDate = i.StartDate,
                    MonthsOfCoverage = i.MonthsOfCoverage,
                    Price = i.Price,
                    Risk = (Risk)i.RiskId,
                    CoverageTypes = i.InsurancesCoverages.Where(ic => ic.InsuranceId == i.Id)
                                                        .Select(ic => (CoverageType)ic.CoverageTypeId)
                });
            }

            return records;
        }

        public InsuranceModel GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public InsuranceModel Update(InsuranceModel insurance)
        {
            throw new System.NotImplementedException();
        }
        #endregion

        #region Private

        private InsuranceEntity AddInsuranceRecord(InsuranceModel insurance)
        {
            var insuranceEntity = new InsuranceEntity
            {
                Name = insurance.Name,
                Description = insurance.Description,
                CoverageRate = insurance.CoverageRate,
                RiskId = (int)insurance.Risk,
                Price = insurance.Price,
                StartDate = insurance.StartDate,
                MonthsOfCoverage = insurance.MonthsOfCoverage
            };

            _context.Insurances.Add(insuranceEntity);
            _context.SaveChanges();

            return insuranceEntity;
        }

        private void SaveInsuranceCoverageRecords(InsuranceModel insurance, InsuranceEntity insuranceEntity)
        {
            var insuranceCoverageRecords = insurance.CoverageTypes.Select(coverageType => new InsuranceCoverageBridgeEntity
            {
                InsuranceId = insuranceEntity.Id,
                CoverageTypeId = (int)coverageType
            });

            _context.InsurancesCoverages.AddRange(insuranceCoverageRecords);
            _context.SaveChanges();
        }

        #endregion

    }
}
