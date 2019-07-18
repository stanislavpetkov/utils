using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

namespace dbxread.Models
{
    public class CountriesInv
    {
        public int? Recid { get;  set;}
        public int? Countryid { get;  set;}
        public int? Activityid { get;  set;}
        
        
        public static List<CountriesInv> ReadTable(FbConnection connection)
        {
            try
            {
                var myCommand = new FbCommand
                {
                    CommandText = "SELECT r.RECID, r.COUNTRYID, r.ACTIVITYID FROM COUNTRIES_INV r",
                    Connection = connection
                };


                var reader = myCommand.ExecuteReader();
                var records = new List<CountriesInv>();
                if (!reader.HasRows) return records;

                while (reader.Read())
                {
                    var m = new CountriesInv()
                    {
                        Recid = reader.GetInt32N(0),
                        Countryid = reader.GetInt32N(1),
                        Activityid = reader.GetInt32N(2)
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