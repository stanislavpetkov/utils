using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

namespace DbxRead.Models
{
    public class CustomPropsInv
    {
        /// Id from CustomProps
        public int ? PropId { get; set; }
        
        
        /// This is RecID from MASTER
        public int ? ItemId { get; set; }
        
        /// Always zero??
        public int ? ItemType { get; set; }
        
        /// <summary>
        /// The value it self as string
        /// </summary>
        public string PropValue { get; set; }
        
        
        public static List<CustomPropsInv> ReadTable(FbConnection connection)
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
                        PropValue = reader.GetString(3)
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