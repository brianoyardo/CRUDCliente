using Microsoft.Extensions.Configuration;
using System;
using Microsoft.Data.SqlClient;
using System.IO;

namespace WebApi.Data
{
    public class GlobalesData
    {
        public static SqlConnection CreateDatabase()
        {
            SqlConnection con = new SqlConnection();
            try
            {
                IConfiguration config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                string cadCon = config.GetConnectionString("conex");
                con = new SqlConnection(cadCon);

                return con;
            }
            catch (Exception ex)
            {
                con.Close();
                con.Dispose();
                throw ex;
            }
        }
    }
}
