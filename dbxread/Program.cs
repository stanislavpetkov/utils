using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dbxread.Models;
using FirebirdSql.Data.FirebirdClient;

namespace dbxread
{
    class Program
    {
        const string connectionStringHD = "User=SYSDBA;" +
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

        const string connectionStringPlaneta = "User=SYSDBA;" +
                                               "Password=masterkey;" +
                                               "Database=databox_planeta_16x9.gdb;" +
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

        const string connectionStringPlanetaFolk = "User=SYSDBA;" +
                                                   "Password=masterkey;" +
                                                   "Database=databox_folk_16x9.gdb;" +
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


        public static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider
                .Instance); //This fixes missing win1251... depends on System.Text.Encoding.CodePages... nuget it

            // Set the ServerType to 1 for connect to the embedded server


            var hDbManager = new DBManager.DBManager(connectionStringHD);
            var folkManager = new DBManager.DBManager(connectionStringPlanetaFolk);
            var resultManager = new DBManager.DBManager(connectionStringPlaneta);

            try
            {
                hDbManager.ReadDatabase();
                folkManager.ReadDatabase();
                resultManager.ReadDatabase();


                resultManager.UpgradeAgeRates(hDbManager);
                resultManager.UpgradeAgeRates(folkManager);

                resultManager.UpgradeTypes(hDbManager);
                resultManager.UpgradeTypes(folkManager);

                
                
                
                
                
                
                
                
                


//We have all records in memory
//Let's panic :)
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            Console.WriteLine("Done");
        }
    }
}