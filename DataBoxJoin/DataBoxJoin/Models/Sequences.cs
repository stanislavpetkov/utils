using System;
using System.Collections.Generic;

namespace DataBoxJoin.Models
{
    public partial class Sequences
    {
        public int Sequenceid { get; set; }
        public string SequenceName { get; set; }
        public int? EpisodeCount { get; set; }
        public int? Color { get; set; }
    }
}
