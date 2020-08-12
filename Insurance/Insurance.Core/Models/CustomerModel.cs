using System.Collections.Generic;

namespace Insurance.Core.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<InsuranceModel> Insurances { get; set; }

    }
}
