using System;
using System.Collections.Generic;

namespace DataBoxJoin.Models
{
    public partial class Media
    {
        public int? Streamid { get; set; }
        public string MediaName { get; set; }
        public int? MediaTypeid { get; set; }
        public int? Archiveid { get; set; }
        public int? MediaPool { get; set; }
        public int Mediaid { get; set; }
        public int? InP { get; set; }
        public int? OutP { get; set; }
        public int? Status { get; set; }
        public string Pool { get; set; }
    }
}
