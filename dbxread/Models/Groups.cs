using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

namespace dbxread.Models
{
    public class Groups
    {
        public int Groupid { get; set; }
        public string GroupName { get; set; }
        public int? Color { get; set; }
        
        public static List<Groups> ReadTable(ref FbConnection connection)
        {
            try
            {
                var myCommand = new FbCommand
                {
                    CommandText = "SELECT r.GROUPID, r.GROUP_NAME, r.COLOR FROM GROUPS r",
                    Connection = connection
                };


                var reader = myCommand.ExecuteReader();
                var records = new List<Groups>();
                if (!reader.HasRows) return records;

                while (reader.Read())
                {
                    var m = new Groups()
                    {
                        Groupid = reader.GetInt32(0),
                        GroupName = reader.GetString(1),
                        Color = reader.GetInt32N(2)
                        
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
