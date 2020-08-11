namespace Insurance.Repository.Entities
{
    public class InsuranceCoverageBridgeEntity
    {
        public int InsuranceId { get; set; }
        public InsuranceEntity Insurance { get; set; }
        public int CoverageTypeId{ get; set; }
        public CoverageTypeEntity CoverageType { get; set; }
    }
}
