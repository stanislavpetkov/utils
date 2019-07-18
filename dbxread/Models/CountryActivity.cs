using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

namespace dbxread.Models
{
    public class CountryActivity
    {
        public int Activityid { get; set; }
        public string ActivityName { get; set; }
        public int? Color { get; set; }
        
        
        public static List<CountryActivity> ReadTable(FbConnection connection)
        {
            try
            {
                var myCommand = new FbCommand
                {
                    CommandText = "SELECT r.ACTIVITYID, r.ACTIVITY_NAME, r.COLOR FROM COUNTRY_ACTIVITY r",
                    Connection = connection
                };


                var reader = myCommand.ExecuteReader();
                var records = new List<CountryActivity>();
                if (!reader.HasRows) return records;

                while (reader.Read())
                {
                    var m = new CountryActivity()
                    {
                        Activityid = reader.GetInt32(0),
                        ActivityName = reader.GetString(1),
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
