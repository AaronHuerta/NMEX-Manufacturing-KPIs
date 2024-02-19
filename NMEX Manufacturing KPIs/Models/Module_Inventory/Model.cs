﻿using System.ComponentModel.DataAnnotations;

namespace NMEX_Manufacturing_KPIs.Models.Module_Inventory
{
    public class Model
    {
        public int Model_id { get; set; }
        [StringLength(maximumLength: 50, ErrorMessage = "VALUE MUST NOT EXCEED 50 CHARACTERS")]
        public string Model_description { get; set; }
        public bool Active { get; set; }
    }
}
