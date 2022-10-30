using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;

namespace AzureFunction
{
    public static class SearchProductByUnitPrice
    {
        //give meaningfull name
        [FunctionName("SearchProductByUnitPrice")]
        public static string Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            //declare variable for result
            string searchResult = ""; 
            //declare connection string to database
            string connectionStr = "Data Source=TATIANALENOVO\\SQL_SERVER;Initial Catalog=CloudPractise;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            //declare search parameter
            string param = req.Query["param"];

            //create connection
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionStr))
                {
                    //open connection
                    sqlConnection.Open();
                    //declare querry
                    var sqlQuerry = "select * from Products where UnitPrice >" + param;
                    var sqlCommand = new SqlCommand(sqlQuerry, sqlConnection);

                    //check if there any other open connection and close it
                    if(sqlCommand.Connection.State==System.Data.ConnectionState.Open)
                    {
                        sqlCommand.Connection.Close();
                    }

                    //open connection

                    sqlCommand.Connection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    //declare result
                    var datatableProducts = new DataTable();
                    datatableProducts.Load(sqlDataReader);
                    //pass data as Json object
                    searchResult = JsonConvert.SerializeObject(datatableProducts);
                }
            }
            catch(Exception e)
            {
                searchResult = e.Message;
            }
            return searchResult;

        }
        
    }
}


//http://localhost:7149/api/SearchProductByUnitPrice