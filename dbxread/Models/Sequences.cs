using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

namespace dbxread.Models
{
    public class Sequences
    {
        public int SequenceId { get; set; }
        public string SequenceName { get; set; }
        public int? EpisodeCount { get; set; }
        public int? Color { get; set; }
        
        
        public static List<Sequences> ReadTable(ref FbConnection connection)
        {
            try
            {
                var myCommand = new FbCommand
                {
                    CommandText = "SELECT r.SEQUENCEID, r.SEQUENCE_NAME, r.EPISODE_COUNT, r.COLOR FROM SEQUENCES r",
                    Connection = connection
                };


                var reader = myCommand.ExecuteReader();
                var records = new List<Sequences>();
                if (!reader.HasRows) return records;

                while (reader.Read())
                {
                    var m = new Sequences()
                    {
                        SequenceId = reader.GetInt32(0),
                        SequenceName = reader.GetString(1),
                        EpisodeCount = reader.GetInt32N(2),
                        Color = reader.GetInt32N(3),
                        
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
