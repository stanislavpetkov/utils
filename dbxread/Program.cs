using System;
using System.Collections.Generic;
using System.Text;
using dbxread.Models;
using FirebirdSql.Data.FirebirdClient;

namespace dbxread
{

    class Program
    {
        
        


        public static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider
                .Instance); //This fixes missing win1251... depends on System.Text.Encoding.CodePages... nuget it

            // Set the ServerType to 1 for connect to the embedded server
            const string connectionString = "User=SYSDBA;" +
                                            "Password=masterkey;" +
                                            "Database=databox_hd.gdb;" +
                                            "DataSource=127.0.0.1;" +
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


            var myConnection1 = new FbConnection(connectionString);

            try
            {
                myConnection1.Open();
                var ageRatesRecords = AgeRates.ReadTable(ref myConnection1);
                var annotationsRecords = Annotations.ReadTable(ref myConnection1);
                var archiveRecords = Archive.ReadTable(ref myConnection1);
                var categoriesRecords = Categories.ReadTable(ref myConnection1);
                var companiesRecords = Companies.ReadTable(ref myConnection1);
                var companiesInvRecords = CompaniesInv.ReadTable(ref myConnection1);
                var companyActivityRecords = CompanyActivity.ReadTable(ref myConnection1);
                var countriesRecords = Countries.ReadTable(ref myConnection1);
                var countriesInvRecords = CountriesInv.ReadTable(ref myConnection1);
                var countryActivityRecords = CountryActivity.ReadTable(ref myConnection1);
                var customPropsRecords = CustomProps.ReadTable(ref myConnection1);
                var customPropsInvRecords = CustomPropsInv.ReadTable(ref myConnection1);
                var dayMaskRecords = DayMask.ReadTable(ref myConnection1);
                var editItemsRecords = EditItems.ReadTable(ref myConnection1);
                var genresRecords = Genres.ReadTable(ref myConnection1);
                var genresInvRecords = GenresInv.ReadTable(ref myConnection1);
                var groupsRecords = Groups.ReadTable(ref myConnection1);
                var instanceRecords = Instance.ReadTable(ref myConnection1);
                var keywordsRecords = Keywords.ReadTable(ref myConnection1);
                var keywordsInvRecords = KeywordsInv.ReadTable(ref myConnection1);
                var languagesRecords = Languages.ReadTable(ref myConnection1);
                var languagesInvRecords = LanguagesInv.ReadTable(ref myConnection1);
                var masterRecords = Master.ReadTable(ref myConnection1);
                var mediaRecords = Media.ReadTable(ref myConnection1);
                var mediaTypeRecords = MediaType.ReadTable(ref myConnection1);
                var notesRecords = Notes.ReadTable(ref myConnection1);
                var personsRecords = Persons.ReadTable(ref myConnection1);
                var personsInvRecords = PersonsInv.ReadTable(ref myConnection1);
                var positionsRecords = Positions.ReadTable(ref myConnection1);
                var qualityRecords = Quality.ReadTable(ref myConnection1);
                var sequencesRecords = Sequences.ReadTable(ref myConnection1);
                var skipZonesRecords = SkipZones.ReadTable(ref myConnection1);
                var streamRecords = Stream.ReadTable(ref myConnection1);
                var streamAudioInfoRecords = StreamAudioInfo.ReadTable(ref myConnection1);
                var streamVideoInfoRecords = StreamVideoInfo.ReadTable(ref myConnection1);
                var typesRecords = Types.ReadTable(ref myConnection1);
                var usersRecords = Users.ReadTable(ref myConnection1);
                var userGroupsRecords = UserGroups.ReadTable(ref myConnection1);
                


                

//                foreach (var mr in masterRecords)
//                {
//                    mr.Instances.AddRange(instanceRecords.Where(p=>p.Recid == mr.Recid));
//                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            Console.WriteLine("Hello World!");
        }
    }
}