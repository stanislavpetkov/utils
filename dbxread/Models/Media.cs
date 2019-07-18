using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

namespace dbxread.Models
{
    public class Media
    {
        public int? StreamId { get; set; }
        public string MediaName { get; set; }
        public int? MediaTypeid { get; set; }
        public int? ArchiveId { get; set; }
        public int? MediaPool { get; set; }
        public int MediaId { get; set; }
        public int? InP { get; set; }
        public int? OutP { get; set; }
        public int? Status { get; set; }
        public string Pool { get; set; }
        
        public static List<Media> ReadTable(ref FbConnection connection)
        {
            try
            {
                var myCommand = new FbCommand
                {
                    CommandText = "SELECT r.STREAMID, r.MEDIA_NAME, r.MEDIA_TYPEID, r.ARCHIVEID, r.MEDIA_POOL, r.MEDIAID, r.IN_P, r.OUT_P, r.STATUS, r.POOL FROM MEDIA r",
                    Connection = connection
                };


                var reader = myCommand.ExecuteReader();
                var records = new List<Media>();
                if (!reader.HasRows) return records;

                while (reader.Read())
                {
                    var m = new Media()
                    {
                        StreamId= reader.GetInt32N(0),
                        MediaName= reader.GetString(1),
                        MediaTypeid= reader.GetInt32N(2),
                        ArchiveId= reader.GetInt32N(3),
                        MediaPool= reader.GetInt32N(4),
                        MediaId= reader.GetInt32(5),
                        InP= reader.GetInt32N(6),
                        OutP= reader.GetInt32N(7),
                        Status= reader.GetInt32N(8),
                        Pool = reader.GetString(9)
                        
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
