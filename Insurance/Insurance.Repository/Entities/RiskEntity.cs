using System.ComponentModel.DataAnnotations;

namespace Insurance.Repository.Entities
{
    public class RiskEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
