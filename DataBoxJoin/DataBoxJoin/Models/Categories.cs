using System;
using System.Collections.Generic;

namespace DataBoxJoin.Models
{
    public partial class Categories
    {
        public int Categoryid { get; set; }
        public string CategoryName { get; set; }
        public int? Typeid { get; set; }
        public int? Color { get; set; }
    }
}
