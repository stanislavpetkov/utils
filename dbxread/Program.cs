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

                var planetaHDFile = new FileStream(@"C:\Users\Control1\Desktop\DATABOX_PLANETA_HD.xml", FileMode.Open);
                // Call the Deserialize method and cast to the object type.
                var planetaHD = (DataBoxExport)mySerializer1.Deserialize(planetaHDFile);
                planetaHDFile.Close();


                var planetaSDFile = new FileStream(@"C:\Users\Control1\Desktop\DATABOX_PLANETA_SD.xml", FileMode.Open);
                // Call the Deserialize method and cast to the object type.
                var mySerializer2 = new XmlSerializer(typeof(DataBoxExport));
                var planetaSD = (DataBoxExport)mySerializer2.Deserialize(planetaSDFile);
                planetaSDFile.Close();


                DataBoxExport outputXML = new DataBoxExport();


                foreach (var seq in planetaHD.Sequences)
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


                uint cntr = 1;
                uint missing = 0;
                uint all_files = 0;

                foreach (DataBoxExportDataBoxRecord sdRec in planetaSD.DataBoxRecord)
                {

                    //It doesn't exist
                    sdRec.clipid = string.Format("PLHD-{0:000000}", cntr); cntr++;

                    if (!FixPath(ref sdRec.instancesField))
                    {
                        DataBoxExportDataBoxRecordKeyword k = new DataBoxExportDataBoxRecordKeyword();
                        k.name = "FILE NOT FOUND";
                        sdRec.Keywords.Add(k);
                        missing++;
                    }
                    all_files++;
                    outputXML.DataBoxRecord.Add(sdRec);
                }



                foreach (var hdRec in planetaHD.DataBoxRecord)
                {
                    //Find if we have one of the filenames in the outputXML
                    //------------------------------------------------------

                    if (!FixPath(ref hdRec.instancesField))
                    {
                        DataBoxExportDataBoxRecordKeyword k = new DataBoxExportDataBoxRecordKeyword
                        {
                            name = "FILE NOT FOUND"
                        };
                        hdRec.Keywords.Add(k);
                        missing++;
                    }

                    bool bAtleastOne = false;
                    foreach (var inst in hdRec.Instances)
                    {
                        foreach (var outRec in outputXML.DataBoxRecord)
                        {
                            bool bFound = false;
                            foreach (var outInst in outRec.Instances)
                            {
                                if (outInst.stream.FileName.ToUpperInvariant() == inst.stream.FileName.ToUpperInvariant())
                                {
                                    bAtleastOne = true;
                                    bFound = true;
                                    foreach (var custProps in hdRec.CustomProperties)
                                    {
                                        Console.WriteLine("FIX THE CUSTOM PROPS");
                                    }

                                }
                            }
                            if (bFound) break;
                        }

                    }

                    if (!bAtleastOne)
                    {
                        all_files++;
                        outputXML.DataBoxRecord.Add(hdRec);
                    }

                }




                outputXML.Sequences.Sort(CompareSequences);
                foreach (var type in outputXML.Types)
                {
                    type.category.Sort(CompareCategories);
                    type.genre.Sort(CompareGenres);
                }

                outputXML.Types.Sort(CompareTypes);

                var myOutputSerializer = new XmlSerializer(typeof(DataBoxExport));

                var outputFile = new FileStream(@"C:\Users\Control1\Desktop\DATABOX_OUTPUT.xml", FileMode.Create);
                // Call the Deserialize method and cast to the object type.
                myOutputSerializer.Serialize(outputFile, outputXML);

                outputFile.Close();
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


        //return false if file not found
        private static bool FixPath(ref List<DataBoxExportDataBoxRecordInstance> instances)
        {

            bool bAtLeastOneFileNotFound = false;
            foreach (var instance in instances)
            {

                instance.stream.media.MediaName = "Main Storage";
                instance.stream.media.Pool = @"\\Storage2\Planeta HD\";
                string newServer = "Storage2";
                string newFolder = "Planeta HD";
                string FileName = instance.stream.FileName;

                if (string.IsNullOrEmpty(FileName))
                {
                    bAtLeastOneFileNotFound = true;
                    continue;
                }
                if (FileName.Length < 3)
                {
                    bAtLeastOneFileNotFound = true;
                    continue;
                }
                if (FileName.Substring(0, 2) != "\\\\")
                {

                    bAtLeastOneFileNotFound = true;
                    continue;
                }

                var posStart = FileName.IndexOf("\\", 0);
                var posEnd = FileName.IndexOf("\\", 2);
                var server = FileName.Substring(posStart + 2, posEnd - (posStart + 2));
                if (server != newServer)
                {
                    FileName = FileName.Replace(server, newServer);
                }

                {
                    var start = FileName.ToUpper().IndexOf("\\PLANETA TV\\", 0);
                    if (start != -1)
                    {
                        string endFN = FileName.Substring(start + ("\\PLANETA TV\\").Length);
                        FileName = FileName.Substring(0, start) + "\\" + newFolder + "\\" + endFN;
                    }
                }

                {
                    var startU = FileName.ToUpper().IndexOf("\\UPSCALED\\", 0);
                    if (startU != -1)
                    {
                        string endFN = FileName.Substring(startU + ("\\UPSCALED\\").Length);
                        FileName = FileName.Substring(0, startU) + "\\" + endFN;
                    }
                }
                var FileInfo = new System.IO.FileInfo(FileName);

                if (!FileInfo.Exists) bAtLeastOneFileNotFound = true;
                instance.stream.FileSize = FileInfo.Exists ? FileInfo.Length : 0;
                instance.stream.FileSizeSpecified = FileInfo.Exists;

                instance.stream.FileName = FileName;

            }

            return bAtLeastOneFileNotFound ? false : true;

        }
        private static int CompareTypes(DataBoxExportType x, DataBoxExportType y)
        {
            return string.Compare(x.name, y.name, true);
        }

        private static int CompareGenres(DataBoxExportTypeGenre x, DataBoxExportTypeGenre y)
        {
            return string.Compare(x.name, y.name, true);
        }

        private static int CompareCategories(DataBoxExportTypeCategory x, DataBoxExportTypeCategory y)
        {

            return string.Compare(x.name, y.name, true);
        }

        private static int CompareSequences(DataBoxExportSequence x, DataBoxExportSequence y)
        {
            return string.Compare(x.name, y.name, true);
        }
    }
}