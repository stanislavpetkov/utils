using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

namespace dbxread.Models
{
    public class LanguagesInv
    {
        public int ? CountryId { get; set; }
        public int ? LanguageId { get; set; }
        
        
        public static List<LanguagesInv> ReadTable(ref FbConnection connection)
        {
            try
            {
                var myCommand = new FbCommand
                {
                    CommandText = "SELECT r.COUNTRYID, r.LANGUAGEID FROM LANGUAGES_INV r",
                    Connection = connection
                };


                var reader = myCommand.ExecuteReader();
                var records = new List<LanguagesInv>();
                if (!reader.HasRows) return records;

                while (reader.Read())
                {
                    var m = new LanguagesInv()
                    {
                        CountryId = reader.GetInt32(0),
                        LanguageId = reader.GetInt32(1)
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