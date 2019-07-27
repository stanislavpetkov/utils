using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

namespace DbxRead.Models
{
    public class MediaType
    {
        public int MediaTypeId { get; set; }
        public string MediaName { get; set; }
        public int? Status { get; set; }
        public int? PrepareTime { get; set; }
        public int? Color { get; set; }
        
        public static List<MediaType> ReadTable(FbConnection connection)
        {
            try
            {
                var myCommand = new FbCommand
                {
                    CommandText = "SELECT r.MEDIA_TYPEID, r.MEDIA_NAME, r.STATUS, r.PREPARE_TIME, r.COLOR FROM MEDIA_TYPE r",
                    Connection = connection
                };


                var reader = myCommand.ExecuteReader();
                var records = new List<MediaType>();
                if (!reader.HasRows) return records;

                while (reader.Read())
                {
                    var m = new MediaType()
                    {
                        MediaTypeId = reader.GetInt32(0),
                        MediaName = reader.GetString(1),
                        Status = reader.GetInt32N(2),
                        PrepareTime = reader.GetInt32N(3),
                        Color = reader.GetInt32N(4)
                        
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
