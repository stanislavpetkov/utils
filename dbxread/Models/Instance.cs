using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

namespace dbxread.Models
{
    public class Instance
    {
        public int Instanceid { get; set; }
        public string InstanceName { get; set; }
        public string Main { get; set; }
        public int? Qualityid { get; set; }
        public int? Recid { get; set; }
        public int? Start { get; set; }
        public int? Duration { get; set; }
        public int? Status { get; set; }
        public DateTime? KillDate { get; set; }


        public static List<Instance> ReadTable(ref FbConnection connection)
        {
            try
            {
                var myCommand = new FbCommand
                {
                    CommandText =
                        "SELECT r.INSTANCEID, r.INSTANCE_NAME, r.MAIN, r.QUALITYID, r.RECID, r.\"START\", r.DURATION, r.STATUS, r.KILL_DATE FROM INSTANCE r",
                    Connection = connection
                };


                var reader = myCommand.ExecuteReader();

                var records = new List<Instance>();
                if (!reader.HasRows) return records;

                while (reader.Read())
                {
                    var m = new Instance()
                    {
                        Instanceid = reader.GetInt32(0),
                        InstanceName = reader.GetString(1),
                        Main = reader.GetString(2),
                        Qualityid = reader.GetInt32N(3),
                        Recid = reader.GetInt32N(4),
                        Start = reader.GetInt32N(5),
                        Duration = reader.GetInt32N(6),
                        Status = reader.GetInt32N(7),
                        KillDate = reader.GetDateTimeN(8)
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