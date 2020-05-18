using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PlanIt.Models
{
  public class PlanItContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet<ExecSummary> ExecSummaries { get; set; }
    public virtual DbSet<BusProposal> BusProposals { get; set; }
    public DbSet<ExecSummaryBusProposal> ExecSummaryBusPropsal { get; set; }

    public PlanItContext(DbContextOptions options) : base(options) { }
  }
}