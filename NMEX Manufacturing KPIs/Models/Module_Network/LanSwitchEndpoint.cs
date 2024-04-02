using System.ComponentModel.DataAnnotations;

namespace NMEX_Manufacturing_KPIs.Models.Module_Network
{
    public class LanSwitchEndpoint
    {
        public int Endpoint_id { get; set; }

        public string Device { get; set; }

        public string Port { get; set; }

        public string Description_port { get; set; }

        public string Category_issue { get; set; }

        public string Description_issue { get; set; }

        public string LanSwitchEnpoints_data { get; set; }

        public int Plant_id { get; set; }
    }
}
