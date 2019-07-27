using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

namespace DbxRead.Models
{
    public class Persons
    {
        public int PersonId { get; set; }
        public string PersonName { get; set; }
        public int? Color { get; set; }
        
        public static List<Persons> ReadTable(FbConnection connection)
        {
            try
            {
                var myCommand = new FbCommand
                {
                    CommandText = "SELECT r.PERSONID, r.PERSON_NAME, r.COLOR FROM PERSONS r",
                    Connection = connection
                };


                var reader = myCommand.ExecuteReader();
                var records = new List<Persons>();
                if (!reader.HasRows) return records;

                while (reader.Read())
                {
                    var m = new Persons()
                    {
                        PersonId = reader.GetInt32(0),
                        PersonName = reader.GetString(1),
                        Color= reader.GetInt32N(2)
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
