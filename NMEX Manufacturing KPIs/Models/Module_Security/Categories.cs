﻿using System.ComponentModel.DataAnnotations;

namespace NMEX_Manufacturing_KPIs.Models.Module_Security
{
    public class Categories
    {
        // Propiedades de la tabla Security
        public int Category_id { get; set; }
        public string Category_name { get; set; }
        public string Category_description { get; set; }
        public int Function_id { get; set; }
        public bool Active { get; set; }
        public string Category_identifier { get; set; }

        // Propiedades de la tabla Function
        public string Function_name{ get; set; }

        public string Result { get; set; }

        public string ResultFunction { get; set; }




    }
}
