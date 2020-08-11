using Insurance.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Insurance.Repository
{
    public interface IInsuranceRepository
    {
        IEnumerable<InsuranceModel> Get();
        InsuranceModel GetById(int id);
        void Delete(int id);
        InsuranceModel Add(InsuranceModel insurance);
        InsuranceModel Update(InsuranceModel insurance);
    }

    public class InsuranceRepository : IInsuranceRepository
    {
        private readonly InsuranceDbContext _context;

        public InsuranceRepository(InsuranceDbContext context)
        {
            _context = context;
        }

        public InsuranceModel Add(InsuranceModel insurance)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<InsuranceModel> Get()
        {
            // TODO: Retrieve records
            return new List<InsuranceModel>();
        }

        public InsuranceModel GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public InsuranceModel Update(InsuranceModel insurance)
        {
            throw new System.NotImplementedException();
        }
    }
}
