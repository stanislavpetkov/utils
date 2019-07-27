using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FirebirdSql.Data.FirebirdClient;

namespace DbxRead.Models
{
    public class Notes
    {
        /// 0-record,1-instance,2-stream,3-media,4-taglines,5-comments,6-trivia,7-Tag
        public int? TypeId { get; set; }


        ///  RecId from Master 
        public int? ItemId { get; set; }


        /// The Value
        public string Text { get; set; }


        public static string TypeName(int typeId)
        {
            // 0-record,1-instance,2-stream,3-media,4-taglines,5-comments,6-trivia,7-Tag
            switch (typeId)
            {
                case 0: return "Record";
                case 1: return "Instance";
                case 2: return "stream";
                case 3: return "Media";

                case 4: return "Tag lines";
                case 5: return "Comments";
                case 6: return "Trivia";
                case 7: return "Tag";
                default: return $"Unknown {typeId}";
            }
        }


        public void RemoveRecord(FbConnection connection, FbTransaction transaction, TextWriter logFile)
        {
               const string notesDeleteSql = "DELETE FROM NOTES a WHERE a.ITEMID = @recid";
               
               var myCommand = new FbCommand(notesDeleteSql, connection, transaction);
               myCommand.Parameters.Add("@recid", ItemId); //ItemId is RecId
               var x = myCommand.ExecuteNonQuery();
               Console.WriteLine($"Deleted {x} records from Notes with ItemId/RecId {ItemId}");            
        }

        public static List<Notes> ReadTable(FbConnection connection, bool joinStrings)
        {
            try
            {
                var myCommand = new FbCommand
                {
                    CommandText = "SELECT r.TYPEID, r.ITEMID, r.TEXT FROM NOTES r",
                    Connection = connection
                };


                var reader = myCommand.ExecuteReader();
                var records = new List<Notes>();
                if (!reader.HasRows) return records;

                while (reader.Read())
                {
                    var m = new Notes()
                    {
                        TypeId = reader.GetInt32N(0),
                        ItemId = reader.GetInt32N(1),
                        Text = reader.GetString(2)
                    };
                    
                    if (!joinStrings)
                    {
                        records.Add(m);
                        continue;
                    }

                    var note = records.FirstOrDefault(p => p.ItemId == m.ItemId && p.TypeId == m.TypeId);
                    if (note == null)
                    {
                        records.Add(m);
                        continue;
                    }

                    note.Text += m.Text;
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