using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class CandidateRepository : Repository<Candidate>, ICandidateRepository
    {
        public CandidateRepository(JobCandidatesDBContext dbContext) : base(dbContext)
        {
        }
    }
}
