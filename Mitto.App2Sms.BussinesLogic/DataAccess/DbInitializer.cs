using Microsoft.Extensions.Configuration;
using Mitto.App2Sms.BussinesLogic.DataAccess.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mitto.App2Sms.BussinesLogic.DataAccess
{
    public class DBManager
    {
        static string serverConnString = string.Empty;

        public static void InitializeDb(IDbConnectionFactory dbFactory)
        {
            List<Country> seedCountries = new List<Country>()
            {
                new Country()
                {
                    Name = "Germany",   
                    Mcc = "262",
                    Cc = "49",
                    PricePerSms = 0.055M
                },
                new Country()
                {
                    Name = "Austria",
                    Mcc = "232",
                    Cc = "43",
                    PricePerSms = 0.053M
                },
                new Country()
                {
                    Name = "Poland",
                    Mcc = "260",
                    Cc = "48",
                    PricePerSms = 0.032M
                },
            };

            using (var db = dbFactory.OpenDbConnection())
            {
                if (db.CreateTableIfNotExists<Country>())
                {
                    db.InsertAll<Country>(seedCountries);
                }

                db.CreateTableIfNotExists<Sms>();
            }
        }

        public static void CreateDb()
        {
            IDbConnectionFactory dbFactory = new OrmLiteConnectionFactory(serverConnString, MySqlDialect.Provider);

            using (var db = dbFactory.OpenDbConnection())
            {
                db.ExecuteNonQuery("CREATE DATABASE IF NOT EXISTS app2sms");  
            }
        }

        public static string GetDBConnString(IConfiguration config)
        {
            string host = config["DBHOST"] ?? "localhost";
            string port = config["DBPORT"] ?? "3306";
            string password = config["DBPASSWORD"] ?? "secret";

            serverConnString = $"server={host};port={port};user id=root;password={password}";
            string dbConnString = $"{serverConnString};database=app2sms";

            return dbConnString;
        }
    }
}
