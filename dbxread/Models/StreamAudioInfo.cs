using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

namespace dbxread.Models
{
    public class StreamAudioInfo
    {
        public int StreamInfoid { get; set; }
        public string ACT { get; set; }
        public int? AudioSampleRate { get; set; }
        public int? AudioBitRate { get; set; }
        public int? AudioChannels { get; set; }
        
        
        public static List<StreamAudioInfo> ReadTable(ref FbConnection connection)
        {
            try
            {
                var myCommand = new FbCommand
                {
                    CommandText = "SELECT r.STREAM_INFOID, r.A_C_T, r.AUDIO_SAMPLE_RATE, r.AUDIO_BIT_RATE, "+
                                  "r.AUDIO_CHANNELS FROM STREAM_AUDIO_INFO r",
                    Connection = connection
                };


                var reader = myCommand.ExecuteReader();
                var records = new List<StreamAudioInfo>();
                if (!reader.HasRows) return records;

                while (reader.Read())
                {
                    var m = new StreamAudioInfo()
                    {
                        StreamInfoid = reader.GetInt32(0),
                        ACT = reader.GetString(1),
                        AudioSampleRate = reader.GetInt32N(2),
                        AudioBitRate = reader.GetInt32N(3),
                        AudioChannels = reader.GetInt32N(4)
                        
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
