using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Insurance.Repository.Entities
{
    public class InsuranceEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double CoverageRate { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public int MonthsOfCoverage { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int RiskId { get; set; }


        public IEnumerable<InsuranceCoverageBridgeEntity> InsurancesCoverages { get; set; }

        public int CustomerId { get; set; }
        public CustomerEntity Customer { get; set; }
    }
}
