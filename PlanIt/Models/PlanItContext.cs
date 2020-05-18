using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PlanIt.Models
{
  public class PlanItContext : IdentityDbContext<ApplicationUser>
  {
    public virtual DbSet<ExecSummary> ExecSummaries { get; set; }
    public DbSet<BusProposal> BusProposals { get; set; }
    public DbSet<ExecSummaryBusProposal> ExecSummaryBusProposal { get; set; }

    public PlanItContext(DbContextOptions options) : base(options) { }
  }
}