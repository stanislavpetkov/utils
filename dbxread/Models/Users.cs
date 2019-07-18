using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

namespace dbxread.Models
{
    public class Users
    {
        public int Userid { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public int? UserGroupId { get; set; }
        
        public static List<Users> ReadTable(ref FbConnection connection)
        {
            try
            {
                var myCommand = new FbCommand
                {
                    CommandText = "SELECT r.USERID, r.USER_NAME, r.USER_PASSWORD, r.USER_GROUPID FROM USERS r",
                    Connection = connection
                };


                var reader = myCommand.ExecuteReader();
                var records = new List<Users>();
                if (!reader.HasRows) return records;

                while (reader.Read())
                {
                    var m = new Users()
                    {
                        Userid = reader.GetInt32(0),
                        UserName = reader.GetString(1),
                        UserPassword = reader.GetString(2),
                        UserGroupId = reader.GetInt32N(3),
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
