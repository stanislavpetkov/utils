using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

namespace dbxread.Models
{
    public class EditItems
    {
        public string UserPc { get; set; }
        public int ? ItemType { get; set; }
        public int ? ItemId { get; set; }
        public DateTime ? EditTime { get; set; }

        public static List<EditItems> ReadTable(FbConnection connection)
        {
            try
            {
                var myCommand = new FbCommand
                {
                    CommandText = "SELECT r.USER_PC, r.ITEM_TYPE, r.ITEM_ID, r.EDIT_TIME FROM EDIT_ITEMS r",
                    Connection = connection
                };


                var reader = myCommand.ExecuteReader();
                var records = new List<EditItems>();
                if (!reader.HasRows) return records;

                while (reader.Read())
                {
                    var m = new EditItems()
                    {
                        UserPc = reader.GetString(0),
                        ItemType = reader.GetInt32N(1),
                        ItemId = reader.GetInt32N(2),
                        EditTime = reader.GetDateTimeN(3)
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