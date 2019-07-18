using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

namespace dbxread.Models
{
    public class Types
    {
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public int? Color { get; set; }

        public Types()
        {}
        public Types(Types t)
        {
            TypeId = t.TypeId;
            TypeName = string.Copy(t.TypeName);
            Color =  t.Color;
        }
        
        public static List<Types> ReadTable(FbConnection connection)
        {
            try
            {
                var myCommand = new FbCommand
                {
                    CommandText = "SELECT r.TYPEID, r.TYPE_NAME, r.COLOR FROM TYPES r",
                    Connection = connection
                };


                var reader = myCommand.ExecuteReader();
                var records = new List<Types>();
                if (!reader.HasRows) return records;

                while (reader.Read())
                {
                    var m = new Types()
                    {
                        TypeId = reader.GetInt32(0),
                        TypeName = reader.GetString(1),
                        Color = reader.GetInt32N(2),
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
