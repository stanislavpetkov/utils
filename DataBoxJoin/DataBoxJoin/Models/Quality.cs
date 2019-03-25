using System;
using System.Collections.Generic;

namespace DataBoxJoin.Models
{
    public partial class Quality
    {
        public int Qualityid { get; set; }
        public string QualityName { get; set; }
        public int? Color { get; set; }
        public int? QOrder { get; set; }
    }
}
