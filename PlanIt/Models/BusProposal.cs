using System.Collections.Generic;

namespace PlanIt.Models
{
  public class BusProposal
  {
    public BusProposal()
    {
        this.ExecSummaries = new HashSet<BusProposalExecSummary>();
    }

    public int BusProposalId { get; set; }
    public string BusProposalTitle { get; set; }
    public virtual ICollection<BusProposalExecSummary> ExecSummaries { get; set; }
  }
}