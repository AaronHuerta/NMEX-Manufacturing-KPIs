﻿using System.ComponentModel.DataAnnotations;

namespace NMEX_Manufacturing_KPIs.Models.Module_Network
{
    public class WLC
    {
        public int WLC_id { get; set; }
        public string Acces_Point { get; set; }

        public int Number_connected_devices_2_4 { get; set; }

        public int Number_connected_devices_5 { get; set; }

        public int Canal_2_4 { get; set; }

        public int Canal_5 { get; set; }

        public int Frecuency_2_4 { get; set; }

        public int Frecuency_5 { get; set; }

        public int Number_SSID_propagated { get; set; }

        public string WLC_data { get; set; }

        public int Plant_id { get; set; }

    }
}
