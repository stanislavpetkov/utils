using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

namespace dbxread.Models
{
    public class GenresInv
    {
        public int? GenreId { get; set; }
        public int? RecId { get; set; }

        public static List<GenresInv> ReadTable(FbConnection connection)
        {
            try
            {
                var myCommand = new FbCommand
                {
                    CommandText = "SELECT r.GENREID, r.RECID FROM GENRES_INV r",
                    Connection = connection
                };


                var reader = myCommand.ExecuteReader();
                var records = new List<GenresInv>();
                if (!reader.HasRows) return records;

                while (reader.Read())
                {
                    var m = new GenresInv()
                    {
                        GenreId = reader.GetInt32N(0),
                        RecId = reader.GetInt32N(1),
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