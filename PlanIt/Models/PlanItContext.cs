using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PlanIt.Models
{
  public class PlanItContext : IdentityDbContext<ApplicationUser>
  {
    public virtual DbSet<BusExecSummary> BusExecSummaries { get; set; }

    public PlanItContext(DbContextOptions options) : base(options) { }
  }
}