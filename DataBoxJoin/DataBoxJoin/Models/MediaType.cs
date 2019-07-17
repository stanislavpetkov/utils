using System;
using System.Collections.Generic;

namespace DataBoxJoin.Models
{
    public partial class MediaType
    {
        public int MediaTypeid { get; set; }
        public string MediaName { get; set; }
        public int? Status { get; set; }
        public int? PrepareTime { get; set; }
        public int? Color { get; set; }
    }
}
