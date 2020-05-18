using System.Collections.Generic;

namespace PlanIt.Models
{
  public class BusProposal
  {
    public BusProposal()
    {
        this.ExecSummaries = new HashSet<ExecSummaryBusProposal>();
    }

    public int BusProposalId { get; set; }
    public string BusProposalTitle { get; set; }
    public virtual ApplicationUser User { get; set; }

    public virtual ICollection<ExecSummaryBusProposal> ExecSummaries { get; set; }
  }
}