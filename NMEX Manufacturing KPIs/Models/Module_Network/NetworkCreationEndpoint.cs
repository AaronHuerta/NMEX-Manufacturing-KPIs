using System.ComponentModel.DataAnnotations;

namespace NMEX_Manufacturing_KPIs.Models.Module_Network
{
    public class NetworkCreationEndpoint:LanSwitchIssues
    {
        public string Device { get; set; }

        public string Port { get; set; }

        public string Description_port { get; set; }

        public string Category_issue { get; set; }

        public string Description_issue { get; set; }

    }
}
