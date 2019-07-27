using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

namespace DbxRead.Models
{
    public class Quality
    {
        public int QualityId { get; set; }
        public string QualityName { get; set; }
        public int? Color { get; set; }
        public int? QOrder { get; set; }


        public static List<Quality> ReadTable(FbConnection connection)
        {
            try
            {
                var myCommand = new FbCommand
                {
                    CommandText = "SELECT r.QUALITYID, r.QUALITY_NAME, r.COLOR, r.Q_ORDER FROM QUALITY r",
                    Connection = connection
                };


                var reader = myCommand.ExecuteReader();
                var records = new List<Quality>();
                if (!reader.HasRows) return records;

                while (reader.Read())
                {
                    var m = new Quality
                    {
                        QualityId = reader.GetInt32(0),
                        QualityName = reader.GetString(1),
                        Color = reader.GetInt32N(2),
                        QOrder = reader.GetInt32N(3)
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