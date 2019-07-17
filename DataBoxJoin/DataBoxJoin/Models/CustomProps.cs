using System;
using System.Collections.Generic;

namespace DataBoxJoin.Models
{
    public partial class CustomProps
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public int? PropType { get; set; }
        public int? Res { get; set; }
        public int? Color { get; set; }
    }
}
