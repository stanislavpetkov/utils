using System;
using System.Text;

namespace DbxRead
{
    class Program
    {
        //ALTER CHARACTER SET WIN1251 SET DEFAULT collation WIN1251;
        private const string ConnectionStringHd = "User=SYSDBA;" +
                                          "Password=masterkey;" +
                                          "Database=zbook:databox_hd.gdb;" +
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
                                                     "Database=zbook:databox_result.gdb;" +
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
                                               "Database=zbook:databox_planeta_16x9.gdb;" +
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
            Encoding.RegisterProvider(CodePagesEncodingProvider
                .Instance); //This fixes missing win1251... depends on System.Text.Encoding.CodePages... nuget it

            // Set the ServerType to 1 for connect to the embedded server

            var startTime = DateTime.Now;
            Console.WriteLine($"Start Time {startTime}");
            var hDbManager = new DBManager.DbManager(ConnectionStringHd);
            //var folkManager = new DBManager.DBManager(connectionStringPlanetaFolk);
            var resultManager = new DBManager.DbManager(ConnectionStringResult);
            var planetaManager = new DBManager.DbManager(ConnectionStringSd);

            try
            {
                hDbManager.ReadDatabase();
                //folkManager.ReadDatabase();
                planetaManager.ReadDatabase();
                resultManager.ReadDatabase();

                if (true)
                {
                    const string poolStorage = @"\\StorageHD\Planeta HD\";

                    resultManager.SetMediaStoragePool(poolStorage);
                    resultManager.UpgradeCustomProps(hDbManager);
                    resultManager.UpgradeKeywords(hDbManager);
                    resultManager.UpdateMediaToDb(hDbManager);
                    resultManager.ReadDatabase();
                    resultManager.ConsistencyCheck("BeforeRemoveFile");
                    resultManager.RemoveEmptyFileRecords();
                    resultManager.ReadDatabase();
                    resultManager.RemoveNotes();
                    resultManager.ReadDatabase();
                }

                
                
                
                
                resultManager.ConsistencyCheck("AfterRemoveFile");

                resultManager.CheckClipsInverse(hDbManager);
                //planetaManager.ConsistencyCheck("PlanetaDB");
                //folkManager.ConsistencyCheck("FolkDB");




                //resultManager.UpgradeAgeRates(hDbManager);
                //resultManager.UpgradeAgeRates(folkManager);

                //resultManager.UpgradeTypes(hDbManager);
                //resultManager.UpgradeTypes(folkManager);

                //
                //Update Media->Pool
                //Update Stream-> without StreamID
                //Update StreamAudioInfo->without StreamInfoid
                //Update StreamVideoInfo->without StreamInfoId


//We have all records in memory
//Let's panic :)
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            var endTime = DateTime.Now;
            Console.WriteLine($"End Time {endTime} ProcessingTime {(endTime-startTime).TotalSeconds}");
            Console.WriteLine("Done");
        }
    }
}