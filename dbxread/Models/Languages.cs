using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

namespace DbxRead.Models
{
    public class Languages
    {
        public int LanguageId { get; set; }
        public string LanguageName { get; set; }
        public int? Color { get; set; }
        
        public static List<Languages> ReadTable(FbConnection connection)
        {
            try
            {
                var myCommand = new FbCommand
                {
                    CommandText = "SELECT r.LANGUAGEID, r.LANGUAGE_NAME, r.COLOR FROM LANGUAGES r",
                    Connection = connection
                };


                var reader = myCommand.ExecuteReader();
                var records = new List<Languages>();
                if (!reader.HasRows) return records;

                while (reader.Read())
                {
                    var m = new Languages()
                    {
                        LanguageId = reader.GetInt32(0),
                        LanguageName = reader.GetString(1),
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
