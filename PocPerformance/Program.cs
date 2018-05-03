using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocPerformance
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Int32> lstID = new List<int>();
            DateTime startTime, endTime;
            int countSQLConnection = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnection"].ConnectionString))
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand("SELECT TOP 20000 CustomerID FROM [SalesLT].[Customer] WITH (NOLOCK) ORDER BY rowguid", conn);

                    //countSQLConnection++;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lstID.Add(Convert.ToInt32(reader[0].ToString()));
                        }

                    }

                    startTime = DateTime.Now;

                    foreach (int ID in lstID)
                    {
                        command = new SqlCommand(@"SELECT [CustomerID]
                              , ([Title] + ' ' + [FirstName] + ' ' + [MiddleName] + ' ' + [LastName]) AS[Name]
                              ,[CompanyName]
                              ,[SalesPerson]
                              ,[EmailAddress]
                              ,[Phone]
                        FROM[SalesLT].[Customer] WHERE CustomerID = @0", conn);
                        command.Parameters.Add(new SqlParameter("0", ID));

                        countSQLConnection++;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            Console.WriteLine("CustomerID\tName\t\tCompanyName\t\tSalesPerson\t\tEmailAddress\t\tPhone");
                            while (reader.Read())
                            {
                                Console.WriteLine(String.Format("{0} \t | {1} \t | {2} \t | {3} \t | {4} \t | {5}",
                                    reader[0], reader[1], reader[2], reader[3], reader[4], reader[5]));
                            }
                        }

                    }

                    endTime = DateTime.Now;

                    TimeSpan span = endTime.Subtract(startTime);
                    Console.WriteLine("Time Difference (seconds): " + span.TotalSeconds);
                    Console.WriteLine("Count SQL Connections: " + countSQLConnection.ToString());
                    Console.WriteLine("Fim da consulta lenta. Pressione enter para iniciar a consulta otimizada.");
                    Console.ReadLine();
                    Console.Clear();

                    string strID = String.Join(",", lstID);
                    startTime = DateTime.Now;
                    countSQLConnection = 0;

                    command = new SqlCommand("getClients", conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@strID", strID));

                    countSQLConnection++;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine("CustomerID\tName\t\tCompanyName\t\tSalesPerson\t\tEmailAddress\t\tPhone");
                        while (reader.Read())
                        {
                            Console.WriteLine(String.Format("{0} \t | {1} \t | {2} \t | {3} \t | {4} \t | {5}",
                                reader[0], reader[1], reader[2], reader[3], reader[4], reader[5]));
                        }
                    }

                    endTime = DateTime.Now;

                    span = endTime.Subtract(startTime);
                    Console.WriteLine("Time Difference (seconds): " + span.TotalSeconds);
                    Console.WriteLine("Count SQL Connections: " + countSQLConnection.ToString());
                    Console.WriteLine("Fim da consulta otimizada. Pressione enter para encerrar!");
                    Console.ReadLine();
                    Console.Clear();
                }

            }

            catch (Exception ex)

            {

                Console.WriteLine(ex.Message);

            }

        }
    }
}
