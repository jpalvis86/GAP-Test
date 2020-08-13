﻿using Insurance.Core.Models;
using Insurance.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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
            var insurancesByCustomer = (from ci in _context.CustomerInsurances
                                        join c in _context.Customers
                                        on ci.CustomerId equals c.Id
                                        join i in _context.Insurances.Include(c => c.InsurancesCoverages)
                                        on ci.InsuranceId equals i.Id
                                        where c.Id == id
                                        select new
                                        {
                                            Customer = c,
                                            Insurance = i
                                        }).ToList();

            var customer = (from d in insurancesByCustomer
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
                            }).FirstOrDefault();

            return customer;
        }

        public void Update(CustomerModel customer)
        {
            var customerEntity = new CustomerEntity
            {
                Id = customer.Id,
                Name = customer.Name
            };

            _context.Customers.Update(customerEntity);
        }
    }
}
