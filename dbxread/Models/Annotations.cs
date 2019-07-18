using System;
using System.Collections.Generic;
using System.IO;
using FirebirdSql.Data.FirebirdClient;

namespace dbxread.Models
{
    public class Annotations
    {
        public int? StreamId { get; set; }
        public int? P1 { get; set; }
        public int? P2 { get; set; }
        public string Text { get; set; }
        public System.IO.MemoryStream Picture { get; set; }
        
        public static List<Annotations> ReadTable(ref FbConnection connection)
        {
            try
            {
                var myCommand = new FbCommand
                {
                    CommandText = "SELECT r.STREAMID, r.P1, r.P2, r.TEXT, r.PICTURE FROM ANNOTATIONS r",
                    Connection = connection
                };


                var reader = myCommand.ExecuteReader();
                var records = new List<Annotations>();
                if (!reader.HasRows) return records;

                while (reader.Read())
                {
                    var strm = reader.GetStream(4);
                    
                    var m = new Annotations()
                    {
                        StreamId = reader.GetInt32N(0),
                        P1 = reader.GetInt32N(1),
                        P2 = reader.GetInt32N(2),
                        Text = reader.GetString(3),
                        Picture = new MemoryStream()
                    };
                    strm.CopyTo(m.Picture);
                    
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