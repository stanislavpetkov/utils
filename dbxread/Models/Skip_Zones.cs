using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

namespace DbxRead.Models
{
    public class SkipZones
    {
        public int ? StreamId { get; set; }
        public int ? P1 { get; set; }
        public int ? P2 { get; set; }
        public int ? P3 { get; set; }
        public int ? P4 { get; set; }
        public string Text { get; set; }
        
        
        public static List<SkipZones> ReadTable(FbConnection connection)
        {
            try
            {
                var myCommand = new FbCommand
                {
                    CommandText = "SELECT r.STREAMID, r.P1, r.P2, r.P3, r.P4, r.TEXT FROM SKIP_ZONES r",
                    Connection = connection
                };


                var reader = myCommand.ExecuteReader();
                var records = new List<SkipZones>();
                if (!reader.HasRows) return records;

                while (reader.Read())
                {
                    var m = new SkipZones
                    {
                        StreamId = reader.GetInt32N(0),
                        P1 = reader.GetInt32N(1),
                        P2 = reader.GetInt32N(2),
                        P3 = reader.GetInt32N(3),
                        P4 = reader.GetInt32N(4),
                        Text = reader.GetString(5)
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