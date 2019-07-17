using System;
using System.Collections.Generic;

namespace DataBoxJoin.Models
{
    public partial class Users
    {
        public int Userid { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public int? UserGroupid { get; set; }
    }
}
