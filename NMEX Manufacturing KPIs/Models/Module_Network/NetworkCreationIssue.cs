using System.ComponentModel.DataAnnotations;

namespace NMEX_Manufacturing_KPIs.Models.Module_Network
{
    public class NetworkCreationIssue:LanSwitchIssues
    {
        public string Device { get; set; }

        public string Type_affectation { get; set; }

        public string Description_affectation { get; set; }

        public string Suggestion { get; set; }

        public string Criticality_level { get; set; }
    }
}
