using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DbxRead.Models;
using FirebirdSql.Data.FirebirdClient;


namespace DbxRead.DBManager
{
    public class DbManager
    {
        private readonly string _connectionString;
        private List<AgeRates> AgeRatesRecords { get; set; }
        private List<Annotations> AnnotationsRecords { get; set; }
        private List<Archive> ArchiveRecords { get; set; }
        private List<Categories> CategoriesRecords { get; set; }
        private List<Companies> CompaniesRecords { get; set; }
        private List<CompaniesInv> CompaniesInvRecords { get; set; }
        private List<CompanyActivity> CompanyActivityRecords { get; set; }
        private List<Countries> CountriesRecords { get; set; }
        private List<CountriesInv> CountriesInvRecords { get; set; }
        private List<CountryActivity> CountryActivityRecords { get; set; }
        private List<CustomProps> CustomPropsRecords { get; set; }
        private List<CustomPropsInv> CustomPropsInvRecords { get; set; }
        private List<DayMask> DayMaskRecords { get; set; }
        private List<EditItems> EditItemsRecords { get; set; }
        private List<Genres> GenresRecords { get; set; }
        private List<GenresInv> GenresInvRecords { get; set; }
        private List<Groups> GroupsRecords { get; set; }
        private List<Instance> InstanceRecords { get; set; }
        private List<Keywords> KeywordsRecords { get; set; }
        private List<KeywordsInv> KeywordsInvRecords { get; set; }
        private List<Languages> LanguagesRecords { get; set; }
        private List<LanguagesInv> LanguagesInvRecords { get; set; }
        private List<Master> MasterRecords { get; set; }
        private List<Media> MediaRecords { get; set; }
        private List<MediaType> MediaTypeRecords { get; set; }
        private List<Notes> NotesRecords { get; set; }
        private List<Persons> PersonsRecords { get; set; }
        private List<PersonsInv> PersonsInvRecords { get; set; }
        private List<Positions> PositionsRecords { get; set; }
        private List<Quality> QualityRecords { get; set; }
        private List<Sequences> SequencesRecords { get; set; }
        private List<SkipZones> SkipZonesRecords { get; set; }
        private List<Models.Stream> StreamRecords { get; set; }
        private List<StreamAudioInfo> StreamAudioInfoRecords { get; set; }
        private List<StreamVideoInfo> StreamVideoInfoRecords { get; set; }
        private List<Types> TypesRecords { get; set; }
        private List<Users> UsersRecords { get; set; }
        private List<UserGroups> UserGroupsRecords { get; set; }

        public DbManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbManager()
        {
            _connectionString = "";
            AgeRatesRecords = new List<AgeRates>();
            AnnotationsRecords = new List<Annotations>();
            ArchiveRecords = new List<Archive>();
            CategoriesRecords = new List<Categories>();
            CompaniesRecords = new List<Companies>();
            CompaniesInvRecords = new List<CompaniesInv>();
            CompanyActivityRecords = new List<CompanyActivity>();
            CountriesRecords = new List<Countries>();
            CountriesInvRecords = new List<CountriesInv>();
            CountryActivityRecords = new List<CountryActivity>();
            CustomPropsRecords = new List<CustomProps>();
            CustomPropsInvRecords = new List<CustomPropsInv>();
            DayMaskRecords = new List<DayMask>();
            EditItemsRecords = new List<EditItems>();
            GenresRecords = new List<Genres>();
            GenresInvRecords = new List<GenresInv>();
            GroupsRecords = new List<Groups>();
            InstanceRecords = new List<Instance>();
            KeywordsRecords = new List<Keywords>();
            KeywordsInvRecords = new List<KeywordsInv>();
            LanguagesRecords = new List<Languages>();
            LanguagesInvRecords = new List<LanguagesInv>();
            MasterRecords = new List<Master>();
            MediaRecords = new List<Media>();
            MediaTypeRecords = new List<MediaType>();
            NotesRecords = new List<Notes>();
            PersonsRecords = new List<Persons>();
            PersonsInvRecords = new List<PersonsInv>();
            PositionsRecords = new List<Positions>();
            QualityRecords = new List<Quality>();
            SequencesRecords = new List<Sequences>();
            SkipZonesRecords = new List<SkipZones>();
            StreamRecords = new List<Models.Stream>();
            StreamAudioInfoRecords = new List<StreamAudioInfo>();
            StreamVideoInfoRecords = new List<StreamVideoInfo>();
            TypesRecords = new List<Types>();
            UsersRecords = new List<Users>();
            UserGroupsRecords = new List<UserGroups>();
        }


        public void RemoveNotes()
        {
            var logFile = File.CreateText($"NotesRemoveReport.log");
            try
            {
                using (var connection = new FbConnection(_connectionString))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        foreach (var note in NotesRecords)
                        {
                            if (MasterRecords.Any(p => p.RecId == note.ItemId)) continue;

                            if (note.TypeId == null) continue;


                            logFile.WriteLine(
                                $"Missing master record for note with RecId {note.ItemId} and TypeID {note.TypeId}, typeName {Notes.TypeName(note.TypeId.Value)}, Text [{note.Text}]");

                            note.RemoveRecord(connection, transaction, logFile);
                        }

                        transaction.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                throw;
            }
        }


        private void RemoveRecords(Models.Stream stream, TextWriter writer)
        {
            if (stream.InstanceId == null)
            {
                writer.WriteLine("Instance Id = null");
                return;
            }

            var streamsCount = StreamRecords.Count(p => p.InstanceId == stream.InstanceId);
            if (streamsCount != 1)
            {
                writer.WriteLine("Multiple Streams for Same InstanceId");
                throw new Exception("Multiple Streams for Same InstanceId");
            }

            var instanceId = stream.InstanceId.Value;
            var instCount = InstanceRecords.Count(p => p.Instanceid == instanceId);
            if (instCount != 1)
            {
                writer.WriteLine("Multiple Instances for Same InstanceId");
                throw new Exception("Multiple Instances for Same InstanceId");
            }

            var inst = InstanceRecords.FirstOrDefault(p => p.Instanceid == instanceId);
            if (inst == null)
            {
                writer.WriteLine($"Can not find instance for instanceId {instanceId}");
                return;
            }

            writer.WriteLine(
                $"\n\nDeleting stream. StreamId {stream.StreamId}, instanceId {stream.InstanceId}, RecId {inst.Recid}");

            var audioInfo = StreamAudioInfoRecords.FirstOrDefault(p => p.StreamInfoid == stream.AudioInfoId);
            var videoInfo = StreamVideoInfoRecords.FirstOrDefault(p => p.StreamInfoid == stream.VideoInfoId);
            Master master = null;

            var instances = InstanceRecords.Count(p => p.Recid == inst.Recid);
            if (instances == 1)
            {
                master = MasterRecords.FirstOrDefault(p => p.RecId == inst.Recid);
            }

            using (var connection = new FbConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            const string streamDeleteSql = "DELETE FROM STREAM a WHERE a.STREAMID = @streamId";
                            const string mediaDeleteSql = "DELETE FROM MEDIA a WHERE a.STREAMID = @streamId";
                            const string instanceDeleteSql = "DELETE FROM INSTANCE a WHERE a.INSTANCEID = @instId";
                            const string audioInfoDeleteSql =
                                "DELETE FROM STREAM_AUDIO_INFO a WHERE a.STREAM_INFOID = @siid";
                            const string videoInfoDeleteSql =
                                "DELETE FROM STREAM_VIDEO_INFO a WHERE a.STREAM_INFOID = @siid";
                            const string masterDeleteSql = "DELETE FROM MASTER a WHERE a.RECID = @recid";
                            const string keywordsInvDeleteSql = "DELETE FROM KEYWORDS_INV a WHERE a.RECID = @recid";
                            //const string notesDeleteSql = "DELETE FROM NOTES a WHERE a.ITEMID = @recid";
                            const string customPropsInvDeleteSql =
                                "DELETE FROM CUSTOM_PROPS_INV a WHERE a.ITEM_ID = @recid";
                            const string personsInvDeleteSql = "DELETE FROM PERSONS_INV a WHERE a.RECID = @recid";
                            const string genresInvDeleteSql = "DELETE FROM GENRES_INV a WHERE a.RECID = @recid";
                            const string companiesInvDeleteSql =
                                "DELETE FROM COMPANIES_INV a WHERE a.RECID = @recid";

                            try
                            {
                                if (audioInfo != null)
                                {
                                    var myCommand = new FbCommand(audioInfoDeleteSql, connection, transaction);
                                    myCommand.Parameters.Add("@siid", audioInfo.StreamInfoid);
                                    var x = myCommand.ExecuteNonQuery();
                                    writer.WriteLine($"Deleted {x} records from AudioInfo");
                                }

                                if (videoInfo != null)
                                {
                                    var myCommand = new FbCommand(videoInfoDeleteSql, connection, transaction);
                                    myCommand.Parameters.Add("@siid", videoInfo.StreamInfoid);
                                    var x = myCommand.ExecuteNonQuery();
                                    writer.WriteLine($"Deleted {x} records from VideoInfo");
                                }

                                if (master != null)
                                {
                                    var myCommand = new FbCommand(masterDeleteSql, connection, transaction);
                                    myCommand.Parameters.Add("@recid", master.RecId);
                                    var x = myCommand.ExecuteNonQuery();
                                    writer.WriteLine($"Deleted {x} records from Master. RecID= {master.RecId}");
                                }


                                {
                                    var myCommand = new FbCommand(instanceDeleteSql, connection, transaction);
                                    myCommand.Parameters.Add("@instId", stream.InstanceId);
                                    var x = myCommand.ExecuteNonQuery();
                                    writer.WriteLine($"Deleted {x} records from Instance");
                                }

                                {
                                    var myCommand = new FbCommand(streamDeleteSql, connection, transaction);
                                    myCommand.Parameters.Add("@streamId", stream.StreamId);
                                    var x = myCommand.ExecuteNonQuery();
                                    writer.WriteLine($"Deleted {x} records from Stream");

                                    myCommand = new FbCommand(mediaDeleteSql, connection, transaction);
                                    myCommand.Parameters.Add("@streamId", stream.StreamId);
                                    x = myCommand.ExecuteNonQuery();
                                    writer.WriteLine($"Deleted {x} records from Media");
                                }

                                if (master != null)
                                {
                                    var notesList = NotesRecords.Where(p => p.ItemId == master.RecId);
                                    foreach (var note in notesList)
                                    {
                                        note.RemoveRecord(connection, transaction, writer);
                                    }
//                                    var myCommand = new FbCommand(notesDeleteSql, connection, transaction);
//                                    myCommand.Parameters.Add("@recid", master.RecId);
//                                    var x = myCommand.ExecuteNonQuery();
//                                    writer.WriteLine($"Deleted {x} records from Notes");

                                    var myCommand = new FbCommand(customPropsInvDeleteSql, connection, transaction);
                                    myCommand.Parameters.Add("@recid", master.RecId);
                                    var x = myCommand.ExecuteNonQuery();
                                    writer.WriteLine($"Deleted {x} records from CustomProps_INV");

                                    myCommand = new FbCommand(keywordsInvDeleteSql, connection, transaction);
                                    myCommand.Parameters.Add("@recid", master.RecId);
                                    x = myCommand.ExecuteNonQuery();
                                    writer.WriteLine($"Deleted {x} records from KeywordsInv");


                                    myCommand = new FbCommand(personsInvDeleteSql, connection, transaction);
                                    myCommand.Parameters.Add("@recid", master.RecId);
                                    x = myCommand.ExecuteNonQuery();
                                    writer.WriteLine($"Deleted {x} records from Persons_Inv");


                                    myCommand = new FbCommand(genresInvDeleteSql, connection, transaction);
                                    myCommand.Parameters.Add("@recid", master.RecId);
                                    x = myCommand.ExecuteNonQuery();
                                    writer.WriteLine($"Deleted {x} records from Genres_Inv");

                                    myCommand = new FbCommand(companiesInvDeleteSql, connection, transaction);
                                    myCommand.Parameters.Add("@recid", master.RecId);
                                    x = myCommand.ExecuteNonQuery();
                                    writer.WriteLine($"Deleted {x} records from Companies_Inv");
                                }

                                transaction.Commit();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Oops 0 {e.Message}");
                                throw;
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Oops  1 {e.Message}");
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Oops 2 {e.Message}");
                    connection.Close();
                    throw;
                }
            }
        }

        public void RemoveEmptyFileRecords()
        {
            var logFile = File.CreateText($"RemoveEmptyFileRecords.log");
            foreach (var stream in StreamRecords)
            {
                if (!string.IsNullOrWhiteSpace(stream.FileName))
                {
                    continue;
                }

                if (stream.InstanceId == null)
                {
                    continue;
                }

                RemoveRecords(stream, logFile);
            }
        }

        public void ConsistencyCheck(string reportName)
        {
            var logFile = File.CreateText($"ConsistencyReport_{reportName}.log");

            logFile.WriteLine(
                "\n\n\n>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Master vs Instance check <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
            foreach (var master in MasterRecords)
            {
                var instCnt = (InstanceRecords.Count(p => p.Recid == master.RecId));
                switch (instCnt)
                {
                    case 0:
                        logFile.WriteLine(
                            $"Missing instance record for RecId {master.RecId} Title [{master.Title}]");
                        continue;
                    case 1:
                        continue;
                    default:
                        logFile.WriteLine(
                            $"Multiple {instCnt} instance record for RecId {master.RecId} Title [{master.Title}]");
                        continue;
                }
            }

            logFile.WriteLine(
                "\n\n\n>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Instance vs Master check <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
            foreach (var inst in InstanceRecords)
            {
                if (MasterRecords.All(p => p.RecId != inst.Recid))
                {
                    logFile.WriteLine(
                        $"Missing master record for RecId {inst.Recid} instance ID {inst.Instanceid}");
                }

                if (StreamRecords.All(p => p.InstanceId != inst.Instanceid))
                {
                    logFile.WriteLine($"Missing stream record for instance ID {inst.Instanceid}");
                }
            }


            logFile.WriteLine(
                "\n\n\n>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Stream vs Instance check <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
            foreach (var stream in StreamRecords)
            {
                if (InstanceRecords.All(p => p.Instanceid != stream.InstanceId))
                {
                    logFile.WriteLine(
                        $"Missing instance record for InstanceId {stream.InstanceId} stream ID {stream.StreamId}. File: {stream.FileName}");
                }

                if (!string.IsNullOrWhiteSpace(stream.FileName)) continue;

                var title = "Unknown";
                var instr = InstanceRecords.FirstOrDefault(p => p.Instanceid == stream.InstanceId);
                if (instr != null)
                {
                    var master = MasterRecords.FirstOrDefault(p => p.RecId == instr.Recid);
                    title = master != null ? master.Title : "HasNoMaster";
                }
                else
                {
                    title = "HasNoInstance";
                }

                logFile.WriteLine(
                    $"FileName is empty for instanceId {stream.InstanceId} stream ID {stream.StreamId}, Title [{title}]");
            }


            logFile.WriteLine(
                "\n\n\n>>>>>>>>>>>>>>>>>>>>>>>>>>>>> StreamInfo vs Instance check <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
            foreach (var stream in StreamRecords)
            {
                if (StreamAudioInfoRecords.All(p => p.StreamInfoid != stream.AudioInfoId))
                {
                    logFile.WriteLine(
                        $"Missing AudioInfo record for InstanceId {stream.InstanceId} stream ID {stream.StreamId}. File: [{stream.FileName}]");
                }

                if (StreamVideoInfoRecords.All(p => p.StreamInfoid != stream.VideoInfoId))
                {
                    logFile.WriteLine(
                        $"Missing VideoInfo record for InstanceId {stream.InstanceId} stream ID {stream.StreamId}. File: [{stream.FileName}]");
                }
            }


            logFile.WriteLine(
                "\n\n\n>>>>>>>>>>>>>>>>>>>>>>>>>>>>> CustomPropsInv vs CustomProps check <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");

            foreach (var customPropInvProp in CustomPropsInvRecords)
            {
                if (CustomPropsRecords.Any(p => p.Id == customPropInvProp.PropId)) continue;

                logFile.WriteLine(
                    $"Missing CustomProp record for custom property with PropId {customPropInvProp.PropId} with value [{customPropInvProp.PropValue}]");
            }

            logFile.WriteLine(
                "\n\n\n>>>>>>>>>>>>>>>>>>>>>>>>>>>>> CustomProps vs Master check <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");

            foreach (var customPropInvProp in CustomPropsInvRecords)
            {
                var prop = CustomPropsRecords.FirstOrDefault(p => p.Id == customPropInvProp.PropId);
                var propName = prop?.Name ?? "Unknown";

                if (MasterRecords.All(p => p.RecId != customPropInvProp.ItemId))
                {
                    logFile.WriteLine(
                        $"Missing master record for custom property {propName} with value [{customPropInvProp.PropValue}]");
                }
            }


            logFile.WriteLine(
                "\n\n\n>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Notes vs Master check <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
            foreach (var note in NotesRecords)
            {
                if (MasterRecords.Any(p => p.RecId == note.ItemId)) continue;

                if (note.TypeId != null)
                    logFile.WriteLine(
                        $"Missing master record for note with RecId {note.ItemId} and TypeID {note.TypeId}, typeName {Notes.TypeName(note.TypeId.Value)}, Text [{note.Text}]");
            }


            logFile.WriteLine(
                "\n\n\n>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Companies vs Master check <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
            foreach (var companies in CompaniesInvRecords)
            {
                if (MasterRecords.Any(p => p.RecId == companies.Recid)) continue;

                logFile.WriteLine(
                    $"Missing master record for company with RecId {companies.Recid} and CompanyId {companies.Companyid}");
            }

            logFile.WriteLine(
                "\n\n\n>>>>>>>>>>>>>>>>>>>>>>>>>>>>> PersonInv vs Master check <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
            foreach (var personsRecord in PersonsInvRecords)
            {
                if (MasterRecords.Any(p => p.RecId == personsRecord.RecId)) continue;

                logFile.WriteLine(
                    $"Missing master record for personInv with RecId {personsRecord.RecId} and CompanyId {personsRecord.PersonId}");
            }

            logFile.Flush();
            logFile.Close();
        }

        private string GetWinFileName(string filePath)
        {
            var res = filePath.Split('\\');
            return res.Last();
        }

//        static public T DeepCopy<T>(T obj)
//        {
//            BinaryFormatter s = new BinaryFormatter();
//            using (MemoryStream ms = new MemoryStream())
//            {
//                s.Serialize(ms, obj);
//                ms.Position = 0;
//                T t = (T)s.Deserialize(ms);
//
//                return t;
//            }
//        }


        public void UpdateMediaToDb(DbManager other)
        {
            int updCount = 0;
            int clipsProcessed = 0;
            var log = File.CreateText("Clips_Not_Found.log");
            var planetaSdInfoId = File.CreateText("PlanetaSD_Clips_With_Wrong_Info_ID.log");
            var planetaHdInfoId = File.CreateText("PlanetaHD_Clips_With_Wrong_Info_ID.log");


            var streamsNotFound = new List<Models.Stream>();


            log.WriteLine("'ClipName'\t'FilePath'");

            foreach (var stream in other.StreamRecords)
            {
                if (string.IsNullOrWhiteSpace(stream.FileName)) continue; //We don't need empty filenames

                var keywordsInv = new List<KeywordsInv>();
                var customPropsInv = new List<CustomPropsInv>();

                clipsProcessed++;
                Console.WriteLine($"Clips To Process {clipsProcessed}/{other.StreamRecords.Count}");

                var record = StreamRecords.FirstOrDefault(p =>
                    string.Equals(GetWinFileName(p.FileName), GetWinFileName(stream.FileName),
                        StringComparison.InvariantCultureIgnoreCase) && !string.Equals(p.FileName, stream.FileName,
                        StringComparison.InvariantCultureIgnoreCase));

                var hasRecord = StreamRecords.Any(p => string.Equals(GetWinFileName(p.FileName),
                    GetWinFileName(stream.FileName), StringComparison.InvariantCultureIgnoreCase));

                if ((record == null) && (!hasRecord))
                {
                    Console.WriteLine($"Media Not Found In SD Channel:  {GetWinFileName(stream.FileName)}");
                    streamsNotFound.Add(stream);


                    var instId = stream.InstanceId;
                    var instance = other.InstanceRecords.FirstOrDefault(p => p.Instanceid == instId);
                    if (instance == null)
                    {
                        log.WriteLine($"'UNKNOWN_INSTANCE'\t'{stream.FileName}'");
                        continue;
                    }

                    var master = other.MasterRecords.FirstOrDefault(p => p.RecId == instance.Recid);
                    if (master == null)
                    {
                        log.WriteLine($"'UNKNOWN_MASTER'\t'{stream.FileName}'");
                        continue;
                    }

                    log.WriteLine($"'{master.Title}'\t'{stream.FileName}'");
                    continue;
                }

                if (record == null) continue;


                if ((record.AudioInfoId == 0) || (record.VideoInfoId == 0))
                {
                    var instId = record.InstanceId;
                    var instance = InstanceRecords.FirstOrDefault(p => p.Instanceid == instId);
                    if (instance == null)
                    {
                        Console.WriteLine($"'UNKNOWN_INSTANCE'\t'{stream.FileName}'");
                        continue;
                    }

                    var master = MasterRecords.FirstOrDefault(p => p.RecId == instance.Recid);
                    if (master == null)
                    {
                        Console.WriteLine($"'UNKNOWN_MASTER'\t'{stream.FileName}'");
                        continue;
                    }

                    planetaSdInfoId.WriteLine($"'{master.Title}'\t'{record.FileName}'");
                    continue;
                }


                var localInstance = InstanceRecords.FirstOrDefault(p => p.Instanceid == record.InstanceId);
                var otherInstance = other.InstanceRecords.FirstOrDefault(p => p.Instanceid == stream.InstanceId);

                if ((localInstance == null) || (otherInstance == null))
                {
                    Console.WriteLine("Instances failed");
                    continue;
                }

                localInstance.Duration = otherInstance.Duration;


//                if ((stream.AudioInfoId == 0) || (stream.VideoInfoId == 0))
//                {
//                    var instId = stream.InstanceId;
//                    var instance = other.InstanceRecords.FirstOrDefault(p => p.Instanceid == instId);
//                    if (instance == null)
//                    {
//                        Console.WriteLine($"'UNKNOWN_INSTANCE'\t'{stream.FileName}'");
//                        continue;
//                    }
//
//                    var master = other.MasterRecords.FirstOrDefault(p => p.RecId == instance.Recid);
//                    if (master == null)
//                    {
//                        Console.WriteLine($"'UNKNOWN_MASTER'\t'{stream.FileName}'");
//                        continue;
//                    }
//
//                    planetaHDInfoID.WriteLine($"'{master.Title}'\t'{stream.FileName}'");
//                    // continue;
//                }


                var localStreamAudio =
                    StreamAudioInfoRecords.FirstOrDefault(p => p.StreamInfoid == record.AudioInfoId);
                var otherStreamAudio =
                    other.StreamAudioInfoRecords.FirstOrDefault(p => p.StreamInfoid == stream.AudioInfoId);

                if ((localStreamAudio != null) && (otherStreamAudio != null))
                {
                    localStreamAudio.AudioChannels = otherStreamAudio.AudioChannels;
                    localStreamAudio.AudioBitRate = otherStreamAudio.AudioBitRate;
                    localStreamAudio.AudioSampleRate = otherStreamAudio.AudioSampleRate;
                    localStreamAudio.Act = otherStreamAudio.Act;
                }

                var localStreamVideo =
                    StreamVideoInfoRecords.FirstOrDefault(p => p.StreamInfoid == record.VideoInfoId);
                var otherStreamVideo =
                    other.StreamVideoInfoRecords.FirstOrDefault(p => p.StreamInfoid == stream.VideoInfoId);

                if ((localStreamVideo != null) && (otherStreamVideo != null))
                {
                    localStreamVideo.Height = otherStreamVideo.Height;
                    localStreamVideo.Width = otherStreamVideo.Width;
                    localStreamVideo.VideoBitRate = otherStreamVideo.VideoBitRate;
                    localStreamVideo.FrameRate = otherStreamVideo.FrameRate;
                    localStreamVideo.Vct = otherStreamVideo.Vct;
                }


                var otherKeywordInv = other.KeywordsInvRecords.Where(p => p.RecId == otherInstance.Recid).ToList();
                var localKeywordInv = KeywordsInvRecords.Where(p => p.RecId == localInstance.Recid).ToList();


                foreach (var keywordInv in otherKeywordInv)
                {
                    var keywdDef = other.KeywordsRecords.FirstOrDefault(p => p.KeywordId == keywordInv.KeywordId);
                    if (keywdDef == null)
                    {
                        Console.WriteLine($"Can not find Keyword definition for keywordId {keywordInv.KeywordId}");
                        continue;
                    }

                    var lkwdef = KeywordsRecords.FirstOrDefault(p =>
                        string.Equals(p.KeywordName, keywdDef.KeywordName, StringComparison.CurrentCultureIgnoreCase));
                    if (lkwdef == null)
                    {
                        Console.WriteLine(
                            $"Can not find Local Keyword definition for keywordId {keywordInv.KeywordId}");
                        continue;
                    }

                    var key = new KeywordsInv
                    {
                        KeywordId = lkwdef.KeywordId,
                        RecId = localInstance.Recid
                    };

                    keywordsInv.Add(key);
                }


                // Our Result DB Does not have custom props
                //var localPropsInv = CustomPropsInvRecords.Where(p => p.ItemId == localInstance.Recid);
                var otherPropsInv = other.CustomPropsInvRecords.Where(p => p.ItemId == otherInstance.Recid);


                foreach (var propsInv in otherPropsInv)
                {
                    var propDef = other.CustomPropsRecords.FirstOrDefault(p => p.Id == propsInv.PropId);

                    if (propDef == null)
                    {
                        Console.WriteLine(
                            $"Something is wrong... other Does not contain prop with id {propsInv.PropId}");
                        continue;
                    }

                    var propDefL = CustomPropsRecords.FirstOrDefault(p => p.Name == propDef.Name);

                    if (propDefL == null)
                    {
                        Console.WriteLine(
                            $"Something is wrong... local Does not contain prop with name {propDef.Name}");
                        continue;
                    }

                    propsInv.PropId = propDefL.Id;
                    propsInv.ItemId = localInstance.Recid;
                    propsInv.ItemType = 0;


                    customPropsInv.Add(propsInv);
                }


                record.FileName = stream.FileName;
                record.FileSize = stream.FileSize;
                record.InP = stream.InP;
                record.OutP = stream.OutP;


                using (var connection = new FbConnection(_connectionString))
                {
                    try
                    {
                        connection.Open();
                        using (var transaction = connection.BeginTransaction())
                        {
                            try
                            {
                                foreach (var key in keywordsInv)
                                {
                                    const string sql =
                                        "INSERT INTO KEYWORDS_INV (RECID, KEYWORDID) VALUES (@recId,@keywordId);";

                                    var myCommand = new FbCommand(sql, connection, transaction);
                                    myCommand.Parameters.Add("@recId", key.RecId);
                                    myCommand.Parameters.Add("@keywordId", key.KeywordId);
                                    myCommand.ExecuteNonQuery();
                                }

                                foreach (var prop in customPropsInv)
                                {
                                    var sql =
                                        $"INSERT INTO CUSTOM_PROPS_INV (PROP_ID, ITEM_ID, ITEM_TYPE, PROP_VALUE) VALUES (@PropId,@ItemId,@ItemType,'{prop.PropValue.Replace("'", "''")}');";

                                    var myCommand = new FbCommand(sql, connection, transaction);
                                    myCommand.Parameters.Add("@PropId", prop.PropId.Value);
                                    myCommand.Parameters.Add("@ItemId", prop.ItemId.Value);
                                    myCommand.Parameters.Add("@ItemType", prop.ItemType.Value);


                                    var x = myCommand.ExecuteNonQuery();
                                }


                                {
                                    var updateLocalInstance = new FbCommand
                                    {
                                        CommandText =
                                            $"UPDATE INSTANCE a SET a.DURATION = '{localInstance.Duration}'  WHERE a.INSTANCEID = '{localInstance.Instanceid}';",
                                        Connection = connection,
                                        Transaction = transaction
                                    };

                                    var y = updateLocalInstance.ExecuteNonQuery();
                                }

                                if ((localStreamVideo != null) && (otherStreamVideo != null))
                                {
                                    var updateLocalVideoInfo = new FbCommand
                                    {
                                        CommandText = $"UPDATE STREAM_VIDEO_INFO a " +
                                                      $"SET a.V_C_T = '{localStreamVideo.Vct}', a.WIDTH = '{localStreamVideo.Width}', a.HEIGHT = '{localStreamVideo.Height}', " +
                                                      $"a.FRAME_RATE = '{localStreamVideo.FrameRate}'," +
                                                      $" a.VIDEO_BIT_RATE = '{localStreamVideo.VideoBitRate}' WHERE  a.STREAM_INFOID = '{localStreamVideo.StreamInfoid}';",
                                        Connection = connection,
                                        Transaction = transaction
                                    };

                                    var y = updateLocalVideoInfo.ExecuteNonQuery();
                                }

                                if ((localStreamAudio != null) && (otherStreamAudio != null))
                                {
                                    var updateLocalAudioInfo = new FbCommand
                                    {
                                        CommandText = $"UPDATE STREAM_AUDIO_INFO a SET " +
                                                      $"a.A_C_T = '{localStreamAudio.Act}', a.AUDIO_SAMPLE_RATE = '{localStreamAudio.AudioSampleRate}', a.AUDIO_BIT_RATE = '{localStreamAudio.AudioBitRate}'," +
                                                      $"a.AUDIO_CHANNELS = '{localStreamAudio.AudioChannels}' WHERE a.STREAM_INFOID = '{localStreamAudio.StreamInfoid}';",
                                        Connection = connection,
                                        Transaction = transaction
                                    };

                                    var y = updateLocalAudioInfo.ExecuteNonQuery();
                                }


                                {
                                    var updateStream = new FbCommand
                                    {
                                        CommandText = "UPDATE STREAM a SET " +
                                                      $"a.INSTANCEID = '{record.InstanceId}'," +
                                                      $"a.AUDIO_LEVEL = '{record.AudioLevel}'," +
                                                      $"a.FILE_NAME = '{record.FileName}', " +
                                                      $"a.STREAM_NAME = '{record.StreamName}'," +
                                                      $"a.STREAM_TYPE = '{record.StreamType}', " +
                                                      $"a.VIDEO_INFOID = '{record.VideoInfoId}', " +
                                                      $"a.AUDIO_INFOID = '{record.AudioInfoId}', " +
                                                      $"a.IN_P = '{record.InP}',  " +
                                                      $"a.OUT_P = '{record.OutP}',  " +
                                                      $"a.LANGUAGEID = '{record.LanguageId}', " +
                                                      $"a.MAIN = '{record.Main}', " +
                                                      $"a.STATUS = '{record.Status}', " +
                                                      $"a.PART = '{record.Part}', " +
                                                      $"a.FILE_SIZE = '{record.FileSize}' " +
                                                      $"WHERE " +
                                                      $"a.STREAMID = '{record.StreamId}';",
                                        Connection = connection,
                                        Transaction = transaction
                                    };

                                    var y = updateStream.ExecuteNonQuery();
                                }

                                transaction.Commit();
                                updCount++;
                            }
                            catch (Exception exception)
                            {
                                transaction.Rollback();
                                Console.WriteLine($"Exception {exception.Message}");
                                throw;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Exception {e.Message}");
                        throw;
                    }


                    connection.Close();
                }
            }


            Console.WriteLine($"Clips updated {updCount}");

            log.Flush();
            log.Close();

            planetaHdInfoId.Flush();
            planetaHdInfoId.Close();
            planetaSdInfoId.Flush();
            planetaSdInfoId.Close();
            ReadDatabase();
        }


        public void UpgradeCustomProps(DbManager other)
        {
            var list = new List<CustomProps>();
            foreach (var tp in other.CustomPropsRecords)
            {
                if (CustomPropsRecords.Any(p => p.Name == tp.Name)) continue;

                var prop = new CustomProps
                {
                    Color = tp.Color,
                    Id = 0,
                    Name = tp.Name,
                    Res = tp.Res,
                    ParentId = tp.ParentId,
                    PropType = tp.PropType
                };


                list.Add(prop);
            }


            if (string.IsNullOrWhiteSpace(_connectionString)) return;
            using (var connection = new FbConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            foreach (var prop in list)
                            {
                                var myCommand = new FbCommand
                                {
                                    CommandText =
                                        "INSERT INTO CUSTOM_PROPS (PARENT_ID, NAME, PROP_TYPE, RES, COLOR)" +
                                        $" VALUES ('{prop.ParentId}','{prop.Name.Replace("'", "''")}','{prop.PropType}','{prop.Res}','{prop.Color}');",
                                    Connection = connection,
                                    Transaction = transaction
                                };
                                var x = myCommand.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            CustomPropsRecords = CustomProps.ReadTable(connection);
                        }
                        catch (Exception e)
                        {
                            transaction.Rollback();
                            Console.WriteLine(e);
                            throw;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exception {e.Message}");
                    connection.Close();
                    throw;
                }
            }
        }


        public void UpgradeKeywords(DbManager other)
        {
            var list = new List<Keywords>();
            foreach (var tp in other.KeywordsRecords)
            {
                if (KeywordsRecords.Any(p =>
                    string.Equals(p.KeywordName, tp.KeywordName, StringComparison.InvariantCultureIgnoreCase)))
                    continue;

                var prop = new Keywords
                {
                    Color = tp.Color,
                    KeywordId = 0,
                    KeywordName = tp.KeywordName
                };


                list.Add(prop);
            }


            if (string.IsNullOrWhiteSpace(_connectionString)) return;
            using (var connection = new FbConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    foreach (var prop in list)
                    {
                        prop.KeywordName = prop.KeywordName.Replace("'", "''");
                        var insertKeyword =
                            $"INSERT INTO KEYWORDS (KEYWORD_NAME, COLOR) VALUES ('{prop.KeywordName}', @color);";
                        var myCommand = new FbCommand(insertKeyword, connection, transaction);


                        myCommand.Parameters.Add("color", prop.Color);

                        Console.WriteLine($"Execute Before {prop.KeywordName}");
                        var x = myCommand.ExecuteNonQuery();
                        Console.WriteLine($"Execute After {prop.KeywordName}");
                    }


                    Console.WriteLine($"Transaction before");
                    transaction.Commit();
                    Console.WriteLine($"Transaction after");
                    KeywordsRecords = Keywords.ReadTable(connection);
                }
            }
        }


        public void SetMediaStoragePool(string pool)
        {
            if (string.IsNullOrWhiteSpace(_connectionString)) return;
            using (var connection = new FbConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        var myCommand = new FbCommand
                        {
                            CommandText = $"UPDATE MEDIA a SET a.MEDIA_NAME = 'Server', a.POOL = '{pool}';",
                            Connection = connection,
                            Transaction = transaction
                        };
                        try
                        {
                            var x = myCommand.ExecuteNonQuery();
                            transaction.Commit();
                            ReadDatabase();
                        }
                        catch (Exception e)
                        {
                            transaction.Rollback();
                            Console.WriteLine(e);
                            throw;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exception {e.Message}");
                    connection.Close();
                    throw;
                }
            }
        }

        public void ReadDatabase()
        {
            if (string.IsNullOrWhiteSpace(_connectionString)) return;

            try
            {
                using (var connection = new FbConnection(_connectionString))
                {
                    try
                    {
                        connection.Open();
                        AgeRatesRecords = AgeRates.ReadTable(connection);
                        AnnotationsRecords = Annotations.ReadTable(connection);
                        ArchiveRecords = Archive.ReadTable(connection);
                        CategoriesRecords = Categories.ReadTable(connection);
                        CompaniesRecords = Companies.ReadTable(connection);
                        CompaniesInvRecords = CompaniesInv.ReadTable(connection);
                        CompanyActivityRecords = CompanyActivity.ReadTable(connection);
                        CountriesRecords = Countries.ReadTable(connection);
                        CountriesInvRecords = CountriesInv.ReadTable(connection);
                        CountryActivityRecords = CountryActivity.ReadTable(connection);
                        CustomPropsRecords = CustomProps.ReadTable(connection);
                        CustomPropsInvRecords = CustomPropsInv.ReadTable(connection);
                        DayMaskRecords = DayMask.ReadTable(connection);
                        EditItemsRecords = EditItems.ReadTable(connection);
                        GenresRecords = Genres.ReadTable(connection);
                        GenresInvRecords = GenresInv.ReadTable(connection);
                        GroupsRecords = Groups.ReadTable(connection);
                        InstanceRecords = Instance.ReadTable(connection);
                        KeywordsRecords = Keywords.ReadTable(connection);
                        KeywordsInvRecords = KeywordsInv.ReadTable(connection);
                        LanguagesRecords = Languages.ReadTable(connection);
                        LanguagesInvRecords = LanguagesInv.ReadTable(connection);
                        MasterRecords = Master.ReadTable(connection);
                        MediaRecords = Media.ReadTable(connection);
                        MediaTypeRecords = MediaType.ReadTable(connection);
                        NotesRecords = Notes.ReadTable(connection, false);
                        PersonsRecords = Persons.ReadTable(connection);
                        PersonsInvRecords = PersonsInv.ReadTable(connection);
                        PositionsRecords = Positions.ReadTable(connection);
                        QualityRecords = Quality.ReadTable(connection);
                        SequencesRecords = Sequences.ReadTable(connection);
                        SkipZonesRecords = SkipZones.ReadTable(connection);
                        StreamRecords = Models.Stream.ReadTable(connection);
                        StreamAudioInfoRecords = StreamAudioInfo.ReadTable(connection);
                        StreamVideoInfoRecords = StreamVideoInfo.ReadTable(connection);
                        TypesRecords = Types.ReadTable(connection);
                        UsersRecords = Users.ReadTable(connection);
                        UserGroupsRecords = UserGroups.ReadTable(connection);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        connection.Close();
                        throw;
                    }

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void CheckClipsInverse(DbManager other)
        {
            var log = File.CreateText("Clips_From_SD_Not_Found_in_HD.log");
            log.WriteLine("ClipName\tFilePath");
            foreach (var stream in StreamRecords)
            {
                var record = other.StreamRecords.FirstOrDefault(p =>
                    string.Equals(GetWinFileName(p.FileName), GetWinFileName(stream.FileName),
                        StringComparison.InvariantCultureIgnoreCase));


                if (record != null) continue;


                var instance = InstanceRecords.FirstOrDefault(p => p.Instanceid == stream.InstanceId);

                if (instance == null)
                {
                    Console.WriteLine($"Stream {stream.FileName}, not found in HD Channel. Local Not Have instance");
                    continue;
                }

                var master = MasterRecords.FirstOrDefault(p => instance.Recid == p.RecId);

                if (master == null)
                {
                    Console.WriteLine($"Stream {stream.FileName}, not found in HD Channel. Local Not Have Master");
                    continue;
                }

                log.WriteLine($"'{master.Title}'\t'{stream.FileName}'");
                //Console.WriteLine($"Stream {stream.FileName}, not found in HD Channel. Title [{master.Title}]");
            }
            log.Flush();
            log.Close();
        }

        public void TryFixFilePaths(string newServer, string newDir)
        {
            foreach (var stream in StreamRecords)
            {
                if (stream.FileName.Substring(0,2)!="\\\\")
                {
                    Console.WriteLine("Failed");
                }

                var posStart = stream.FileName.IndexOf("\\", 0);
                var posEnd = stream.FileName.IndexOf("\\", 2);
                var server = stream.FileName.Substring(posStart+2, posEnd-(posStart + 2));
                if (server != newServer)
                {
                    stream.FileName = stream.FileName.Replace(server, newServer);
                }

                var start = stream.FileName.ToUpper().IndexOf("\\PLANETA TV\\", 0);
                if (start != -1)
                {
                    var endF = start + ("\\PLANETA TV\\").Length;
                    var endFN = stream.FileName.Substring(endF);
                    stream.FileName = stream.FileName.Substring(0, start) + "\\" + newDir + "\\" + endFN;   
                }

                if (!File.Exists(stream.FileName))
                {
                    Console.WriteLine(stream.FileName + " not exist");
                }
                Console.WriteLine(stream.FileName);
            }
        }
    }
}