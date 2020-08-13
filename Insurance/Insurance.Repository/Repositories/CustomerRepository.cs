using Insurance.Core.Models;
using Insurance.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
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
                var insurancesByCustomer = (from ci in _context.CustomerInsurances
                                            join c in _context.Customers
                                            on ci.CustomerId equals c.Id
                                            join i in _context.Insurances.Include(c => c.InsurancesCoverages)
                                            on ci.InsuranceId equals i.Id
                                            select new
                                            {
                                                Customer = c,
                                                Insurance = i
                                            }).ToList();

                customers = (from d in insurancesByCustomer
                             group d by d.Customer into g
                             select new CustomerModel
                             {
                                 Id = g.Key.Id,
                                 Name = g.Key.Name,
                                 Insurances = g.Select(d => new InsuranceModel
                                 {
                                     Id = d.Insurance.Id,
                                     Name = d.Insurance.Name,
                                     Description = d.Insurance.Description,
                                     CoverageRate = d.Insurance.CoverageRate,
                                     StartDate = d.Insurance.StartDate,
                                     MonthsOfCoverage = d.Insurance.MonthsOfCoverage,
                                     Price = d.Insurance.Price,
                                     Risk = (Risk)d.Insurance.RiskId,
                                     CoverageTypes = d.Insurance.InsurancesCoverages.Where(ic => ic.InsuranceId == d.Insurance.Id)
                                                  .Select(ic => (CoverageType)ic.CoverageTypeId)
                                 })
                             }).ToList();
            }

            return customers;
        }

        public CustomerModel GetById(int id)
        {
            var customerEntity = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerEntity is null)
                return null;

            var insurancesByCustomer = from ci in _context.CustomerInsurances
                                       where ci.CustomerId == customerEntity.Id
                                       join i in _context.Insurances.Include(i => i.InsurancesCoverages)
                                       on ci.InsuranceId equals i.Id
                                       select i;

            var customer = new CustomerModel
            {
                Id = customerEntity.Id,
                Name = customerEntity.Name,
                Insurances = insurancesByCustomer.Select(i => new InsuranceModel
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
            };

            return customer;
        }

        public void Update(CustomerModel customer)
        {
            var existingInsurancesByCustomer = _context.CustomerInsurances.Where(ci => ci.CustomerId == customer.Id);
            _context.CustomerInsurances.RemoveRange(existingInsurancesByCustomer);

            var newInsurancesByCustomer = customer.Insurances.Select(i => new CustomerInsurancesEntity
            {
                CustomerId = customer.Id,
                InsuranceId = i.Id
            });

            _context.CustomerInsurances.AddRange(newInsurancesByCustomer);

            _context.SaveChanges();
        }
    }
}
