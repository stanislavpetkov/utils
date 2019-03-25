using System;
using System.Collections.Generic;

namespace DataBoxJoin.Models
{
    public partial class Master
    {
        public int Recid { get; set; }
        public string Creator { get; set; }
        public string Clipid { get; set; }
        public string Title { get; set; }
        public string Season { get; set; }
        public int? Status { get; set; }
        public int? Sequenceid { get; set; }
        public int? Groupid { get; set; }
        public int? Typeid { get; set; }
        public int? Categoryid { get; set; }
        public int? Duration { get; set; }
        public DateTime? Proddate { get; set; }
        public DateTime? Receiptdate { get; set; }
        public int? Episodeno { get; set; }
        public short? ReqLeft { get; set; }
        public short? ReqTotal { get; set; }
        public short? ReqYear1 { get; set; }
        public short? ReqYear2 { get; set; }
        public short? ReqMonth1 { get; set; }
        public short? ReqMonth2 { get; set; }
        public short? ReqWeek1 { get; set; }
        public short? ReqWeek2 { get; set; }
        public short? ReqDay1 { get; set; }
        public short? ReqDay2 { get; set; }
        public short? ReqHour1 { get; set; }
        public short? ReqHour2 { get; set; }
        public short? ReqSlot1 { get; set; }
        public short? ReqSlot2 { get; set; }
        public double? ValidFrom { get; set; }
        public double? ValidTo { get; set; }
        public short? Priority { get; set; }
        public double? Rating { get; set; }
        public string Plotoutline { get; set; }
        public int? AgeRate { get; set; }
        public int? Enablemask { get; set; }
        public int? Languageid { get; set; }
    }
}
