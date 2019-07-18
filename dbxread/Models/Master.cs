﻿using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

namespace dbxread.Models
{
    public class Master
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


        public static List<Master> ReadTable(FbConnection connection)
        {
            try
            {
                var myCommand = new FbCommand
                {
                    CommandText = "SELECT r.RECID, r.CREATOR, r.CLIPID, r.TITLE, r.SEASON, r.STATUS, r.SEQUENCEID, " +
                                  "r.GROUPID, r.TYPEID, r.CATEGORYID, r.DURATION, r.PRODDATE, r.RECEIPTDATE, " +
                                  "r.EPISODENO, r.REQ_LEFT, r.REQ_TOTAL, r.REQ_YEAR1, r.REQ_YEAR2, r.REQ_MONTH1, " +
                                  "r.REQ_MONTH2, r.REQ_WEEK1, r.REQ_WEEK2, r.REQ_DAY1, r.REQ_DAY2, r.REQ_HOUR1, " +
                                  "r.REQ_HOUR2, r.REQ_SLOT1, r.REQ_SLOT2, r.VALID_FROM, r.VALID_TO, r.PRIORITY, " +
                                  "r.RATING, r.PLOTOUTLINE, r.AGE_RATE, r.ENABLEMASK, r.LANGUAGEID FROM MASTER r",
                    Connection = connection
                };


                var reader = myCommand.ExecuteReader();
                
                var records = new List<Master>();
                
                if (!reader.HasRows) return records;

                while (reader.Read())
                {
                    var m = new Master
                    {
                        Recid = reader.GetInt32(0),
                        Creator = reader.GetString(1),
                        Clipid = reader.GetString(2),
                        Title = reader.GetString(3),
                        Season = reader.GetString(4),
                        Status = reader.GetInt32N(5),
                        Sequenceid = reader.GetInt32N(6),
                        Groupid = reader.GetInt32N(7),
                        Typeid = reader.GetInt32N(8),
                        Categoryid = reader.GetInt32N(9),
                        Duration = reader.GetInt32N(10),
                        Proddate = reader.GetDateTimeN(11),
                        Receiptdate = reader.GetDateTimeN(12),
                        Episodeno = reader.GetInt32N(13),
                        ReqLeft = reader.GetInt16N(14),
                        ReqTotal = reader.GetInt16N(15),
                        ReqYear1 = reader.GetInt16N(16),
                        ReqYear2 = reader.GetInt16N(17),
                        ReqMonth1 = reader.GetInt16N(18),
                        ReqMonth2 = reader.GetInt16N(19),
                        ReqWeek1 = reader.GetInt16N(20),
                        ReqWeek2 = reader.GetInt16N(21),
                        ReqDay1 = reader.GetInt16N(22),
                        ReqDay2 = reader.GetInt16N(23),
                        ReqHour1 = reader.GetInt16N(24),
                        ReqHour2 = reader.GetInt16N(25),
                        ReqSlot1 = reader.GetInt16N(26),
                        ReqSlot2 = reader.GetInt16N(27),
                        ValidFrom = reader.GetDouble(28),
                        ValidTo = reader.GetDouble(29),
                        Priority = reader.GetInt16N(30),
                        Rating = reader.GetDouble(31),
                        Plotoutline = reader.GetString(32),
                        AgeRate = reader.GetInt32(33),
                        Enablemask = reader.GetInt32(34),
                        Languageid = reader.GetInt32(35)
                    };
                    records.Add(m);
                }
                return records;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}