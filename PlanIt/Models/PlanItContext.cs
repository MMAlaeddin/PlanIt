using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PlanIt.Models
{
  public class PlanItContext : IdentityDbContext<ApplicationUser>
  {
    public virtual DbSet<BusProposal> BusProposals { get; set; }
    public DbSet<ExecSummary> ExecSummaries { get; set; }
    public DbSet<BusProposalExecSummary> BusProposalExecSummary { get; set; }

    public PlanItContext(DbContextOptions options) : base(options) { }
  }
