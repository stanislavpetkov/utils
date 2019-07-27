using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

namespace DbxRead.Models
{
    public class Countries
    {
        public int Countryid { get; set; }
        public string CountryName { get; set; }
        public int? Color { get; set; }
        public int? Flagid { get; set; }
        public int? DialingCode { get; set; }
        public string Ab2 { get; set; }
        public string Ab3 { get; set; }
        
        public static List<Countries> ReadTable(FbConnection connection)
        {
            try
            {
                var myCommand = new FbCommand
                {
                    CommandText = "SELECT r.COUNTRYID, r.COUNTRY_NAME, r.COLOR, r.FLAGID, r.DIALING_CODE, r.AB2, r.AB3 FROM COUNTRIES r",
                    Connection = connection
                };


                var reader = myCommand.ExecuteReader();
                var records = new List<Countries>();
                if (!reader.HasRows) return records;

                while (reader.Read())
                {
                    var m = new Countries()
                    {
                        
                        Countryid = reader.GetInt32(0),
                        CountryName = reader.GetString(1),
                        Color = reader.GetInt32N(2),
                        Flagid = reader.GetInt32N(3),
                        DialingCode = reader.GetInt32N(4),
                        Ab2 = reader.GetString(5),
                        Ab3 = reader.GetString(6)
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
