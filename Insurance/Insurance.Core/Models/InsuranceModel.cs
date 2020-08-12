using System;
using System.Collections.Generic;

namespace Insurance.Core.Models
{
    public class InsuranceModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<CoverageType> CoverageTypes { get; set; }
        public double CoverageRate { get; set; }
        public DateTime StartDate { get; set; }
        public int MonthsOfCoverage { get; set; }
        public decimal Price { get; set; }
        public Risk Risk { get; set; }
    }

    public enum Risk
    {
        Low = 1,
        Medium = 2,
        MediumHigh = 3,
        High = 4
    }

    public enum CoverageType
    {
        Earthquake = 1,
        Fire = 2,
        Robbery = 3,
        Damage = 4,
        Lost = 5
    }
}
