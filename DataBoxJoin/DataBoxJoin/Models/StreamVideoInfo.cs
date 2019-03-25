using System;
using System.Collections.Generic;

namespace DataBoxJoin.Models
{
    public partial class StreamVideoInfo
    {
        public int StreamInfoid { get; set; }
        public string VCT { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public float? FrameRate { get; set; }
        public int? VideoBitRate { get; set; }
    }
}
