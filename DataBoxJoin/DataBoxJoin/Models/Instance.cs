using System;
using System.Collections.Generic;

namespace DataBoxJoin.Models
{
    public partial class Instance
    {
        public int Instanceid { get; set; }
        public string InstanceName { get; set; }
        public string Main { get; set; }
        public int? Qualityid { get; set; }
        public int? Recid { get; set; }
        public int? Start { get; set; }
        public int? Duration { get; set; }
        public int? Status { get; set; }
        public DateTime? KillDate { get; set; }
    }
}
