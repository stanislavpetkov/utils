using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

namespace dbxread.Models
{
    public class Positions
    {
        public int PositionId { get; set; }
        public string PositionName { get; set; }
        public int? Color { get; set; }
        
        public static List<Positions> ReadTable(ref FbConnection connection)
        {
            try
            {
                var myCommand = new FbCommand
                {
                    CommandText = "SELECT r.POSITIONID, r.POSITION_NAME, r.COLOR FROM POSITIONS r",
                    Connection = connection
                };


                var reader = myCommand.ExecuteReader();
                var records = new List<Positions>();
                if (!reader.HasRows) return records;

                while (reader.Read())
                {
                    var m = new Positions()
                    {
                        PositionId = reader.GetInt32(0),
                        PositionName= reader.GetString(1),
                        Color= reader.GetInt32N(2),
                        
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
