using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

namespace DbxRead.Models
{
    public class KeywordsInv
    {
        public int? RecId { get; set; }
        public int? KeywordId { get; set; }
        
        public static List<KeywordsInv> ReadTable(FbConnection connection)
        {
            try
            {
                var myCommand = new FbCommand
                {
                    CommandText = "SELECT r.RECID, r.KEYWORDID FROM KEYWORDS_INV r",
                    Connection = connection
                };


                var reader = myCommand.ExecuteReader();
                var records = new List<KeywordsInv>();
                if (!reader.HasRows) return records;

                while (reader.Read())
                {
                    var m = new KeywordsInv()
                    {
                        RecId = reader.GetInt32N(0),
                        KeywordId = reader.GetInt32N(1)
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