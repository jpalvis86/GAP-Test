using Insurance.Core.Models;
using Insurance.Repository.Entities;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Insurance.Repository
{
    public interface ICustomerRepository
    {
        IEnumerable<CustomerModel> Get();
        CustomerModel GetById(int id);
        void Update(CustomerModel customer);
    }

    public class CustomerRepository : ICustomerRepository
    {
        private readonly InsuranceDbContext _context;

        public CustomerRepository(InsuranceDbContext context)
        {
            _context = context;
        }

        public IEnumerable<CustomerModel> Get()
        {
            var customers = new List<CustomerModel>();

            if (_context.Customers.Any())
            {
                customers = _context.Customers.Select(c => new CustomerModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Insurances = c.Insurances.Select(i => new InsuranceModel
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
                    })
                }).ToList();
            }

            return customers;
        }

        public CustomerModel GetById(int id)
        {
            var customerRecord = _context.Customers.SingleOrDefault(c => c.Id == id);

            var insurances = new List<InsuranceModel>();

            if (customerRecord.Insurances != null && customerRecord.Insurances.Any())
            {
                insurances = customerRecord.Insurances.Select(i => new InsuranceModel
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
                }).ToList();
            }
            return new CustomerModel
            {
                Id = customerRecord.Id,
                Name = customerRecord.Name,
                Insurances = insurances
            };
        }

        public void Update(CustomerModel customer)
        {
            var customerEntity = new CustomerEntity
            {
                Id = customer.Id,
                Name = customer.Name,
                Insurances = customer.Insurances.Select(i => new InsuranceEntity
                {
                    Id = i.Id,
                    Name = i.Name,
                    Description = i.Description,
                    CoverageRate = i.CoverageRate,
                    StartDate = i.StartDate,
                    MonthsOfCoverage = i.MonthsOfCoverage,
                    Price = i.Price,
                    RiskId = (int)i.Risk,
                    InsurancesCoverages = i.CoverageTypes.Select(coverageType => new InsuranceCoverageBridgeEntity
                    {
                        InsuranceId = i.Id,
                        CoverageTypeId = (int)coverageType
                    })
                })
            };

            _context.Customers.Update(customerEntity);
        }
    }
}
