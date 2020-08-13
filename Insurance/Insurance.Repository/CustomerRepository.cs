﻿using Insurance.Core.Models;
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
            throw new System.NotImplementedException();
        }

        public void Update(CustomerModel customer)
        {
            throw new System.NotImplementedException();
        }
    }
}
