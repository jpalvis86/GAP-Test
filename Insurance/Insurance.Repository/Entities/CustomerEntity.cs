using System.Collections.Generic;

namespace Insurance.Repository.Entities
{
    public class CustomerEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<InsuranceEntity> Insurances { get; set; }
    }
}
