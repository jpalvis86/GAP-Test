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
        Low,
        Medium,
        MediumHigh,
        High
    }

    public enum CoverageType
    {
        Earthquake,
        Fire,
        Robbery,
        Damage,
        Lost
    }
}
