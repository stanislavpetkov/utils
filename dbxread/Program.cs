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

        public static void Main(string[] _)
        {
            //JoinHDandSDXMLs();

            MFormatsSDKLic.IntializeProtection();





            var mySerializer1 = new XmlSerializer(typeof(DataBoxExport));

            var sourcefile = new FileStream(@"C:\Users\Control1\Desktop\DATABOX_PLANETA_FOLK_SD_16x9.xml", FileMode.Open);
            // Call the Deserialize method and cast to the object type.
            var srcDB = (DataBoxExport)mySerializer1.Deserialize(sourcefile);
            sourcefile.Close();





            var srcLocation = @"\\storage2\Planeta HD\";

            var files_all = Directory.GetFiles(srcLocation, "*.*", SearchOption.AllDirectories);




            string[] files = files_all.Where(p => Path.GetFileName(p).First() != '.').ToArray();

            Console.WriteLine($"Files on {srcLocation} - {files.Length}");

            //<MediaType name="LocalHDD" Status="3" PrepareTime="0"/>

            srcDB.MediaTypes.Clear();

            DataBoxMediaType mt = new()
            {
                name = "Network Storage",
                Status = "3",
                PrepareTime = "0"
            };
            srcDB.MediaTypes.Add(mt);



            //var sourcefileOuttest = new FileStream(@"C:\Users\Control1\Desktop\TEST_FILE.xml", FileMode.Create);
            //mySerializer1.Serialize(sourcefileOuttest, srcDB);
            //sourcefileOuttest.Close();

            Int64 id = 0;
            foreach (var elm in srcDB.DataBoxRecord)
            {
                elm.clipid = $"FOLK-2022-12-30-CVT-{id}";
                id++;
            }

                foreach (var elm in srcDB.DataBoxRecord)
            {
                bool Found = false;
                foreach (var inst in elm.Instances)
                {


                    string fn = files.FirstOrDefault(p => Path.GetFileName(p.ToLowerInvariant()) == Path.GetFileName(inst.stream.FileName.ToLowerInvariant()));
                    if (!string.IsNullOrWhiteSpace(fn))
                    {
                        inst.stream.media.Pool = srcLocation;
                        inst.stream.media.MediaName = "Planeta Folk HD";
                        inst.stream.media.MediaType = mt.name;


                        var fi = new FileInfo(fn);
                        inst.stream.FileSize = fi.Length;
                        inst.stream.FileName = fn;
                        Found = true;
                    }
                    else
                    {
                        inst.stream.media.Pool = srcLocation;
                        inst.stream.media.MediaName = "Planeta Folk HD";
                        inst.stream.media.MediaType = mt.name;
                        //inst.stream.FileName - keep the original
                        inst.stream.FileSize = 0;
                        Found = false;
                    }
                }

                if (!Found)
                {


                    DataBoxExportDataBoxRecordKeyword k = new()
                    {
                        name = "FILE NOT FOUND"
                    };
                    elm.Keywords.Add(k);
                }
            }

            foreach (var dbr in srcDB.Types)
            {
                if (dbr.name == "16x9 МУЗИКА - ФОЛК")
                {
                    dbr.name = "МУЗИКА - ФОЛК";
                }
                else if (dbr.name == "16x9 КАШОВЕ - ФОЛК")
                {
                    dbr.name = "КАШОВЕ - ФОЛК";
                }
            }
            var all_recs = srcDB.DataBoxRecord.Count;
            ulong filesProcessed = 0;
            Console.WriteLine("Media Info");



            foreach (var dbr in srcDB.DataBoxRecord)
            {


                if (string.IsNullOrWhiteSpace(dbr.category))
                {
                    dbr.category = "NO CATEGORY!!!";
                }
                else
                {
                    dbr.category = dbr.category.Trim();
                    while ((dbr.category.Last() == '-') || (dbr.category.Last() == ' '))
                    {
                        dbr.category = dbr.category.Substring(startIndex: 0, dbr.category.Length - 1);
                        if (string.IsNullOrWhiteSpace(dbr.category))
                        {
                            dbr.category = "NO CATEGORY!!!";
                            break;
                        }
                    }
                }


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
                            Console.WriteLine($"Processing {filesProcessed} / {all_recs}");
                        }
                        try
                        {
                            var reader = new MFReader();


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

                            var props = reader as IMFProps;





                            props.PropsGet("info::video.0::codec_name", out var vcodecName);
                            props.PropsGet("info::video.0::bit_rate", out var vBitRate);
                            if (!uint.TryParse(vBitRate, out var videoBitRate))
                            {
                                videoBitRate = 0;
                            }

                            props.PropsGet("info::audio.0::codec_name", out var acodecName);

                            props.PropsGet("info::audio.0::bit_rate", out var aBitRate);
                            if (!uint.TryParse(aBitRate, out var audioBitRate))
                            {
                                audioBitRate = 0;
                            }


                            firstFrame.MFAllGet(out MF_FRAME_INFO fi);

                            string Suffix = (fi.avProps.vidProps.nWidth == 1920) ? "HD" :
                                (fi.avProps.vidProps.nWidth == 3840) ? "4K" :
                                (fi.avProps.vidProps.nWidth == 720) ? $"SD {fi.avProps.vidProps.nAspectX}x{fi.avProps.vidProps.nAspectY}" :
                                $"{fi.avProps.vidProps.nWidth}x{fi.avProps.vidProps.nHeight}";



                            if (dbr.type == "16x9 МУЗИКА - ФОЛК")
                            {
                                dbr.type = $"МУЗИКА - ФОЛК";
                            }
                            else if (dbr.type == "16x9 КАШОВЕ - ФОЛК")
                            {
                                dbr.type = $"КАШОВЕ - ФОЛК";
                            }




                            inst.stream.Width = (uint)fi.avProps.vidProps.nWidth;
                            inst.stream.Height = (uint)fi.avProps.vidProps.nHeight;
                            inst.stream.VideoBitrate = videoBitRate;
                            inst.stream.SampleRate = (uint)fi.avProps.audProps.nSamplesPerSec;
                            inst.stream.AudioBitRate = audioBitRate / 1000;
                            inst.stream.Channels = (uint)fi.avProps.audProps.nChannels;
                            inst.stream.FrameRate = (uint)fi.avProps.vidProps.dblRate;
                            inst.stream.VCT = vcodecName;
                            inst.stream.ACT = acodecName;

                            if (OperatingSystem.IsWindows())
                            {

                                //System.Runtime.InteropServices.Marshal.Release(props);                                
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(firstFrame);
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(lastFrame);
                                reader.ReaderClose();
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(reader);
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Exception {e.Message}");
                        }

                    }
                }
            }



            //sdRec.CustomProperties
            Console.WriteLine("Media Info");

            var sourcefileOut = new FileStream(@"C:\Users\Control1\Desktop\DATABOX_PLANETA_FOLK_HD.xml", FileMode.Create);
            mySerializer1.Serialize(sourcefileOut, srcDB);
            sourcefileOut.Close();





            var xmlFiles = new List<string>();

            foreach (var dbr in srcDB.DataBoxRecord)
            {
                foreach (var inst in dbr.Instances)
                {
                    xmlFiles.Add(inst.stream.FileName.ToLowerInvariant());
                }
            }

            var filesThatDontExistOnStorage = new List<string>();
            var filesThatDontExistOnDB = new List<string>();

            foreach (var storFile in files)
            {
                var fileName = Path.GetFileName(storFile);
                if (!xmlFiles.Any(p => p == fileName)) filesThatDontExistOnDB.Add(storFile);
            }




            foreach (var xmlfile in xmlFiles)
            {
                if (!files.Any(p => Path.GetFileName(p) == xmlfile)) filesThatDontExistOnStorage.Add(xmlfile);
            }


            {
                TextWriter tw = new StreamWriter("filesThatDontExistOnDB.txt");

                foreach (String s in filesThatDontExistOnDB)
                    tw.WriteLine(s);

                tw.Close();
            }

            {
                TextWriter tw = new StreamWriter("filesThatDontExistOnStorage.txt");

                foreach (String s in xmlFiles)
                    tw.WriteLine(s);

                tw.Close();
            }


        }

        public static void JoinHDandSDXMLs()
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


            var outputXML = new DataBoxExport();


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

                var Categories = new List<DataBoxExportTypeCategory>();

                Categories.AddRange(typeRes.category);
                Categories.AddRange(type.category);

                typeRes.category = Categories.Distinct().ToList();


                var Genres = new List<DataBoxExportTypeGenre>();

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

                            if (OperatingSystem.IsWindows())
                            {
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(firstFrame);
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(lastFrame);
                                reader.ReaderClose();
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(reader);
                            }
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
                if (dbr.CustomProperties.Count < 1)
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