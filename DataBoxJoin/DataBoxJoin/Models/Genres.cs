using System;
using System.Collections.Generic;

namespace DataBoxJoin.Models
{
    public partial class Genres
    {
        public int Genreid { get; set; }
        public string GenreName { get; set; }
        public int? Typeid { get; set; }
        public int? Categoryid { get; set; }
        public int? Color { get; set; }
    }
}
