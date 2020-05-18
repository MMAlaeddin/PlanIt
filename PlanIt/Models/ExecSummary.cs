using System.Collections.Generic;

namespace PlanIt.Models
{
    public class ExecSummary
    {
        public ExecSummary()
        {
            this.BusProposals = new HashSet<ExecSummaryBusProposal>();
        }

        public int ExecSummaryId { get; set; }
        public string ExecSummaryTitle { get; set; }
        public string Intro { get; set; }
        public string Problem { get; set; } 
        public string Finance { get; set; }
        // public virtual ApplicationUser User { get; set; }

        public ICollection<ExecSummaryBusProposal> BusProposals { get; set; }
    }
}