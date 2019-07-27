using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

namespace DbxRead.Models
{
    public class Stream
    {
        public int StreamId { get; set; }
        public int? InstanceId { get; set; }
        public float? AudioLevel { get; set; }
        public string FileName { get; set; }
        public string StreamName { get; set; }
        public int? StreamType { get; set; }
        public int? VideoInfoId { get; set; }
        public int? AudioInfoId { get; set; }
        public int? InP { get; set; }
        public int? OutP { get; set; }
        public int? LanguageId { get; set; }
        public string Main { get; set; }
        public int? Status { get; set; }
        public int? Part { get; set; }
        public int? FileSize { get; set; }


        public static List<Stream> ReadTable(FbConnection connection)
        {
            try
            {
                var myCommand = new FbCommand
                {
                    CommandText = @"SELECT r.STREAMID, r.INSTANCEID, r.AUDIO_LEVEL, r.FILE_NAME," +
                                  "r.STREAM_NAME, r.STREAM_TYPE, r.VIDEO_INFOID, r.AUDIO_INFOID, r.IN_P, r.OUT_P, " +
                                  "r.LANGUAGEID, r.MAIN, r.STATUS, r.PART, r.FILE_SIZE FROM STREAM r",
                    Connection = connection
                };


                var reader = myCommand.ExecuteReader();
                var records = new List<Stream>();
                if (!reader.HasRows) return records;

                while (reader.Read())
                {
                    var m = new Stream
                    {
                        StreamId = reader.GetInt32(0),
                        InstanceId = reader.GetInt32N(1),
                        AudioLevel = reader.GetFloatN(2),
                        FileName = reader.GetString(3),
                        StreamName = reader.GetString(4),
                        StreamType = reader.GetInt32N(5),
                        VideoInfoId = reader.GetInt32N(6),
                        AudioInfoId = reader.GetInt32N(7),
                        InP = reader.GetInt32N(8),
                        OutP = reader.GetInt32N(9),
                        LanguageId = reader.GetInt32N(10),
                        Main = reader.GetString(11),
                        Status = reader.GetInt32N(12),
                        Part = reader.GetInt32N(13),
                        FileSize = reader.GetInt32N(14)
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