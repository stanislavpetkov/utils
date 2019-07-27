using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

namespace DbxRead.Models
{
    public class Archive
    {
        public int ArchiveId { get; set; }
        public string ArchiveName { get; set; }
        public int? Color { get; set; }
        
        
        public static List<Archive> ReadTable(FbConnection connection)
        {
            try
            {
                var myCommand = new FbCommand
                {
                    CommandText = "SELECT r.ARCHIVEID, r.ARCHIVE_NAME, r.COLOR FROM ARCHIVE r",
                    Connection = connection
                };


                var reader = myCommand.ExecuteReader();
                var records = new List<Archive>();
                if (!reader.HasRows) return records;

                while (reader.Read())
                {
                    var m = new Archive()
                    {
                        ArchiveId = reader.GetInt32(0),
                        ArchiveName = reader.GetString(1),
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
