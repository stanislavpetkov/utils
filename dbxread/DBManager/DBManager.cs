using System;
using System.Collections.Generic;
using System.Linq;
using dbxread.Models;
using FirebirdSql.Data.FirebirdClient;

namespace dbxread.DBManager
{
    public class DBManager
    {
        private readonly string _connectionString;
        public List<AgeRates> AgeRatesRecords { get; set; }
        public List<Annotations> AnnotationsRecords { get; set; }
        public List<Archive> ArchiveRecords { get; set; }
        public List<Categories> CategoriesRecords { get; set; }
        public List<Companies> CompaniesRecords { get; set; }
        public List<CompaniesInv> CompaniesInvRecords { get; set; }
        public List<CompanyActivity> CompanyActivityRecords { get; set; }
        public List<Countries> CountriesRecords { get; set; }
        public List<CountriesInv> CountriesInvRecords { get; set; }
        public List<CountryActivity> CountryActivityRecords { get; set; }
        public List<CustomProps> CustomPropsRecords { get; set; }
        public List<CustomPropsInv> CustomPropsInvRecords { get; set; }
        public List<DayMask> DayMaskRecords { get; set; }
        public List<EditItems> EditItemsRecords { get; set; }
        public List<Genres> GenresRecords { get; set; }
        public List<GenresInv> GenresInvRecords { get; set; }
        public List<Groups> GroupsRecords { get; set; }
        public List<Instance> InstanceRecords { get; set; }
        public List<Keywords> KeywordsRecords { get; set; }
        public List<KeywordsInv> KeywordsInvRecords { get; set; }
        public List<Languages> LanguagesRecords { get; set; }
        public List<LanguagesInv> LanguagesInvRecords { get; set; }
        public List<Master> MasterRecords { get; set; }
        public List<Media> MediaRecords { get; set; }
        public List<MediaType> MediaTypeRecords { get; set; }
        public List<Notes> NotesRecords { get; set; }
        public List<Persons> PersonsRecords { get; set; }
        public List<PersonsInv> PersonsInvRecords { get; set; }
        public List<Positions> PositionsRecords { get; set; }
        public List<Quality> QualityRecords { get; set; }
        public List<Sequences> SequencesRecords { get; set; }
        public List<SkipZones> SkipZonesRecords { get; set; }
        public List<Stream> StreamRecords { get; set; }
        public List<StreamAudioInfo> StreamAudioInfoRecords { get; set; }
        public List<StreamVideoInfo> StreamVideoInfoRecords { get; set; }
        public List<Types> TypesRecords { get; set; }
        public List<Users> UsersRecords { get; set; }
        public List<UserGroups> UserGroupsRecords { get; set; }

        public DBManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DBManager()
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
         StreamRecords = new List<Stream>();
         StreamAudioInfoRecords = new List<StreamAudioInfo>();
         StreamVideoInfoRecords = new List<StreamVideoInfo>();
         TypesRecords = new List<Types>();
         UsersRecords = new List<Users>();
         UserGroupsRecords = new List<UserGroups>();
            
        }

        public void UpgradeAgeRates(DBManager other)
        {
            foreach (var ageRates in other.AgeRatesRecords)
            {
                if (AgeRatesRecords.Any(p => p.AgeRateName == ageRates.AgeRateName)) continue;
                    
                var tpNew = new AgeRates(ageRates) {AgeRateId = AgeRatesRecords.Max(p => p.AgeRateId) + 1};
                AgeRatesRecords.Add(tpNew);
            }
        }
        
        public void UpgradeTypes(DBManager other)
        {
            foreach (var tp in other.TypesRecords)
            {
                if (TypesRecords.Any(p => p.TypeName == tp.TypeName)) continue;
                    
                //we don't have this record
                var tpNew = new Types(tp) {TypeId = TypesRecords.Max(p => p.TypeId) + 1};
                TypesRecords.Add(tpNew);
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
                        NotesRecords = Notes.ReadTable(connection);
                        PersonsRecords = Persons.ReadTable(connection);
                        PersonsInvRecords = PersonsInv.ReadTable(connection);
                        PositionsRecords = Positions.ReadTable(connection);
                        QualityRecords = Quality.ReadTable(connection);
                        SequencesRecords = Sequences.ReadTable(connection);
                        SkipZonesRecords = SkipZones.ReadTable(connection);
                        StreamRecords = Stream.ReadTable(connection);
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
    }
}