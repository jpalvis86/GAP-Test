using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Insurance.Repository.Entities
{
    public class CoverageTypeEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public IEnumerable<InsuranceCoverageBridgeEntity> InsurancesCoverages { get; set; }
    }
}
