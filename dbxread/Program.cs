using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DbxRead
{
    class Program
    {
        
        //ALTER CHARACTER SET WIN1251 SET DEFAULT collation WIN1251;
        private const string ConnectionStringHd = "User=SYSDBA;" +
                                          "Password=masterkey;" +
                                          @"Database=127.0.0.1:c:\development\databox_planeta_hd.gdb;" +
                                          "Port=3050;" +
                                          "Dialect=3;" +
                                          "Charset=WIN1251;" + //Ensure database is enforced to win1251
                                          "Role=;" +
                                          "Connection lifetime=15;" +
                                          "Pooling=true;" +
                                          "MinPoolSize=0;" +
                                          "MaxPoolSize=50;" +
                                          "Packet Size=8192;" +
                                          "ServerType=0";

        private const string ConnectionStringResult = "User=SYSDBA;" +
                                                     "Password=masterkey;" +
                                                     @"Database=127.0.0.1:c:\development\databox_result.gdb;" +
                                                     "Port=3050;" +
                                                     "Dialect=3;" +
                                                     "Charset=WIN1251;" + //Ensure database is enforced to win1251
                                                     "Role=;" +
                                                     "Connection lifetime=15;" +
                                                     "Pooling=true;" +
                                                     "MinPoolSize=0;" +
                                                     "MaxPoolSize=50;" +
                                                     "Packet Size=16384;" +
                                                     "ServerType=0";

        private const string ConnectionStringSd = "User=SYSDBA;" +
                                               "Password=masterkey;" +
                                               @"Database=127.0.0.1:c:\development\databox_planeta_16x9.gdb;" +
                                               "Port=3050;" +
                                               "Dialect=3;" +
                                               "Charset=WIN1251;" + //Ensure database is enforced to win1251
                                               "Role=;" +
                                               "Connection lifetime=15;" +
                                               "Pooling=true;" +
                                               "MinPoolSize=0;" +
                                               "MaxPoolSize=50;" +
                                               "Packet Size=8192;" +
                                               "ServerType=0";

        private const string ConnectionStringFolk = "User=SYSDBA;" +
                                                   "Password=masterkey;" +
                                                   "Database=zbook:databox_folk_16x9.gdb;" +
                                                   "Port=3050;" +
                                                   "Dialect=3;" +
                                                   "Charset=WIN1251;" + //Ensure database is enforced to win1251
                                                   "Role=;" +
                                                   "Connection lifetime=15;" +
                                                   "Pooling=true;" +
                                                   "MinPoolSize=0;" +
                                                   "MaxPoolSize=50;" +
                                                   "Packet Size=8192;" +
                                                   "ServerType=0";


        public static void Main(string[] args)
        {
            var startTime = DateTime.Now;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            try
            {
                
                var mySerializer1 = new XmlSerializer(typeof(DataBoxExport));

                var planetaHDFile= new FileStream(@"C:\Users\Control1\Desktop\DATABOX_PLANETA_HD.xml", FileMode.Open);
                // Call the Deserialize method and cast to the object type.
                var planetaHD = (DataBoxExport)mySerializer1.Deserialize(planetaHDFile);
                planetaHDFile.Close();


                var planetaSDFile = new FileStream(@"C:\Users\Control1\Desktop\DATABOX_PLANETA_SD.xml", FileMode.Open);
                // Call the Deserialize method and cast to the object type.
                var mySerializer2 = new XmlSerializer(typeof(DataBoxExport));
                var planetaSD = (DataBoxExport)mySerializer2.Deserialize(planetaSDFile);
                planetaSDFile.Close();


                DataBoxExport outputXML = new DataBoxExport();


                foreach(var seq in planetaHD.Sequences)
                {
                    // seq.EpisodeCount;
                    var res = outputXML.Sequences.Find(p => p.name.ToLowerInvariant() == seq.name.ToLowerInvariant());
                    if (res != null)
                    {
                        continue;
                    }

                    outputXML.Sequences.Add(seq);
                }



                foreach (var type in planetaHD.Types)
                {
                    // seq.EpisodeCount;
                    var typeRes = outputXML.Types.Find(p => p.name.ToLowerInvariant() == type.name.ToLowerInvariant());
                    if (typeRes == null)
                    {
                        outputXML.Types.Add(type);
                        continue;
                    }

                    List<DataBoxExportTypeCategory> Categories = new List<DataBoxExportTypeCategory>();

                    Categories.AddRange(typeRes.category);
                    Categories.AddRange(type.category);

                    typeRes.category = Categories.Distinct().ToList();


                    List<DataBoxExportTypeGenre> Genres = new List<DataBoxExportTypeGenre>();

                    Genres.AddRange(typeRes.genre);
                    Genres.AddRange(type.genre);

                    typeRes.genre = Genres.Distinct().ToList();
                }


                // ======================================================= PLANETA SD

                foreach (var seq in planetaSD.Sequences)
                {
                    // seq.EpisodeCount;
                    var res = outputXML.Sequences.Find(p => p.name.ToLowerInvariant() == seq.name.ToLowerInvariant());
                    if (res != null)
                    {
                        continue;
                    }

                    outputXML.Sequences.Add(seq);
                }



                foreach (var type in planetaSD.Types)
                {
                    // seq.EpisodeCount;
                    var typeRes = outputXML.Types.Find(p => p.name.ToLowerInvariant() == type.name.ToLowerInvariant());
                    if (typeRes == null)
                    {
                        outputXML.Types.Add(type);
                        continue;
                    }

                    List<DataBoxExportTypeCategory> Categories = new List<DataBoxExportTypeCategory>();

                    Categories.AddRange(typeRes.category);
                    Categories.AddRange(type.category);

                    typeRes.category = Categories.Distinct().ToList();


                    List<DataBoxExportTypeGenre> Genres = new List<DataBoxExportTypeGenre>();

                    Genres.AddRange(typeRes.genre);
                    Genres.AddRange(type.genre);

                    typeRes.genre = Genres.Distinct().ToList();
                }

                //outputXML.DataBoxRecord;

                foreach (var hdRec in planetaHD.DataBoxRecord)
                {
                    var outRec = outputXML.DataBoxRecord.Find(p => p..name.ToLowerInvariant() == type.name.ToLowerInvariant());

                }




            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            var endTime = DateTime.Now;
            Console.WriteLine($"End Time {endTime} ProcessingTime {(endTime - startTime).TotalSeconds}");
            Console.WriteLine("Done");
        }
    }
}