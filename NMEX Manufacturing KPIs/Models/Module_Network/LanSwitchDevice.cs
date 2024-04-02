using System.ComponentModel.DataAnnotations;

namespace NMEX_Manufacturing_KPIs.Models.Module_Network
{
    public class LanSwitchDevice
    {
        public int Switch_device_id { get; set; }

        public string Device { get; set; }

        public int CPU_processing { get; set; }

        public int Temperature_level { get; set;}

        public string FAN_status { get; set; }

        public int Bandwidth { get; set; }

        public string Power_source { get; set; }

        public string LanSwitchDevice_data { get; set; }

        public int Plant_id { get; set; }
    }
}
