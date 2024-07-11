using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class JobCandidatesDBContext : DbContext
    {
        public JobCandidatesDBContext(DbContextOptions<JobCandidatesDBContext> options) : base(options)
        {
        }

        public DbSet<Candidate> Candidates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
