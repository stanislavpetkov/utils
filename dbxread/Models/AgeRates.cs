using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

namespace dbxread.Models
{
    public class AgeRates
    {
        public int AgeRateid { get; set; }
        public string AgeRateName { get; set; }
        public int? Color { get; set; }
        
        public static List<AgeRates> ReadTable(ref FbConnection connection)
        {
            try
            {
                var myCommand = new FbCommand
                {
                    CommandText = "SELECT r.AGE_RATEID, r.AGE_RATE_NAME, r.COLOR FROM AGE_RATES r",
                    Connection = connection
                };


                var reader = myCommand.ExecuteReader();
                var records = new List<AgeRates>();
                if (!reader.HasRows) return records;

                while (reader.Read())
                {
                    var m = new AgeRates()
                    {
                        AgeRateid = reader.GetInt32(0),
                        AgeRateName = reader.GetString(1),
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
