using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

namespace dbxread.Models
{
    public class Genres
    {
        public int Genreid { get; set; }
        public string GenreName { get; set; }
        public int? Typeid { get; set; }
        public int? Categoryid { get; set; }
        public int? Color { get; set; }
        
        
        public static List<Genres> ReadTable(ref FbConnection connection)
        {
            try
            {
                var myCommand = new FbCommand
                {
                    CommandText = "SELECT r.GENREID, r.GENRE_NAME, r.TYPEID, r.CATEGORYID, r.COLOR FROM GENRES r",
                    Connection = connection
                };


                var reader = myCommand.ExecuteReader();
                var records = new List<Genres>();
                if (!reader.HasRows) return records;

                while (reader.Read())
                {
                    var m = new Genres()
                    {
                        Genreid = reader.GetInt32(0),
                        GenreName = reader.GetString(1),
                        Typeid = reader.GetInt32N(2),
                        Categoryid = reader.GetInt32N(3),
                        Color = reader.GetInt32N(4)
                        
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
