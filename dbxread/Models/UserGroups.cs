using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

namespace DbxRead.Models
{
    public class UserGroups
    {
        public int UserGroupId { get; set; }
        public string UserGroupName { get; set; }
        public int? Rights { get; set; }
        
        public static List<UserGroups> ReadTable(FbConnection connection)
        {
            try
            {
                var myCommand = new FbCommand
                {
                    CommandText = "SELECT r.USERID, r.USER_NAME, r.USER_PASSWORD, r.USER_GROUPID FROM USERS r",
                    Connection = connection
                };


                var reader = myCommand.ExecuteReader();
                var records = new List<UserGroups>();
                if (!reader.HasRows) return records;

                while (reader.Read())
                {
                    var m = new UserGroups
                    {
                        UserGroupId = reader.GetInt32(0),
                        UserGroupName = reader.GetString(1),
                        Rights = reader.GetInt32N(2)
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
