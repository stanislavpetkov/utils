using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

namespace dbxread.Models
{
    public class Categories
    {
        public int Categoryid { get; set; }
        public string CategoryName { get; set; }
        public int? Typeid { get; set; }
        public int? Color { get; set; }
        
        public static List<Categories> ReadTable(ref FbConnection connection)
        {
            try
            {
                var myCommand = new FbCommand
                {
                    CommandText = "SELECT r.CATEGORYID, r.CATEGORY_NAME, r.TYPEID, r.COLOR FROM CATEGORIES r",
                    Connection = connection
                };


                var reader = myCommand.ExecuteReader();
                var records = new List<Categories>();
                if (!reader.HasRows) return records;

                while (reader.Read())
                {
                    var m = new Categories()
                    {
                        Categoryid = reader.GetInt32(0),
                        CategoryName = reader.GetString(1),
                        Typeid = reader.GetInt32N(2),
                        Color = reader.GetInt32N(3)
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
