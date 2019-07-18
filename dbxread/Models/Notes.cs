using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

namespace dbxread.Models
{
    public class Notes
    {
        public int? TypeId { get; set; }
        public int? ItemId { get; set; }
        public string Text { get; set; }
        
        public static List<Notes> ReadTable(FbConnection connection)
        {
            try
            {
                var myCommand = new FbCommand
                {
                    CommandText = "SELECT r.TYPEID, r.ITEMID, r.TEXT FROM NOTES r",
                    Connection = connection
                };


                var reader = myCommand.ExecuteReader();
                var records = new List<Notes>();
                if (!reader.HasRows) return records;

                while (reader.Read())
                {
                    var m = new Notes()
                    {
                        TypeId = reader.GetInt32N(0),
                        ItemId = reader.GetInt32N(1),
                        Text = reader.GetString(2),
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