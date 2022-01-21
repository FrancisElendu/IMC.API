using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMC.API.Models
{
    public class Rate
    {
        public string city { get; set; }
        public string city_rate { get; set; }
        public string combined_district_rate { get; set; }
        public string combined_rate { get; set; }
        public string country { get; set; }
        public string country_rate { get; set; }
        public string county { get; set; }
        public string county_rate { get; set; }
        public bool freight_taxable { get; set; }
        public string state { get; set; }
        public string state_rate { get; set; }
        public string zip { get; set; }
    }
    public class TaxRate
    {
        public Rate rate { get; set; }
    }
}
