using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

namespace dbxread.Models
{
    public class Companies
    {
        public int Companyid { get; set; }
        public string CompanyName { get; set; }
        public int? Color { get; set; }
        
        
        
        public static List<Companies> ReadTable(ref FbConnection connection)
        {
            try
            {
                var myCommand = new FbCommand
                {
                    CommandText = "SELECT r.COMPANYID, r.COMPANY_NAME, r.COLOR FROM COMPANIES r",
                    Connection = connection
                };


                var reader = myCommand.ExecuteReader();
                var records = new List<Companies>();
                if (!reader.HasRows) return records;

                while (reader.Read())
                {
                    var m = new Companies()
                    {
                        Companyid = reader.GetInt32(0),
                        CompanyName = reader.GetString(1),
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
