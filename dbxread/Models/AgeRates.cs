using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

namespace dbxread.Models
{
    public class AgeRates
    {
        public int AgeRateId { get; set; }
        public string AgeRateName { get; set; }
        public int? Color { get; set; }

        public AgeRates()
        {
        }

        public AgeRates(AgeRates from)
        {
            AgeRateId = from.AgeRateId;
            AgeRateName = string.Copy(from.AgeRateName);
            Color = from.Color;
        }
        
        public static List<AgeRates> ReadTable(FbConnection connection)
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
                        AgeRateId = reader.GetInt32(0),
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
