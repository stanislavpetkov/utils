using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

namespace dbxread.Models
{
    public class PersonsInv
    {
        public int ? RecId { get; set; }
        public int ? PersonId { get; set; }
        public int ? PositionId { get; set; }

        
        public static List<PersonsInv> ReadTable(FbConnection connection)
        {
            try
            {
                var myCommand = new FbCommand
                {
                    CommandText = "SELECT r.RECID, r.PERSONID, r.POSITIONID FROM PERSONS_INV r",
                    Connection = connection
                };


                var reader = myCommand.ExecuteReader();
                var records = new List<PersonsInv>();
                if (!reader.HasRows) return records;

                while (reader.Read())
                {
                    var m = new PersonsInv()
                    {
                        RecId = reader.GetInt32N(0),
                        PersonId = reader.GetInt32N(1),
                        PositionId = reader.GetInt32N(2)
                        
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