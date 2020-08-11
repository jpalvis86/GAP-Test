using Insurance.Core.Models;
using System;
using System.Collections.Generic;

namespace Insurance.UnitTests.Helpers
{
    internal static class InsuranceRecordsHelper
    {
        internal static IEnumerable<InsuranceModel> GetInsurances()
        {
            return new List<InsuranceModel>
            {
                new InsuranceModel
                {
                    Id = 1,
                    Name ="Test",
                    Description ="Test Insurance",
                    StartDate = DateTime.Today,
                    MonthsOfCoverage = 24,
                    CoverageRate = 0.5,
                    CoverageTypes = new List<CoverageType>{ CoverageType.Earthquake, CoverageType.Robbery },
                    Risk = Risk.Low,
                    Price = 999.99M,
                },
                new InsuranceModel
                {
                    Id = 2,
                    Name ="Test 2",
                    Description ="Test Insurance 2",
                    StartDate = DateTime.Today.AddMonths(1),
                    MonthsOfCoverage = 12,
                    CoverageRate = 0.25,
                    CoverageTypes = new List<CoverageType>{ CoverageType.Earthquake, CoverageType.Robbery, CoverageType.Fire, CoverageType.Damage },
                    Risk = Risk.High,
                    Price = 14999.99M,
                }
            };
        }
    }
}
