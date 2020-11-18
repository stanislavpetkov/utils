using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using MFORMATSLib;
namespace DbxRead
{
    class Program
    {
        public static void Main(string[] args)
        {
            var startTime = DateTime.Now;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);


            var mySerializer1 = new XmlSerializer(typeof(DataBoxExport));

            var planetaHDFile = new FileStream(@"C:\Users\Control1\Desktop\PLANETA_HD_3.xml", FileMode.Open);
            // Call the Deserialize method and cast to the object type.
            var planetaHD = (DataBoxExport)mySerializer1.Deserialize(planetaHDFile);
            planetaHDFile.Close();


            var planetaSDFile = new FileStream(@"C:\Users\Control1\Desktop\PLANETA_SD_3.xml", FileMode.Open);
            // Call the Deserialize method and cast to the object type.
            var mySerializer2 = new XmlSerializer(typeof(DataBoxExport));
            var planetaSD = (DataBoxExport)mySerializer2.Deserialize(planetaSDFile);
            planetaSDFile.Close();


            DataBoxExport outputXML = new DataBoxExport();


            Console.WriteLine("HD Sequences");

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


            Console.WriteLine("HD Types");
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

            Console.WriteLine("SD Sequences");
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


            Console.WriteLine("SD Types");

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


            Console.WriteLine("SD Records");
            foreach (DataBoxExportDataBoxRecord sdRec in planetaSD.DataBoxRecord)
            {

                //It doesn't exist
                sdRec.clipid = string.Format("PLHD-{0:000000}", cntr); cntr++;

                sdRec.CustomProperties.Clear(); //REMOVE SD custom Props

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


            Console.WriteLine("HD Records");
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
                hdRec.clipid = string.Format("PLHD-{0:000000}", cntr); cntr++;
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
                                outRec.CustomProperties = hdRec.CustomProperties;
                                
                                break;
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



            Console.WriteLine("Sort Sequences");
            outputXML.Sequences.Sort(CompareSequences);

            Console.WriteLine("Sort Types");
            foreach (var type in outputXML.Types)
            {
                type.category.Sort(CompareCategories);
                type.genre.Sort(CompareGenres);
            }

            outputXML.Types.Sort(CompareTypes);





uint filesProcessed = 0;
            var all_recs = outputXML.DataBoxRecord.Count();

            Console.WriteLine("Media Info");
            foreach (var dbr in outputXML.DataBoxRecord)
            {
                foreach (var inst in dbr.Instances)
                {
                    filesProcessed++;
                    inst.stream.Status = 1;
                    inst.stream.StatusSpecified = true;
                    if (string.IsNullOrWhiteSpace(inst.stream.LanguageID))
                    {
                        inst.stream.LanguageID = "Български";
                    }
                    if (inst.stream.FileSize > 0)
                    {
                        if (string.IsNullOrWhiteSpace(inst.stream.FileName))
                        {
                            Console.WriteLine("OOOPS");
                            continue;
                        }
                        var fn = Path.GetFileName(inst.stream.FileName);
                        if (fn != null)
                        {
                            Console.WriteLine($"Processing '{fn}' {filesProcessed} / {all_recs}");
                        }
                        else
                        {
                            Console.WriteLine("Processing {} / {}", filesProcessed, all_recs);
                        }
                        try
                        {
                            MFReader reader = new MFReader();
                            reader.ReaderOpen(inst.stream.FileName, "");
                            reader.ReaderDurationGet(out double duration);
                            reader.SourceFrameGetByNumber(0, -1, out MFFrame firstFrame, "");
                            reader.SourceFrameGetByTime(duration + 10, -1, out MFFrame lastFrame, "");

                            firstFrame.MFTimeGet(out M_TIME stTime);
                            lastFrame.MFTimeGet(out M_TIME enTime);
                            long frameDuration = stTime.rtEndTime - stTime.rtStartTime;
                            uint seconds = (uint)((enTime.rtEndTime - stTime.rtStartTime) / 10000000);
                            var frames = (uint)(((enTime.rtEndTime - stTime.rtStartTime) - ((long)seconds) * 10000000) / frameDuration);

                            var tmp = seconds;
                            uint h, m, s, f;
                            h = seconds / 3600;
                            seconds -= h * 3600;
                            m = seconds / 60;
                            seconds -= m * 60;
                            s = seconds;
                            f = frames;

                            
                            inst.duration = h | m << 8 | s << 16 | f << 24;
                            inst.durationSpecified = true;
                            inst.stream.OUT_P = inst.duration;
                            dbr.duration = inst.duration;
                            dbr.durationSpecified = true;

                            firstFrame.MFAllGet(out MF_FRAME_INFO fi);
                            inst.stream.Width = (uint)fi.avProps.vidProps.nWidth; 
                            inst.stream.Height = (uint)fi.avProps.vidProps.nHeight;
                            inst.stream.VideoBitrate = 50000000;
                            inst.stream.SampleRate = (uint)fi.avProps.audProps.nSamplesPerSec;
                            inst.stream.AudioBitRate = 384;
                            inst.stream.Channels = (uint)fi.avProps.audProps.nChannels;
                            inst.stream.FrameRate = (uint)fi.avProps.vidProps.dblRate;
                            inst.stream.VCT = "MPEG-2";


                            System.Runtime.InteropServices.Marshal.ReleaseComObject(firstFrame);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(lastFrame);
                            reader.ReaderClose();
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(reader);
                        }
                        catch (Exception)
                        { }

                    }
                }
            }



            //sdRec.CustomProperties
            Console.WriteLine("Media Info");
            foreach (var dbr in outputXML.DataBoxRecord)
            {
                if (dbr.CustomProperties.Count<1)
                {
                    DataBoxExportDataBoxRecordKeyword k = new DataBoxExportDataBoxRecordKeyword
                    {
                        name = "NO CUST PROPS"
                    };
                    dbr.Keywords.Add(k);
                    continue;
                }


                if (dbr.CustomProperties.Any(p => string.IsNullOrWhiteSpace(p.value)))
                {
                    DataBoxExportDataBoxRecordKeyword k = new DataBoxExportDataBoxRecordKeyword
                    {
                        name = "EMPTY CUST PROPS"
                    };
                    dbr.Keywords.Add(k);
                }
            }

                var myOutputSerializer = new XmlSerializer(typeof(DataBoxExport));

            var outputFile = new FileStream(@"C:\Users\Control1\Desktop\DATABOX_OUTPUT.xml", FileMode.Create);
            // Call the Deserialize method and cast to the object type.
            myOutputSerializer.Serialize(outputFile, outputXML);

            outputFile.Close();


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