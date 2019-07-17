using System;
using System.Collections.Generic;

namespace DataBoxJoin.Models
{
    public partial class UserGroups
    {
        public int UserGroupid { get; set; }
        public string UserGroupName { get; set; }
        public int? Rights { get; set; }
    }
}
