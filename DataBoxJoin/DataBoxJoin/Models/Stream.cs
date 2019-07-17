using System;
using System.Collections.Generic;

namespace DataBoxJoin.Models
{
    public partial class Stream
    {
        public int Streamid { get; set; }
        public int? Instanceid { get; set; }
        public float? AudioLevel { get; set; }
        public string FileName { get; set; }
        public string StreamName { get; set; }
        public int? StreamType { get; set; }
        public int? VideoInfoid { get; set; }
        public int? AudioInfoid { get; set; }
        public int? InP { get; set; }
        public int? OutP { get; set; }
        public int? Languageid { get; set; }
        public string Main { get; set; }
        public int? Status { get; set; }
        public int? Part { get; set; }
        public int? FileSize { get; set; }
    }
}
