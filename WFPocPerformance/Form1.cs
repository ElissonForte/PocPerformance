using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFPocPerformance
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_MultiplesQuery_Click(object sender, EventArgs e)
        {
            List<Int32> lstID = new List<int>();
            DateTime startTime, endTime;
            int countSQLConnection = 0;
            DataTable dt = new DataTable();
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();

                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnection"].ConnectionString))
                {
                    conn.Open();
                    String sql = "SELECT TOP " + numRowCount.Value.ToString() + " CustomerID FROM [SalesLT].[Customer] WITH (NOLOCK) ORDER BY rowguid";

                    SqlCommand command = new SqlCommand(sql, conn);

                    countSQLConnection++;
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
                            dt.Load(reader);
                        }
                        Application.DoEvents();
                        Cursor.Current = Cursors.WaitCursor;
                    }

                    dataGridView1.DataSource = dt;

                    endTime = DateTime.Now;

                    TimeSpan span = endTime.Subtract(startTime);
                    String str = String.Format("Time Difference (seconds): {0} " + Environment.NewLine + "Count SQL Connections: {1} ", span.TotalSeconds, countSQLConnection.ToString());
                    MessageBox.Show(str);

                }

            }

            catch (Exception ex)

            {

                Console.WriteLine(ex.Message);

            }

            Cursor.Current = Cursors.Default;
        }

        private void btn_OneQuery_Click(object sender, EventArgs e)
        {
            List<Int32> lstID = new List<int>();
            DateTime startTime, endTime;
            int countSQLConnection = 0;
            DataTable dt = new DataTable();
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();

                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnection"].ConnectionString))
                {
                    conn.Open();
                    String sql = "SELECT TOP " + numRowCount.Value.ToString() + " CustomerID FROM [SalesLT].[Customer] WITH (NOLOCK) ORDER BY rowguid";

                    SqlCommand command = new SqlCommand(sql, conn);

                    countSQLConnection++;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lstID.Add(Convert.ToInt32(reader[0].ToString()));
                        }

                    }

                    startTime = DateTime.Now;

                    Int32 index = 0;
                    Int32 count = 10000;
                    List<Task> tasks = new List<Task>();

                    for (int i = 0; i < lstID.Count; i+=count)
                    {
                        tasks.Add(Task.Factory.StartNew(() =>
                        {
                            if ((index + count) > lstID.Count)
                                count = lstID.Count - index;

                            List<int> lstIdBlock = lstID.GetRange(index, count);

                            index += count;

                            string strID = String.Join(",", lstIdBlock);

                            command = new SqlCommand("getClients", conn);
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.Parameters.Add(new SqlParameter("@strID", strID));

                            countSQLConnection++;
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                dt.Load(reader);
                            }

                        }));

                    }

                    Task.WaitAll(tasks.ToArray());

                    dataGridView1.DataSource = dt;

                    endTime = DateTime.Now;

                    TimeSpan span = endTime.Subtract(startTime);
                    String str = String.Format("Time Difference (seconds): {0} " + Environment.NewLine + "Count SQL Connections: {1} ", span.TotalSeconds, countSQLConnection.ToString());
                    MessageBox.Show(str);
                }

            }

            catch (Exception ex)

            {

                Console.WriteLine(ex.Message);

            }
            Cursor.Current = Cursors.Default;
        }
    }
}
