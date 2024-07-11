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
            modelBuilder.Entity<Candidate>().HasIndex(u => u.Email).IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
