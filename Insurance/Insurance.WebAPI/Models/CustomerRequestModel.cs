using System.Collections.Generic;

namespace Insurance.WebAPI.Models
{
    public class CustomerRequestModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<int> InsuranceIds { get; set; }
    }
}
