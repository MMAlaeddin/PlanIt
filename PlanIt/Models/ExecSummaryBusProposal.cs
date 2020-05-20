namespace PlanIt.Models
{
public class ExecSummaryBusProposal
  {       
    public int ExecSummaryBusProposalId { get; set; }
    public int BusProposalId { get; set; }
    public int ExecSummaryId { get; set; }
    public BusProposal Busproposal { get; set; }
    public ExecSummary ExecSummary { get; set; }
  }
}