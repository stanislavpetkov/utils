using System;
using System.Collections.Generic;

namespace DataBoxJoin.Models
{
    public partial class StreamAudioInfo
    {
        public int StreamInfoid { get; set; }
        public string ACT { get; set; }
        public int? AudioSampleRate { get; set; }
        public int? AudioBitRate { get; set; }
        public int? AudioChannels { get; set; }
    }
}
