using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

namespace dbxread.Models
{
    public class StreamVideoInfo
    {
        public int StreamInfoid { get; set; }
        public string VCT { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public float? FrameRate { get; set; }
        public int? VideoBitRate { get; set; }


        public static List<StreamVideoInfo> ReadTable(FbConnection connection)
        {
            try
            {
                var myCommand = new FbCommand
                {
                    CommandText = "SELECT r.STREAM_INFOID, r.V_C_T, r.WIDTH, r.HEIGHT, r.FRAME_RATE, " +
                                  "r.VIDEO_BIT_RATE FROM STREAM_VIDEO_INFO r",
                    Connection = connection
                };


                var reader = myCommand.ExecuteReader();
                var records = new List<StreamVideoInfo>();
                if (!reader.HasRows) return records;

                while (reader.Read())
                {
                    var m = new StreamVideoInfo()
                    {
                        StreamInfoid = reader.GetInt32(0),
                        VCT = reader.GetString(1),
                        Width = reader.GetInt32N(2),
                        Height = reader.GetInt32N(3),
                        FrameRate = reader.GetFloatN(4),
                        VideoBitRate = reader.GetInt32N(5)
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