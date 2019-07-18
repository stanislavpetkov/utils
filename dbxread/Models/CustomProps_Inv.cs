using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

namespace dbxread.Models
{
    public class CustomPropsInv
    {
        public int ? PropId { get; set; }
        public int ? ItemId { get; set; }
        public int ? ItemType { get; set; }
        public string PropValue { get; set; }
        
        
        public static List<CustomPropsInv> ReadTable(ref FbConnection connection)
        {
            try
            {
                var myCommand = new FbCommand
                {
                    CommandText = "SELECT r.PROP_ID, r.ITEM_ID, r.ITEM_TYPE, r.PROP_VALUE FROM CUSTOM_PROPS_INV r",
                    Connection = connection
                };


                var reader = myCommand.ExecuteReader();
                var records = new List<CustomPropsInv>();
                if (!reader.HasRows) return records;

                while (reader.Read())
                {
                    var m = new CustomPropsInv()
                    {
                        PropId = reader.GetInt32N(0),
                        ItemId = reader.GetInt32N(1),
                        ItemType = reader.GetInt32N(2),
                        PropValue = reader.GetString(3),
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