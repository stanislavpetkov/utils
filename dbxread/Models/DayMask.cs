using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

namespace dbxread.Models
{
    public class DayMask
    {
        public int ? RecId { get; set; }
        public int ? Inp { get; set; }
        public int ? Outp { get; set; }

        public static List<DayMask> ReadTable(ref FbConnection connection)
        {
            try
            {
                var myCommand = new FbCommand
                {
                    CommandText = "SELECT r.RECID, r.INP, r.OUTP FROM DAY_MASK r",
                    Connection = connection
                };


                var reader = myCommand.ExecuteReader();
                var records = new List<DayMask>();
                if (!reader.HasRows) return records;

                while (reader.Read())
                {
                    var m = new DayMask()
                    {
                        RecId = reader.GetInt32N(0),
                        Inp = reader.GetInt32N(1),
                        Outp = reader.GetInt32N(2),
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