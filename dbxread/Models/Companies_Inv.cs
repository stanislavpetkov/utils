using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

namespace DbxRead.Models
{
    public class CompaniesInv
    {
        public int ? Recid { get; set; }
        public int ? Companyid { get; set; }
        public int ? Activityid { get; set; }
        
        public static List<CompaniesInv> ReadTable(FbConnection connection)
        {
            try
            {
                var myCommand = new FbCommand
                {
                    CommandText = "SELECT r.RECID, r.COMPANYID, r.ACTIVITYID FROM COMPANIES_INV r",
                    Connection = connection
                };


                var reader = myCommand.ExecuteReader();
                var records = new List<CompaniesInv>();
                if (!reader.HasRows) return records;

                while (reader.Read())
                {
                    var m = new CompaniesInv()
                    {
                        Recid = reader.GetInt32N(0),
                        Companyid = reader.GetInt32N(1),
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