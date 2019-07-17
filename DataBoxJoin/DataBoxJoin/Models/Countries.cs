using System;
using System.Collections.Generic;

namespace DataBoxJoin.Models
{
    public partial class Countries
    {
        public int Countryid { get; set; }
        public string CountryName { get; set; }
        public int? Color { get; set; }
        public int? Flagid { get; set; }
        public int? DialingCode { get; set; }
        public string Ab2 { get; set; }
        public string Ab3 { get; set; }
    }
}
