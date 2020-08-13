using Insurance.Core.Models;
using Insurance.Repository.Entities;
using Microsoft.EntityFrameworkCore;
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

        IEnumerable<CustomerModel> GetCustomersByInsurance(int insuranceId);
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

        public IEnumerable<InsuranceModel> Get()
        {
            IEnumerable<InsuranceModel> records = new List<InsuranceModel>();

            if (_context.Insurances.Any())
            {
                records = _context.Insurances.Include(i => i.InsurancesCoverages)
                        .Select(i => new InsuranceModel
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
            var insuranceEntity = _context.Insurances.Include(i => i.InsurancesCoverages).SingleOrDefault(i => i.Id == id);

            if (insuranceEntity is null)
                return null;

            return new InsuranceModel
            {
                Id = insuranceEntity.Id,
                Name = insuranceEntity.Name,
                Description = insuranceEntity.Description,
                CoverageRate = insuranceEntity.CoverageRate,
                StartDate = insuranceEntity.StartDate,
                MonthsOfCoverage = insuranceEntity.MonthsOfCoverage,
                Price = insuranceEntity.Price,
                Risk = (Risk)insuranceEntity.RiskId,
                CoverageTypes = insuranceEntity.InsurancesCoverages.Where(ic => ic.InsuranceId == insuranceEntity.Id)
                                                        .Select(ic => (CoverageType)ic.CoverageTypeId)
            };
        }

        public InsuranceModel Update(InsuranceModel insurance)
        {
            var insuranceEntity = GetEntityFromId(insurance.Id);

            if (insuranceEntity is null)
                return null;
            
            insuranceEntity.Name = insurance.Name;
            insuranceEntity.Description = insurance.Name;
            insuranceEntity.CoverageRate = insurance.CoverageRate;
            insuranceEntity.Price = insurance.Price;
            insuranceEntity.StartDate = insurance.StartDate;
            insuranceEntity.MonthsOfCoverage = insurance.MonthsOfCoverage;
            insuranceEntity.RiskId = (int)insurance.Risk;

            var insuranceCoverageRecords = insurance.CoverageTypes.Select(coverageType => new InsuranceCoverageBridgeEntity
            {
                InsuranceId = insuranceEntity.Id,
                CoverageTypeId = (int)coverageType
            });

            _context.InsurancesCoverages.RemoveRange(insuranceEntity.InsurancesCoverages);
            _context.Insurances.Update(insuranceEntity);
            _context.InsurancesCoverages.AddRange(insuranceCoverageRecords);
            
            _context.SaveChanges();

            return insurance;
        }

        public void Delete(int id)
        {
            var insuranceEntity = GetEntityFromId(id);

            if (insuranceEntity is null)
                return;

            _context.InsurancesCoverages.RemoveRange(insuranceEntity.InsurancesCoverages);
            _context.Insurances.Remove(insuranceEntity);

            _context.SaveChanges();
        }

        public IEnumerable<CustomerModel> GetCustomersByInsurance(int insuranceId)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Private

        private InsuranceEntity GetEntityFromId(int id)
        {
            return _context.Insurances.Include(i => i.InsurancesCoverages).SingleOrDefault(i => i.Id == id);
        }

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
