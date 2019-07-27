using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

namespace DbxRead.Models
{
    public class Keywords
    {
        public int KeywordId { get; set; }
        public string KeywordName { get; set; }
        public int? Color { get; set; }

        public static List<Keywords> ReadTable(FbConnection connection)
        {
            try
            {
                var myCommand = new FbCommand
                {
                    CommandText = "SELECT r.KEYWORDID, r.KEYWORD_NAME, r.COLOR FROM KEYWORDS r",
                    Connection = connection
                };


                var reader = myCommand.ExecuteReader();
                var records = new List<Keywords>();
                if (!reader.HasRows) return records;

                while (reader.Read())
                {
                    var m = new Keywords()
                    {
                        KeywordId = reader.GetInt32(0),
                        KeywordName = reader.GetString(1),
                        Color = reader.GetInt32N(2)
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