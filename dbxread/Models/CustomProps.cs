using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

namespace DbxRead.Models
{
    public class CustomProps
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public int? PropType { get; set; }
        public int? Res { get; set; }
        public int? Color { get; set; }
        
        
        
        
        public static List<CustomProps> ReadTable(FbConnection connection)
        {
            try
            {
                var myCommand = new FbCommand
                {
                    CommandText = "SELECT r.ID, r.PARENT_ID, r.NAME, r.PROP_TYPE, r.RES, r.COLOR FROM CUSTOM_PROPS r",
                    Connection = connection
                };


                var reader = myCommand.ExecuteReader();
                var records = new List<CustomProps>();
                if (!reader.HasRows) return records;

                while (reader.Read())
                {
                    var m = new CustomProps()
                    {
                        Id = reader.GetInt32(0),
                        ParentId = reader.GetInt32N(1),
                        Name = reader.GetString(2),
                        PropType = reader.GetInt32N(3),
                        Res = reader.GetInt32N(4),
                        Color = reader.GetInt32N(5)
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
